using NUnit.Framework;
using UnityEngine;

public class ClockTests
{
    private GameObject clockObject;
    private Clock clockScript;
    private GameObject playerObject;
    private Inventory mockInventory;

    [SetUp]
    public void SetUp()
    {
        // Create Clock GameObject and attach Clock script
        clockObject = new GameObject("Clock");
        clockScript = clockObject.AddComponent<Clock>();

        // Create Player GameObject and set it in the Clock script
        playerObject = new GameObject("Player");
        clockScript.player = playerObject.transform;

        // Add Canvas as a child of the Clock object
        var canvasObject = new GameObject("Canvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.enabled = false; // Initial state
        canvasObject.transform.SetParent(clockObject.transform);
        clockScript.canvas = canvas;

        // Add placeholder objects for aiguille and cle
        clockScript.aiguille = new GameObject("Aiguille");
        clockScript.cle = new GameObject("Cle");
        clockScript.aiguille.SetActive(false);
        clockScript.cle.SetActive(false);

        // Mock Inventory object
        mockInventory = new GameObject("Inventory").AddComponent<Inventory>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(clockObject);
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(mockInventory.gameObject);
    }

    [Test]
    public void CalculateDistanceToPlayer_ReturnsCorrectValue()
    {
        // Arrange
        clockObject.transform.position = Vector3.zero;
        playerObject.transform.position = new Vector3(3, 4, 0); // Distance = 5

        // Act
        float distance = clockScript.CalculateDistanceToPlayer();

        // Assert
        Assert.AreEqual(5f, distance);
    }

    [Test]
    public void EnableCanvas_SetsCanvasToEnabled()
    {
        // Act
        clockScript.EnableCanvas();

        // Assert
        Assert.IsTrue(clockScript.canvas.enabled);
    }

    [Test]
    public void DisableCanvas_SetsCanvasToDisabled()
    {
        // Arrange
        clockScript.canvas.enabled = true;

        // Act
        clockScript.DisableCanvas();

        // Assert
        Assert.IsFalse(clockScript.canvas.enabled);
    }

    
}
