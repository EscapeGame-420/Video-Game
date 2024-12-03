using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTest : MonoBehaviour
{
    private GameObject _playerTest;
    private Inventory inventory;
    public Sprite emptySprite;
    private Item item;
    public GameObject itemObject;
    public GameObject toolbarPrefab;

    [SetUp]
    public void SetUp()
    {
        _playerTest = new GameObject("Julie");
        itemObject = new GameObject("item");
        itemObject.AddComponent<Item>();
        item = itemObject.GetComponent<Item>();

        _playerTest.AddComponent<Inventory>();
        inventory = _playerTest.GetComponent<Inventory>();

        // Add InventoryManager component and initialize it
        GameObject inventoryManagerObject = new GameObject("InventoryManager");
        var inventoryManager = inventoryManagerObject.AddComponent<InventoryManager>();
        inventoryManager.inventory = inventory;

        // Load and instantiate the toolbar prefab
        GameObject toolbarPrefab = Resources.Load<GameObject>("UI/UI");
        if (toolbarPrefab == null)
        {
            Debug.LogError("Toolbar prefab not found. Ensure it is located at 'Assets/Resources/UI/UI.prefab'");
            return;
        }
        GameObject toolbarInstance = Object.Instantiate(toolbarPrefab);
        inventoryManager.toolbar = toolbarInstance.transform;

        // Initialize the inventory items array
        inventory.items = new Item[inventory.maxInventorySize];
        for (int i = 0; i < inventory.maxInventorySize; i++)
        {
            inventory.items[i] = new Item { itemName = "Empty Slot" };
        }
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_playerTest);
        Object.DestroyImmediate(itemObject);
        Object.DestroyImmediate(GameObject.Find("InventoryManager"));
        Object.DestroyImmediate(GameObject.Find("UI(Clone)"));
    }

    [Test]
    public void InventoryTestInit()
    {
        inventory.Init();
        Assert.AreEqual(inventory.items.Length, inventory.maxInventorySize);
        for (int i = 0; i < inventory.maxInventorySize; i++)
        {
            Assert.AreEqual(inventory.items[i].itemName, "Empty Slot");
        }
    }

    [Test]
    public void InventoryTestAddItem()
    {
        item.itemName = "Item 1";
        item.sprite = emptySprite;
        inventory.Init();
        inventory.AddItem(item);
        Assert.AreEqual(inventory.items[0].itemName, "Item 1");
    }

    [Test]
    public void InventoryTestUseItem()
    {
        item.itemName = "Item 1";
        item.sprite = emptySprite;
        inventory.Init();
        inventory.AddItem(item);
        inventory.UseItem(item.itemName);
        Assert.AreEqual(inventory.items[0].itemName, "Empty Slot");
    }

    [Test]
    public void InventoryTestIncludeItem()
    {
        item.itemName = "Item 1";
        item.sprite = emptySprite;
        inventory.Init();
        inventory.AddItem(item);
        Assert.AreEqual(inventory.IncludeItemName(item.itemName), true);
    }

    [Test]
    public void InventoryTestSelectingItem()
    {
        item.itemName = "Item 1";
        item.sprite = emptySprite;
        inventory.Init();
        inventory.AddItem(item);
        inventory.selecting = 0;
        Assert.AreEqual(inventory.IsSelectingItem(item.itemName), true);
    }
}