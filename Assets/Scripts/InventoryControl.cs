using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour
{
    
    [SerializeField] private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AppearInInventory(GameObject item)
    {
        
    }

    void AddInInventory(GameObject item)
    {
        bool added = inventory.AddItem(item);

        if (added)
        {
            AppearInInventory(item);
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }

    void UpdateInventoryUI()
    {
        
    }


}
