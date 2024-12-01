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
        clockObject = new GameObject("ClockObject");
        clockScript = clockObject.AddComponent<Clock>();
        playerObject = new GameObject("PlayerObject");
        clockScript.player = playerObject.transform;

        var canvasObject = new GameObject("CanvasObject");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.enabled = false;
        canvasObject.transform.SetParent(clockObject.transform);
        clockScript.canvas = canvas;

        clockScript.aiguille = new GameObject("Aiguille");
        clockScript.cle = new GameObject("Cle");
        clockScript.aiguille.SetActive(false);
        clockScript.cle.SetActive(false);

        mockInventory = new GameObject("MockInventory").AddComponent<Inventory>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(clockObject);
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(mockInventory.gameObject);
    }

    [Test]
    public void CalculateDistanceToPlayerTest()
    {
        clockObject.transform.position = Vector3.zero;
        playerObject.transform.position = new Vector3(3, 4, 0);

        float distance = clockScript.CalculateDistanceToPlayer();

        Assert.AreEqual(5f, distance);
    }

    [Test]
    public void EnableCanvasTest()
    {
        clockScript.EnableCanvas();

        Assert.IsTrue(clockScript.canvas.enabled);
    }

    [Test]
    public void DisableCanvasTest()
    {
        clockScript.canvas.enabled = true;

        clockScript.DisableCanvas();

        Assert.IsFalse(clockScript.canvas.enabled);
    }

    [Test]
    public void InitializeClockTest()
    {
        GameObject mockCanvasPrefab = new GameObject("MockCanvasPrefab");
        mockCanvasPrefab.AddComponent<Canvas>();
        clockScript.prefabPath = "UI/Canvas";
        Resources.Load<GameObject>("UI/Canvas");
        clockObject.AddComponent<Animator>();
        clockScript.InitializeClock();
        Assert.IsNotNull(clockScript.canvas, "Canvas should be initialized.");
        Assert.AreEqual(clockObject.transform.position + clockScript.canvasOffset, clockScript.canvas.transform.position, "Canvas position should match the offset.");
        Assert.IsFalse(clockScript.cle.activeSelf, "Cle object should be inactive after initialization.");
    }

    [Test]
public void FinalizeClockInteractionTest()
{
    // Arrange
    clockScript.canvas.enabled = true;  // Ensure the canvas is enabled
    clockScript.canPick = true;         // Set canPick to true so cle will be activated

    // Act
    clockScript.FinalizeClockInteraction();

    // Assert
    Assert.IsFalse(clockScript.canvas.enabled, "Canvas should be disabled after FinalizeClockInteraction.");
    Assert.IsTrue(clockScript.cle.activeSelf, "Cle object should be active after FinalizeClockInteraction.");
    Assert.IsFalse(clockScript.canPick, "canPick should be false after FinalizeClockInteraction.");
}

}
