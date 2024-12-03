using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;  // For the Unity Text component

public class ItemTests
{
    private GameObject testObject;
    private Item itemComponent;
    private AudioSource audioSource;
    private AudioClip testClip;
    private GameObject canvasObject;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        itemComponent = testObject.AddComponent<Item>();
        audioSource = testObject.AddComponent<AudioSource>();
        itemComponent.audioSource = audioSource;
        testClip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        itemComponent.selectionSound = testClip;
        canvasObject = new GameObject("CanvasObject");
    }

    [Test]
    public void PlaySelectionSoundTest()
    {
        Canvas canvas = Item.CreateCanvas(testObject);
        itemComponent.canvas = canvas;
        itemComponent.PlaySelectionSound();
        Assert.AreEqual(testClip, audioSource.clip);
        Assert.IsTrue(audioSource.isPlaying);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObject);
        GameObject.DestroyImmediate(canvasObject);
    }
}
