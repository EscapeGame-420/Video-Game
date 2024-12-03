using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class InventoryManagerTest : MonoBehaviour
{
    private GameObject _playerTest;
    private Inventory inventory;
    private InventoryManager inventoryManager;
    public Sprite emptySprite;
    private Item item;
    public GameObject itemObject;
    public GameObject toolbarPrefab;

    [SetUp]
    public void SetUp()
    {
        _playerTest = new GameObject("Julie");
        _playerTest.tag = "Player"; // Ensure the player has the correct tag
        itemObject = new GameObject("item");
        itemObject.AddComponent<Item>();
        item = itemObject.GetComponent<Item>();
        emptySprite = Resources.Load<Sprite>("meme");
        if (emptySprite == null)
        {
            Debug.LogError("Empty sprite not found. Ensure it is located at 'Assets/Resources/Sprites/EmptySprite.png'");
            return;
        }
        _playerTest.AddComponent<Inventory>();
        inventory = _playerTest.GetComponent<Inventory>();

        // Add InventoryManager component and initialize it
        

        GameObject toolbarPrefab = Resources.Load<GameObject>("UI/UI");
        GameObject toolbarInstance = Object.Instantiate(toolbarPrefab);
        inventoryManager = toolbarInstance.GetComponent<InventoryManager>();
        // Load and instantiate the toolbar prefab
        if (toolbarPrefab == null)
        {
            Debug.LogError("Toolbar prefab not found. Ensure it is located at 'Assets/Resources/UI/UI.prefab'");
            return;
        }
        // Create and set up the Camera
        GameObject cameraObject = new GameObject("MainCamera");
        Camera camera = cameraObject.AddComponent<Camera>();
        cameraObject.tag = "MainCamera";
        cameraObject.AddComponent<LookAtCam>();
        // Initialize the inventory items array
        
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_playerTest);
        Object.DestroyImmediate(itemObject);
        Object.DestroyImmediate(inventoryManager.gameObject);
        Object.DestroyImmediate(GameObject.Find("UI(Clone)"));
        Object.DestroyImmediate(GameObject.Find("MainCamera"));
    }

    [UnityTest]
    public IEnumerator InventoryManagerTestInit()
    {
        yield return new WaitForSeconds(2.0f); // Wait for the WaitForToolbar coroutine to complete
        Assert.IsNotNull(inventoryManager.toolbar);
    }

   

    

    [UnityTest]
    public IEnumerator InventoryManagerTestIsSelected()
    {
        yield return new WaitForSeconds(2.0f); // Wait for the WaitForToolbar coroutine to complete
        inventoryManager.IsSelected(0);
        Image selectedSlotImage = inventoryManager.toolbar.GetChild(0).GetComponent<Image>();
        Assert.AreEqual(selectedSlotImage.sprite, inventoryManager.outline);
    }
}