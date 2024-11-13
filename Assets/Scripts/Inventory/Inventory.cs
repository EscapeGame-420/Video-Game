using System;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxInventorySize = 28 / 4; // Set the maximum inventory size
    public Item[] items; // Fixed-size array for inventory
    private int currentItemCount = 0;
    public Sprite emptySprite;
    public Item[] itemInArray;
    private InventoryManager inventoryManager;
    public int selecting = 0;
    void Start()
    {
        items = new Item[maxInventorySize];
        inventoryManager = FindObjectOfType<InventoryManager>(); // Initialize the InventoryManager reference
        for (int i = 0; i < maxInventorySize; i++)
        {
            items[i] = new Item // Create NEW instance per slot
            {
                itemName = "Empty Slot",
                sprite = emptySprite
            };
            Debug.Log($"Slot {i} initialized with unique instance");
        }
    }

    void Update()
    {
        selecting = inventoryManager.selecting;

        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < items.Length; i++)
            {
                Debug.Log("slot " + i + ": " + items[i].itemName);
            }
        }
        itemInArray = items.ToArray();
    }

    public void AddItem(Item item)
    {
        // Ensure the items array is initialized and has the correct size
        if (items == null || items.Length != maxInventorySize)
        {
            Debug.LogError("Inventory array not initialized correctly.");
            return;
        }

        // Find the first empty slot with the name "Empty Slot"
        for (int i = 0; i < maxInventorySize; i++)
        {
            if (items[i].itemName == "Empty Slot") // Found an empty slot
            {
                items[i] = item; // Replace the empty slot with the new item
                currentItemCount++;
                Debug.Log(item.itemName + " added to inventory at slot " + i);
                inventoryManager.MapSprite(i); // Update the UI
                return; // Exit once we've added the item
            }
        }

        // If no empty slot was found, inventory is full
        Debug.Log("Inventory full! Cannot add " + item.itemName);
    }

    public void UseItem(string name)
    {
        for (int i = 0; i < maxInventorySize; i++)
        {
            if (items[i].itemName == name)
            {
                Debug.Log(items[i].itemName + " used from inventory.");
                items[i].itemName = "Empty Slot";
                items[i].sprite = emptySprite;
                currentItemCount--;
                inventoryManager.MapSprite(i); // Update the UI
                Debug.Log("Item used from inventory at slot " + i + ". " + "Now it is " + items[i].itemName);
                return;
            }
        }
        Debug.Log("No usable item found with name " + name);
    }

    public bool IncludeItemName(string name)
    {
        for (int i = 0; i < maxInventorySize - 1; i++)
        {
            if (items[i].itemName == name)
            {
                return true;
            }
        }
        return false;
    }

    // Method to swap items between two slots, even if one or both are empty
    public void SwapItems(int index1, int index2)
    {
        // Ensure indices are within bounds
        if (index1 < 0 || index1 >= maxInventorySize || index2 < 0 || index2 >= maxInventorySize)
        {
            Debug.LogError("Swap indices are out of bounds.");
            return;
        }

        // Swap the items at the given indices
        Item temp = items[index1];
        items[index1] = items[index2];
        items[index2] = temp;

        Debug.Log($"Swapped items in slot {index1} and slot {index2}.");
        inventoryManager.MapSprite(index1); // Update the UI
        inventoryManager.MapSprite(index2); // Update the UI
    }

    public void UseItemFromIndex(int index)
    {
        if (index < 0 || index >= maxInventorySize)
        {
            Debug.LogError("Invalid index to use item from.");
            return;
        }

        Debug.Log(items[index].itemName + " used from inventory.");
        items[index].itemName = "Empty Slot";
        items[index].sprite = emptySprite;
        currentItemCount--;
        inventoryManager.MapSprite(index); // Update the UI
    }
    public Boolean IncludeItem(string name)
    {
       return items.Any(item => item.itemName == name);
    }
    public Boolean IsSelectingItem(string name){
        return items[selecting].itemName == name;
    }
    
}