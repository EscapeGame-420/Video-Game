using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LockTest
{
    private GameObject locksObject;
    private Lock locks;
    private GameObject playerObject;
    private Inventory inventory;

    [SetUp]
    public void SetUp()
    {
        
        locksObject = new GameObject("Lock");
        locks = locksObject.AddComponent<Lock>();
        playerObject = new GameObject("Player");
        locks.player = playerObject.transform;
        inventory = playerObject.AddComponent<Inventory>();
        locks.activationDistance = 1.0f;
        locks.canvasTransform = new GameObject("Canvas").transform;
        locks.itemName = "key";
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(locksObject);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void TestUpdateCanvasVisibility()
    {
        locks.canvasCreated = true;
        locks.canvasTransform = new GameObject("Canvas").transform;
        locks.UpdateCanvasVisibility(1.0f);
        Assert.IsTrue(locks.canvasTransform.gameObject.activeSelf);
        locks.UpdateCanvasVisibility(2.0f);
        Assert.IsFalse(locks.canvasTransform.gameObject.activeSelf);
    }

    [Test]
    public void TestCanActivateCanvas()
    {
        inventory.items = new Item[1] { new Item { itemName = "key" } };
        Assert.IsTrue(locks.CanActivateCanvas(1.0f, inventory));
        Assert.IsFalse(locks.CanActivateCanvas(2.0f, inventory));
    }

    [Test]
    public void TestHandleCanvasCreation()
    {
        locks.HandleCanvasCreation();
        Assert.IsTrue(locks.canvasCreated);
        GameObject childCanvas = GameObject.Find(locksObject.name+"Canvas");
        Assert.IsNotNull(childCanvas);
        Assert.IsNotNull(locks);
    }

    
}