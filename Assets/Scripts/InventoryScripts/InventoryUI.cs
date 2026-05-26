using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlotUI[] slots;

    public void RefreshUI()
    {
        Debug.Log("Inventory: " + inventory);
        Debug.Log("Slots array: " + slots);

        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log("Slot index: " + i);

            Debug.Log("Slot object: " + slots[i]);

            if (i < inventory.GetItems().Count)
            {
                Debug.Log("Item: " + inventory.GetItems()[i]);

                slots[i].SetItem(inventory.GetItems()[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}