using System.Collections.Generic;
using DG.Tweening;
using LTX;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class InventoryUI : MonoSingleton<InventoryUI>
    {
        public GridLayoutGroup GridLayout => gridLayout;

        [SerializeField]
        private GridLayoutGroup gridLayout;
        [SerializeField]
        private InventoryItemUI itemUIPrefab;

        private CanvasGroup canvasGroup;

        private Dictionary<InventoryItemData, InventoryItemUI> itemUis;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;

            itemUis = new Dictionary<InventoryItemData, InventoryItemUI>();
        }

        public void Open(Inventory inventory)
        {
            foreach (InventoryItem item in inventory.Items)
            {
                var itemUI = itemUIPrefab.InstantiatePrefab(gridLayout.transform);
                itemUI.Sync(item);
                itemUis.Add(item.Data, itemUI);

                float delay = transform.GetSiblingIndex() * .07f;

                itemUI.transform.DOKill(true);
                itemUI.canvasGroup.DOFade(1, .15f)
                    .ChangeStartValue(0)
                    .SetDelay(delay);
                itemUI.transform.DOPunchScale(Vector3.one  * .9f, .4f)
                    .SetDelay(delay);
            }

            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, .3f);
            inventory.OnUpdate += Sync;
        }

        public void Close(Inventory inventory)
        {
            inventory.OnUpdate -= Sync;
            gridLayout.transform.ClearChildren();
            itemUis.Clear();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0, .3f)
                .OnComplete(() => canvasGroup.blocksRaycasts = false);
        }

        private void Sync(Inventory inventory)
        {
            using (ListPool<InventoryItemData>.Get(out List<InventoryItemData> remove))
            {
                foreach ((InventoryItemData data, InventoryItemUI uiItem) in itemUis)
                {
                    if (inventory.GetItemQuantity(data) > 0)
                        uiItem.Sync(inventory.GetItem(data));
                    else
                        remove.Add(data);
                }

                foreach (var item in inventory.Items)
                {
                    if (!itemUis.ContainsKey(item.Data))
                    {
                        var itemUI = itemUIPrefab.InstantiatePrefab(gridLayout.transform);
                        itemUI.Sync(item);
                        itemUis.Add(item.Data, itemUI);
                    }
                }

                foreach (var data in remove)
                {
                    itemUis.Remove(data, out var uiItem);
                    uiItem.DestroyGameObject();
                }
            }
        }



    }
}