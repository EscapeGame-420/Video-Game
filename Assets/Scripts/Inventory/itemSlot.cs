using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public InventoryManager inventoryManage;  // Reference to your InventoryManage instance
    private Inventory inventory;             // Reference to the Inventory instance
    [SerializeField]
    private AudioClip selectionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Find the InventoryManage and Inventory instances in the scene
        inventoryManage = FindFirstObjectByType<InventoryManager>();
        inventory = FindFirstObjectByType<Inventory>();

        if (inventoryManage == null || inventory == null)
        {
            Debug.LogError("InventoryManage or Inventory not found in the scene.");
        }
    }

    // Called when an item is dropped onto this slot
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PlaySelectionSound();

            GameObject draggedObject = eventData.pointerDrag;
            DragNDrop dragNDropScript = draggedObject.GetComponent<DragNDrop>();

            int originalSlot = CalculateSlotIndex(dragNDropScript.originalParent.name, dragNDropScript.originalIndex);
            int targetSlot = CalculateSlotIndex(gameObject.transform.parent.name, ExtractSlotIndex(gameObject.name));

            Transform targetParent = gameObject.transform;

            if (targetParent.childCount > 0)
            {
                // Swap the current child in the target slot with the dragged item
                Transform currentChild = targetParent.GetChild(0);
                currentChild.SetParent(dragNDropScript.originalParent);
                currentChild.localPosition = Vector3.zero;

                draggedObject.transform.SetParent(targetParent);
                draggedObject.transform.localPosition = Vector3.zero;

                // Update the original index of the swapped item
                DragNDrop currentChildDragNDrop = currentChild.GetComponent<DragNDrop>();
                currentChildDragNDrop.originalParent = dragNDropScript.originalParent;
                currentChildDragNDrop.originalIndex = originalSlot;
            }
            else
            {
                // If no child in the target slot, just move the dragged item
                draggedObject.transform.SetParent(targetParent);
                draggedObject.transform.localPosition = Vector3.zero;
            }

            // Update the original parent and index of the dragged item
            dragNDropScript.originalParent = targetParent;
            dragNDropScript.originalIndex = targetSlot;

            // Swap the items in the inventory
            inventory.SwapItems(originalSlot, targetSlot);
            Debug.Log($"Swapped items at index {originalSlot} and {targetSlot}");

            // Update the UI to reflect the changes
            inventoryManage.MapSprite(originalSlot);
            inventoryManage.MapSprite(targetSlot);
        }
    }

    // Method to calculate the slot index based on the parent name
    private int CalculateSlotIndex(string parentName, int columnIndex)
    {
        if (parentName.StartsWith("Row"))
        {
            int rowIndex = ExtractSlotIndex(parentName);
            return (rowIndex * 7) + columnIndex + 6;
        }
        else
        {
            return columnIndex;
        }
    }

    // Method to extract slot index from the game object name
    private int ExtractSlotIndex(string name)
    {
        // Implement your logic to extract the slot index from the name
        // This is just a placeholder example
        Match match = Regex.Match(name, @"\d+");
        return match.Success ? int.Parse(match.Value) : -1;
    }

    public void PlaySelectionSound(){
        if(selectionSound != null){
            audioSource.clip = selectionSound;
            audioSource.Play();
        }
    }
}