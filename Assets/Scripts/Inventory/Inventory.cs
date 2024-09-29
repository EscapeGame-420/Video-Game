using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].itemName);  
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.name + " added to inventory.");
    }

    public void UseItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.name + " removed from inventory.");
        }
        else
        {
            Debug.Log(item.name + " not found in inventory.");
        }
    }
}
