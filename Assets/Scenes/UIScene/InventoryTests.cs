using System;
using Inventories;
using Inventories.UI;
using UnityEngine;

public class InventoryTests : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InventoryUI inventoryUi;

    [SerializeField]
    private CraftUI craftUI;


    private void Start()
    {
        inventoryUi.Open(inventory);
        craftUI.Open(inventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Start();
    }
}