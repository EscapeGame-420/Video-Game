using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;       // Reference to your Inventory script
    public Transform toolbar;         // Reference to the Toolbar Transform
    public Sprite outline;            // Outline sprite for the selected slot
    public Sprite outlineEmpty;       // Outline sprite for unselected slots
    private List<Item> oldList;       // Keep track of the previous inventory state
    private int selecting = 0;        // Currently selected toolbar index
    private int totalSlots = 0;       // Total number of toolbar slots
    private Text text;
    void Start()
    {
        toolbar = GameObject.Find("Toolbar").transform;

        if (inventory != null && toolbar != null)
        {
            totalSlots = toolbar.childCount; // Get the total number of toolbar slots
            MapSprite(0); // Initial sprite mapping
        }
        else
        {
            Debug.LogError("Inventory or Toolbar not assigned or found!");
        }
    }

    void Update()
    {
        if (inventory != null && (oldList == null || oldList != inventory.items))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                MapSprite(i);
            }
            oldList = new List<Item>(inventory.items);
        }

        for (int i = 1; i <= 8; i++) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i)) 
            {
                selecting = i - 1; 
                IsSelected(selecting); 
                Debug.Log($"Slot {i} selected.");
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f) 
        {
            if (scroll > 0f)
            {
                
                selecting = (selecting - 1 + totalSlots) % totalSlots;
            }
            else if (scroll < 0f)
            {
                
                selecting = (selecting + 1) % totalSlots;
            }
            IsSelected(selecting); 
            Debug.Log($"Slot {selecting + 1} selected via scroll.");
        }
    }

    void MapSprite(int x)
    {
        if (x < inventory.items.Count)
        {
            Item currentItem = inventory.items[x];
            Transform toolbarSlot = toolbar.GetChild(x);

            // Check if the slot already has a child, if not create a new item image
            if (toolbarSlot.childCount < 1)
            {
                GameObject newItemImage = new GameObject(currentItem.itemName);
                newItemImage.transform.SetParent(toolbarSlot);

                Image itemImageComponent = newItemImage.AddComponent<Image>();
                itemImageComponent.sprite = currentItem.sprite;

                RectTransform rectTransform = newItemImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 70);
                rectTransform.anchoredPosition = Vector2.zero;
            }

            Debug.Log($"Mapped {currentItem.itemName} to toolbar slot {x}.");
        }
        else
        {
            Debug.LogWarning("Index out of range. Inventory may be smaller than toolbar slots.");
        }
    }

    // Method to highlight the selected toolbar slot
    void IsSelected(int index)
    {
        // Reset all toolbar slots to the empty outline sprite
        for (int i = 0; i < toolbar.childCount; i++)
        {
            Image currSlotImage = toolbar.GetChild(i).GetComponent<Image>();
            currSlotImage.sprite = outlineEmpty;
        }

        // Highlight the currently selected slot with the outline sprite
        if (index >= 0 && index < toolbar.childCount) // Ensure index is in range
        {
            Image selectedSlotImage = toolbar.GetChild(index).GetComponent<Image>();
            selectedSlotImage.sprite = outline;
            Debug.Log($"Toolbar slot {index + 1} highlighted.");
        }
    }
}
