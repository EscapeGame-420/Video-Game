using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InventoryManage : MonoBehaviour
{
    public Inventory Toolbar;          // Reference to your Toolbar script
    public Transform toolbar;          // Reference to the Toolbar Transform
    public Sprite outline;             // Outline sprite for the selected slot
    public Sprite outlineEmpty;        // Outline sprite for unselected slots
    private Item[] oldList;            // Keep track of the previous Toolbar state
    private int selecting = 0;         // Currently selected toolbar index
    private int totalSlots = 0;        // Total number of toolbar slots
    public Transform mainToolbar;
    private Item[] toolbarArray;

    void Start()
    {
        toolbar = GameObject.Find("Toolbar").transform;
        mainToolbar = GameObject.Find("InventoryFrame").transform;

        if (Toolbar != null && toolbar != null)
        {
            totalSlots = toolbar.childCount;
            MapAllSprites(); // Initial sprite mapping for all slots
        }
        else
        {
            Debug.LogError("Toolbar or Toolbar not assigned or found!");
        }
    }

    // Method to map sprites to all toolbar slots
    public void MapAllSprites()
    {
        for (int i = 0; i < Toolbar.items.Length; i++)
        {
            MapSprite(i);
        }
    }

    // Method to map sprite to a specific toolbar slot
    public void MapSprite(int x)
    {
        if (x < Toolbar.items.Length && x < toolbar.childCount)
        {
            Item currentItem = Toolbar.items[x];
            Transform toolbarSlot = toolbar.GetChild(x);

            // Clear existing children
            foreach (Transform child in toolbarSlot)
            {
                Destroy(child.gameObject);
            }

            // Log item mapping
            if (currentItem.itemName != "Empty Slot")
            {
                Debug.Log($"Mapping item {currentItem.itemName} to toolbar slot {x}");
            }
            else
            {
                Debug.Log($"No item in toolbar slot {x}");
            }

            // Check if the slot already has a child, if not create a new item image
            if (currentItem.itemName != "Empty Slot")
            {
                GameObject newItemImage = new GameObject(currentItem.itemName);
                newItemImage.transform.SetParent(toolbarSlot);

                Image itemImageComponent = newItemImage.AddComponent<Image>();
                itemImageComponent.sprite = currentItem.sprite;

                DragNDrop dragNDrop = newItemImage.AddComponent<DragNDrop>();
                CanvasGroup canvasGroup = newItemImage.AddComponent<CanvasGroup>();
                canvasGroup.interactable = true;

                // Set the dragNDrop's canvasGroup reference
                dragNDrop.SetCanvasGroup(canvasGroup);

                RectTransform rectTransform = newItemImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(70, 70);
                rectTransform.anchoredPosition = Vector2.zero;

                Debug.Log($"Created item image for {currentItem.itemName} in toolbar slot {x}");
            }
        }
        else
        {
            Debug.LogWarning("Index out of range. Toolbar may be smaller than toolbar slots.");
        }
    }

    // Update method for user input and handling inventory interactions
    void Update()
    {
        toolbarArray = Toolbar.itemInArray;
        if (oldList == null || !AreItemsEqual(oldList, toolbarArray))
        {
            MapAllSprites();
            oldList = (Item[])Toolbar.items.Clone(); // Clone the array for future comparisons
        }

        for (int i = 1; i <= 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                selecting = i - 1;
                IsSelected(selecting);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mainToolbar.gameObject.SetActive(!mainToolbar.gameObject.activeSelf);
            Cursor.lockState = mainToolbar.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            selecting = (selecting + (scroll > 0f ? -1 : 1) + totalSlots) % totalSlots;
            IsSelected(selecting);
            Debug.Log($"Slot {selecting + 1} selected via scroll.");
        }
    }

    // Method to highlight the selected toolbar slot
    void IsSelected(int index)
    {
        for (int i = 0; i < toolbar.childCount; i++)
        {
            Image currSlotImage = toolbar.GetChild(i).GetComponent<Image>();
            currSlotImage.sprite = outlineEmpty;
        }

        if (index >= 0 && index < toolbar.childCount)
        {
            Image selectedSlotImage = toolbar.GetChild(index).GetComponent<Image>();
            selectedSlotImage.sprite = outline;
        }
    }

    // Method to compare items between two arrays (for detecting changes)
    private bool AreItemsEqual(Item[] array1, Item[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false; // Arrays are of different lengths
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i].itemName != array2[i].itemName) // Compare by item name
            {
                return false;
            }
        }
        return true; // All items are equal
    }


    
}