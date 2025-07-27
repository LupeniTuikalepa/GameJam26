
using System;
using Crafts;
using Inventories;
using Inventories.UI;
using UnityEngine;

public class InventoryTests : MonoBehaviour, IInventoryContainer
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InventoryUI inventoryUi;

    public Inventory GetInventory() => inventory;

    public void SetInventory(Inventory inventory) => this.inventory = inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            inventoryUi.Bind(this);
    }
}