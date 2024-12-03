using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class DeathSequenceTest
{
    private GameObject gameObject;
    private DeathSequence deathSequence;
    private JulieMovement mockJulieMovement;
    private JulieVisionFollow mockJulieVisionFollow;
    private Image mockBlackScreen;
    private GameObject mockCanvas;
    private AudioListener audioListener;
    private AudioSource audioSource;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        deathSequence = gameObject.AddComponent<DeathSequence>();

        var mockAnimator = gameObject.AddComponent<Animator>();
        mockJulieMovement = gameObject.AddComponent<JulieMovement>();
        deathSequence.julieMovement = mockJulieMovement;

        mockCanvas = new GameObject();
        mockCanvas.AddComponent<Canvas>();
        deathSequence.deathCanvas = mockCanvas;

        mockBlackScreen = new GameObject().AddComponent<Image>();
        deathSequence.blackScreen = mockBlackScreen;

        mockJulieVisionFollow = new GameObject().AddComponent<JulieVisionFollow>();
        deathSequence.julieVisionFollow = mockJulieVisionFollow;

        audioListener = new GameObject("AudioListener").AddComponent<AudioListener>();

        audioSource = gameObject.AddComponent<AudioSource>();
        deathSequence.audioSource = audioSource;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(deathSequence);
        Object.Destroy(mockJulieMovement);
        Object.Destroy(mockJulieVisionFollow);
        Object.Destroy(mockCanvas);
        Object.Destroy(mockBlackScreen.gameObject);
        Object.Destroy(audioListener.gameObject);
        Object.Destroy(audioSource);
        Object.Destroy(gameObject);
    }

    [Test]
    public void SetInitialCanvasStateTest()
    {
        deathSequence.SetInitialCanvasState();
        Assert.IsFalse(mockCanvas.activeSelf);
    }

    [Test]
    public void TriggerDeathSequenceTest()
    {
        deathSequence.TriggerDeathSequence();
        
        Assert.IsTrue(mockCanvas.activeSelf);
        Assert.IsFalse(mockJulieMovement.enabled);
        Assert.IsFalse(mockJulieVisionFollow.enabled);
        Assert.IsTrue(Cursor.visible);
        Assert.AreEqual(Cursor.lockState, CursorLockMode.None);
    }

    [Test]
    public void SetImageAlphaTest()
    {
        deathSequence.SetImageAlpha(mockBlackScreen, 0);
        Assert.AreEqual(mockBlackScreen.color.a, 0f);

        deathSequence.SetImageAlpha(mockBlackScreen, 1);
        Assert.AreEqual(mockBlackScreen.color.a, 1f);
    }
}
