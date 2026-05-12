using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Assets")]
public class Inventory : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public const int size = 6;

    private List<GameObject> InventoryContainer = new List<GameObject>();

    public bool AddItem(GameObject item)
    {
        if (InventoryContainer.Count >= size)
        {
            return false;
        }

        InventoryContainer.Add(item);
        return true;
    }

    public List<GameObject> GetItems()
    {
        return InventoryContainer;
    }
}
