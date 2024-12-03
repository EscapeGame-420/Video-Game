using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;
 
public class FadeOutEffectTests
{
    private FadeOutEffect fadeOutEffect;
    private Image fadeImage;
    private Image crosshair;
    private GameObject toolbar;
    
 
    [SetUp]
    public void SetUp()
    {
        fadeOutEffect = new GameObject().AddComponent<FadeOutEffect>();
        fadeImage = new GameObject().AddComponent<Image>();
        fadeOutEffect.fadeImage = fadeImage;

        crosshair = new GameObject().AddComponent<Image>();
        fadeOutEffect.crosshair = crosshair;

        toolbar = new GameObject();
        fadeOutEffect.toolbar = toolbar;

    }
 
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(fadeOutEffect);
        Object.DestroyImmediate(fadeImage);
        Object.DestroyImmediate(crosshair);
        Object.DestroyImmediate(toolbar);
    }
 
    [Test]
    public void TestInitializeFadeImage()
    {
        fadeOutEffect.InitializeFadeImage();
        Assert.AreEqual(new Color(0f, 0f, 0f, 1f), fadeImage.color);
    }

    [Test]
    public void TestHideCrosshair()
    {
        crosshair.enabled = true;
        fadeOutEffect.HideCrosshair();
        Assert.IsFalse(crosshair.enabled);
    }

    [Test]
    public void TestHideToolbar()
    {
        Assert.IsTrue(toolbar.activeSelf);
        fadeOutEffect.HideToolbar();
        Assert.IsFalse(toolbar.activeSelf);
    }

    [Test]
    public void TestEnableCrosshair()
    {
        crosshair.enabled = false;
        fadeOutEffect.EnableCrosshair();
        Assert.IsTrue(crosshair.enabled);
    }

    [Test]
    public void TestShowToolbar()
    {
        toolbar.SetActive(false);
        fadeOutEffect.ShowToolbar();
        Assert.IsTrue(toolbar.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestFadeOutCoroutine()
    {
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        fadeOutEffect.StartCoroutine(fadeOutEffect.FadeOut());
        float fadeDuration = fadeOutEffect.fadeDuration + fadeOutEffect.delayBeforeFade;
        yield return new WaitForSeconds(fadeDuration);
        Assert.That(fadeImage.color, Is.EqualTo(new Color(0f, 0f, 0f, 0f)).Within(0.01f));
    }
    
}