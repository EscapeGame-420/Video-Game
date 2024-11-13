using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Inventory inventory; // Add this line

    // Save the original index and parent
    public int originalIndex; // Public to access in itemSlot
    public Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Ui").GetComponent<Canvas>();
        inventory = FindObjectOfType<Inventory>(); // Initialize the inventory variable
    
    }

    // This is called when dragging starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Save the original index and parent when dragging begins
        originalParent = rectTransform.parent;
        originalIndex = ExtractSlotIndex(originalParent.name); // Get the slot index from the parent
    }

    // This is called when the item is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // This is called when the dragging ends (when dropped)
  public void OnEndDrag(PointerEventData eventData)
{
    canvasGroup.alpha = 1f;
    canvasGroup.blocksRaycasts = true;

    // Check if the item is dropped into the "DropZone" GameObject
    if (eventData.pointerEnter != null && eventData.pointerEnter.name == "DropZone")
    {
        Debug.Log("rm");
    }
    else
    {
        // Handle other drop targets if necessary
        if (eventData.pointerEnter != null)
        {
            Debug.Log($"Dropped on {eventData.pointerEnter.name}");
        }
        else
        {
            inventory.UseItemFromIndex(originalIndex); // Call the UseItem method in the Inventory script
            Destroy(gameObject); // Destroy the item object
        }
    }
}

    // A method to extract the index from the parent name (you might already have this implemented)
    private int ExtractSlotIndex(string parentName)
    {
        // Assuming parentName is something like "Slot1", "Slot2", etc.
        if (int.TryParse(System.Text.RegularExpressions.Regex.Match(parentName, @"\d+").Value, out int index))
        {
            return index ; // Convert to 0-based index
        }
        return 0; // Return -1 if no valid index is found
    }

    // Add the SetCanvasGroup method here
    public void SetCanvasGroup(CanvasGroup group)
    {
        canvasGroup = group;
    }
}