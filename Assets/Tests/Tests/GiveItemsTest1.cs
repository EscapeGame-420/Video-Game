using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
public class GiveItemsTest
{
    private GameObject giveItemsObject;
    private GiveItems giveItems;
    private GameObject playerObject;
    private Inventory inventory;

    [SetUp]
    public void SetUp()
    {
        giveItemsObject = new GameObject("GiveItems");
        giveItems = giveItemsObject.AddComponent<GiveItems>();

        playerObject = new GameObject("Player");
        giveItems.player = playerObject.transform;
        inventory = playerObject.AddComponent<Inventory>();

        giveItems.activationDistance = 1.0f;
        giveItems.canvasTransform = new GameObject("Canvas").transform;
        giveItems.x = 1.67f;
        giveItems.y = 2.37f;
        giveItems.z = -0.5f;
        inventory.selecting = 0;
        inventory.Init();
        giveItems.itemName = "item";
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(giveItemsObject);
        Object.DestroyImmediate(playerObject);
    }

    

    [Test]
    public void TestCanActivateCanvas()
    {
        Assert.IsTrue(giveItems.CanActivateCanvas(1.0f, inventory));
        Assert.IsFalse(giveItems.CanActivateCanvas(2.0f, inventory));
        inventory.items = new Item[1] { new Item { itemName = "item" } };
        Assert.IsFalse(giveItems.CanActivateCanvas(1.0f, inventory));
    }

    [Test]
    public void TestHandleCanvasCreation()
    {
        // Mock the Item.CreateCanvas method
        GameObject mockCanvas = new GameObject("GiveItemsCanvas");
        mockCanvas.transform.SetParent(giveItemsObject.transform);

        giveItems.HandleCanvasCreation();

        Assert.IsTrue(giveItems.canvasCreated);
        Transform canvasTransform = giveItemsObject.transform.Find("GiveItemsCanvas");
        Assert.IsNotNull(canvasTransform);
        Assert.AreEqual(new Vector3(1.67f, 2.37f, -0.5f), canvasTransform.localPosition);
    }

    [UnityTest]
    public IEnumerator TestHandleInteraction()
    {
        giveItems.canvasTransform = new GameObject("Canvas").transform;
        giveItems.canvasCreated = true;

        // Simulate pressing the "e" key
        yield return null; // Wait for a frame to simulate the Update call

        giveItems.HandleInteraction(inventory);
        Assert.IsTrue(inventory.IncludeItemName("item"));
    }
}