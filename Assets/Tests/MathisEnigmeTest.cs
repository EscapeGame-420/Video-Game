using NUnit.Framework;
using UnityEngine;

public class MathisEnigmeTests
{
    private GameObject testObject;
    private MathisEnigme mathisEnigme;
    private GameObject fire;

    private string prefabPath = "UI/Canvas";

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        mathisEnigme = testObject.AddComponent<MathisEnigme>();
        mathisEnigme.prefabPath = prefabPath;
        mathisEnigme.player = new GameObject().transform;
        mathisEnigme.player.position = Vector3.zero;
        mathisEnigme.canvasOffset = new Vector3(0, 1, 0);

        mathisEnigme.bigFlame = testObject.AddComponent<SpriteRenderer>();
        mathisEnigme.aiguille = new GameObject();
        
       
    }

    [Test]
    public void InitializeCanvasTest()
    {
        mathisEnigme.InitializeCanvas();

        Assert.NotNull(mathisEnigme.canvas);

        var lookAtCam = mathisEnigme.canvas.gameObject.GetComponent<LookAtCam>();
        Assert.NotNull(lookAtCam);

        Vector3 expectedPosition = testObject.transform.position + mathisEnigme.canvasOffset;
        Assert.AreEqual(expectedPosition, mathisEnigme.canvas.transform.position);
    }

    [Test]
    public void InitializeObjectsTest()
    {
        mathisEnigme.InitializeObjects();

        Assert.IsFalse(mathisEnigme.bigFlame.enabled);
        Assert.IsFalse(mathisEnigme.aiguille.activeSelf);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testObject);
        Object.DestroyImmediate(mathisEnigme.player.gameObject);
        Object.DestroyImmediate(fire);
    }
}
