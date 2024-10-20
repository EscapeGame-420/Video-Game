using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //private List<string> items = new List<string>();

    public static List<string> items = new List<string>();
    void Update(){
        
        if (Input.GetKeyDown("tab")){
            for (int i = 0; i < items.Count; i++){
                Debug.Log(items[i]);
            }
        }

    }

    public void AddItem(string item){

        items.Add(item);
        Debug.Log(item + " added to inventory.");

    }

    public void UseItem(string item){

        if (items.Contains(item)){
            items.Remove(item);
            Debug.Log(item + " removed from inventory.");
        }else{
            Debug.Log(item + " not found in inventory.");
        }
        
    }
}
