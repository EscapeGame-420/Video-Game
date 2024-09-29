using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory; // Reference to your Inventory script
    public Transform toolbar;  // Reference to the Toolbar Transform

    void Start()
    {
        toolbar = GameObject.Find("Toolbar").transform;
        if (inventory != null && toolbar != null)
        {
            MapSprite(0);
        }
        else
        {
            Debug.LogError("Inventory or Toolbar not assigned or found!");
        }
    }
    void Update()
    {
        if (inventory != null)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                MapSprite(i);
            }
        }
    }
    void MapSprite(int x)
    {
        if (inventory.items.Count > 0)
        {
            Item firstItem = inventory.items[x];
            Image firstToolbarSlotImage = toolbar.GetChild(x).GetComponent<Image>();
            firstToolbarSlotImage.sprite = firstItem.sprite;
            Debug.Log($"Mapped {firstItem.itemName} to the first toolbar slot.");
        }
        else
        {
            Debug.LogWarning("Inventory is empty, no item to map to the toolbar.");
        }
    }
}
