using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using LTX;
using LTX.Singletons;
using UnityEngine;

namespace Inventories.UI
{
    public class CraftUI : MonoSingleton<CraftUI>
    {
        private static InventoryItemData[] craftableItems;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadCraftableItems()
        {
            craftableItems = Resources.LoadAll<InventoryItemData>("Items")
                .Where(ctx => ctx.IsCraftable)
                .ToArray();
        }

        [SerializeField]
        private Transform container;
        [SerializeField]
        private CraftElementUI craftElementUIPrefab;

        private Inventory currentInventory;

        private Dictionary<CraftElementUI, InventoryItemData> elements;
        private CanvasGroup canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            elements = new Dictionary<CraftElementUI, InventoryItemData>(craftableItems.Length);

            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;

        }

        private void Start()
        {
            for (int i = 0; i < craftableItems.Length; i++)
            {
                CraftElementUI instance = craftElementUIPrefab.InstantiatePrefab(container);
                instance.Sync(craftableItems[i], this);
                elements.Add(instance, craftableItems[i]);
            }
        }


        public void Open(Inventory inventory)
        {
            if(currentInventory != null)
                Close(currentInventory);

            currentInventory = inventory;
            currentInventory.OnUpdate += UpdateElements;

            canvasGroup.DOKill();

            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, .3f);

            UpdateElements(currentInventory);
        }

        public void Close(Inventory inventory)
        {
            if (currentInventory == inventory)
            {
                currentInventory.OnUpdate -= UpdateElements;
                currentInventory = null;
            }

            canvasGroup.DOKill();

            canvasGroup.DOFade(0, .3f)
                .OnComplete(() => canvasGroup.blocksRaycasts = false);
        }

        private void UpdateElements(Inventory inventory)
        {
            foreach ((CraftElementUI element, InventoryItemData data) in elements)
                element.UpdateCraftableState(data, inventory);
        }

        public void Craft(CraftElementUI craftElementUI)
        {
            if (elements.TryGetValue(craftElementUI, out InventoryItemData data))
            {
                data.Consume(currentInventory);
                currentInventory.AddItem(data, data.CraftQuantity);
            }
        }
    }
}