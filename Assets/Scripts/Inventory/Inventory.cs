using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item){

        items.Add(item);
        Debug.Log(item.name + " added to inventory.");

    }

    public void UseItem(GameObject item){

        if (items.Contains(item)){
            items.Remove(item);
            Debug.Log(item.name + " removed from inventory.");
        }else{
            Debug.Log(item.name + " not found in inventory.");
        }
        
    }
}
