using UnityEngine;

public class InventoryControl : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ItemData testItem;

    private void Start()
    {
        inventory.ClearInventory();
        inventoryUI.RefreshUI();
    }

    public void AddInInventory(ItemData item)
    {
        bool added = inventory.AddItem(item);

        if (added)
        {
            inventoryUI.RefreshUI();
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    public void TestAddItem()
    {
        AddInInventory(testItem);
    }
}