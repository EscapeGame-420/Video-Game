using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;          // Reference to your Toolbar script
    public Transform toolbar;          // Reference to the Toolbar Transform
    public Sprite outline;             // Outline sprite for the selected slot
    public Sprite outlineEmpty;        // Outline sprite for unselected slots
    private Item[] oldList;            // Keep track of the previous Toolbar state
    public int selecting = 0;         // Currently selected toolbar index
    private int totalSlots = 0;        // Total number of toolbar slots
    private Item[] toolbarArray;
    private bool openingInvent;

    void Start()
    {
        StartCoroutine(WaitForToolbar());
    }

    IEnumerator WaitForToolbar()
    {
        // Wait for the death sequence to complete (adjust the delay as needed)
        yield return new WaitForSeconds(2.0f); // Delay of 1 second (or more) to allow the death sequence to finish

        // Now, wait for the toolbar to be active in the hierarchy
        while (toolbar == null || !toolbar.gameObject.activeInHierarchy)
        {
            toolbar = GameObject.Find("Toolbar")?.transform;
            yield return null; // Wait for the next frame
        }

        // Once the toolbar is active, proceed with initialization
        if (toolbar != null)
        {
            inventory = GameObject.Find("Julie").GetComponent<Inventory>();
            totalSlots = toolbar.childCount;
            MapAllSprites(); // Initial sprite mapping for all slots
        }
        else
        {
            Debug.LogError("Toolbar not found or inactive!");
        }
       
        
        
    }

    // Method to map sprites to all toolbar slots
    public void MapAllSprites()
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            MapSprite(i);
            Debug.Log("Mapping all sprites");
        }
    }

    // Method to map sprite to a specific toolbar slot
    public void MapSprite(int x)
    {
        if (x < inventory.items.Length && x < toolbar.childCount)
        {
            Item currentItem = inventory.items[x];
            Transform toolbarSlot = toolbar.GetChild(x);

            // Log item mapping
            if (currentItem.itemName != "Empty Slot")
            {
                Debug.Log($"Mapping item {currentItem.itemName} to toolbar slot {x}");
            }
            else
            {
                Debug.Log($"No item in toolbar slot {x}");
            }

            // Delete existing sprite
            DeleteSprite(toolbarSlot);

            // Create new sprite if the item is not empty
            if (currentItem.itemName != "Empty Slot")
            {
                CreateSprite(toolbarSlot, currentItem);
            }
        }
        else
        {
            Debug.LogWarning("Index out of range. Toolbar may be smaller than toolbar slots.");
        }
    }

    // Method to delete existing sprite from a toolbar slot
    private void DeleteSprite(Transform toolbarSlot)
    {
        foreach (Transform child in toolbarSlot)
        {
            Debug.Log("Destroying " + child.gameObject.name);
            Destroy(child.gameObject);
        }
    }

    // Method to create a new sprite in a toolbar slot
    private void CreateSprite(Transform toolbarSlot, Item currentItem)
    {
        GameObject newItemImage = new GameObject(currentItem.itemName);
        newItemImage.transform.SetParent(toolbarSlot);

        Image itemImageComponent = newItemImage.AddComponent<Image>();
        itemImageComponent.sprite = currentItem.sprite;
        // itemImageComponent.raycastTarget = true; // Ensure Raycast Target is enabled

        // //DragNDrop dragNDrop = newItemImage.AddComponent<DragNDrop>();
        // CanvasGroup canvasGroup = newItemImage.AddComponent<CanvasGroup>();
        // canvasGroup.interactable = true; // Ensure CanvasGroup is interactable
        // canvasGroup.blocksRaycasts = true; // Ensure CanvasGroup blocks raycasts

        // Set the dragNDrop's canvasGroup reference
        //dragNDrop.SetCanvasGroup(canvasGroup);

        RectTransform rectTransform = newItemImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(70, 70);
        rectTransform.anchoredPosition = Vector2.zero;

        Debug.Log($"Created item image for {currentItem.itemName} in toolbar slot {toolbarSlot.GetSiblingIndex()}");
    }

    // Other methods in InventoryManager...

    void Update()
    {
          if (inventory == null)
        {
            Debug.LogError("Inventory not found!");
        }


        toolbarArray = inventory.items.Take(7).ToArray(); // Get the first 8 items from the Toolbar
        
        if (oldList == null || !AreItemsEqual(oldList, toolbarArray))
        {
            
            MapAllSprites();
            oldList = (Item[])inventory.items.Clone(); // Clone the array for future comparisons
        }

        for (int i = 1; i <= 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                selecting = i - 1;
                IsSelected(selecting);
            }
        }

        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     Cursor.lockState = openingInvent ? CursorLockMode.None : CursorLockMode.Locked;
        //     openingInvent = !openingInvent;
        // }

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
            return false; 
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i].itemName != array2[i].itemName) 
            {
                return false;
            }
        }
        return true; 
    }


    
}