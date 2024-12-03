using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyCounter : MonoBehaviour
{
    public Inventory inventory;
    public Door door;
    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        door = GameObject.FindObjectOfType<Door>();
    }
    void FixedUpdate()
    {
        GetKeyCount();
    }
    void GetKeyCount()
    {
    int lockOpened = 0;
    for (int i = 0; i < inventory.items.Length; i++)
    {
        if (inventory.items[i].itemName == "gold" || inventory.items[i].itemName == "red" || inventory.items[i].itemName == "blue")
        {
            lockOpened++;
        }
    }
    //Debug.Log("Lock opened: " + lockOpened);
    int lockLeft = (int) Door.lockopened;
    
    TextMeshProUGUI textMeshPro = GameObject.Find("Objective").GetComponent<TMPro.TextMeshProUGUI>();
    textMeshPro.text = lockOpened + "/" + (4- lockLeft);
    }
}
