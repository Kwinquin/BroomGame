using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Assets")]
public class Inventory : ScriptableObject
{
    public const int size = 6;

    private List<ItemData> InventoryContainer = new List<ItemData>();

    public bool AddItem(ItemData item)
    {
        if (InventoryContainer.Count >= size)
        {
            return false;
        }

        InventoryContainer.Add(item);
        return true;
    }

    public List<ItemData> GetItems()
    {
        return InventoryContainer;
    }

    public void ClearInventory()
    {
        InventoryContainer.Clear();
    }
}