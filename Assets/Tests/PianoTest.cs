using NUnit.Framework;
using UnityEngine;

public class PianoTest: MonoBehaviour
{
    private GameObject pianoControllerObject;
    private PianoController pianoController;
    private GameObject canvasObject;

    [SetUp]
    public void SetUp()
    {
        pianoControllerObject = new GameObject("PianoController");
        pianoController = pianoControllerObject.AddComponent<PianoController>();

        canvasObject = new GameObject("Canvas");
        pianoController.canvasToOpen = canvasObject;
        pianoController.pianoKey = new GameObject("key");

    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(pianoControllerObject);
        Object.DestroyImmediate(canvasObject);
    }

    [Test]
    public void TestCloseCanvas()
    {
        // Set initial state
        canvasObject.SetActive(true);
        pianoController.isCanvasActive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Call the method
        pianoController.CloseCanvas();

        // Assert the expected state
        Assert.IsFalse(canvasObject.activeSelf);
        Assert.IsFalse(pianoController.isCanvasActive);
        Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);
        Assert.IsFalse(Cursor.visible);
    }
    [Test]
    public void TestToggleCanvas()
    {
        canvasObject.SetActive(false);
        pianoController.isCanvasActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pianoController.ToggleCanvas();

        Assert.IsTrue(canvasObject.activeSelf);
        Assert.IsTrue(pianoController.isCanvasActive);
        Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
        Assert.IsTrue(Cursor.visible);

        pianoController.ToggleCanvas();

        Assert.IsFalse(canvasObject.activeSelf);
        Assert.IsFalse(pianoController.isCanvasActive);
        Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);
        Assert.IsFalse(Cursor.visible);
    }
}