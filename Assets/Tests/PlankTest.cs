using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlankTest
{
    private GameObject plankObject;
    private Plank plank;
    private GameObject playerObject;
    private Inventory inventory;

    [SetUp]
    public void SetUp()
    {
        
        plankObject = Object.Instantiate(Resources.Load<GameObject>("woodPlank"));
        plank = plankObject.GetComponent<Plank>();
        playerObject = new GameObject("Player");
        plank.player = playerObject.transform;
        inventory = playerObject.AddComponent<Inventory>();
        plank.activationDistance = 1.0f;
        plank.obstacle = new GameObject("Obstacle");
        plank.canvasTransform = new GameObject("Canvas").transform;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(plankObject);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void TestUpdateCanvasVisibility()
    {
        plank.canvasCreated = true;
        plank.canvasTransform = new GameObject("Canvas").transform;
        plank.UpdateCanvasVisibility(1.0f);
        Assert.IsTrue(plank.canvasTransform.gameObject.activeSelf);
        plank.UpdateCanvasVisibility(2.0f);
        Assert.IsFalse(plank.canvasTransform.gameObject.activeSelf);
    }

    [Test]
    public void TestCanActivateCanvas()
    {
        inventory.items = new Item[1] { new Item { itemName = "crowbar" } };
        Assert.IsTrue(plank.CanActivateCanvas(1.0f, inventory));
        Assert.IsFalse(plank.CanActivateCanvas(2.0f, inventory));
    }

    [Test]
    public void TestHandleCanvasCreation()
    {
        plank.HandleCanvasCreation();
        Assert.IsTrue(plank.canvasCreated);
        GameObject childCanvas = GameObject.Find(plankObject.name+"Canvas");
        Assert.IsNotNull(childCanvas);
        Assert.IsNotNull(plank);
    }

    
}