using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class ItemSlot : MonoBehaviour
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