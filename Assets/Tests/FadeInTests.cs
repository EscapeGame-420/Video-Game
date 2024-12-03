using NUnit.Framework;
using UnityEngine;
using TMPro;

public class FadeInTests
{
    private FadeIn fadeIn;
    private TextMeshProUGUI youDiedText;
    private TextMeshProUGUI restartText;
    private TextMeshProUGUI backToMenuText;

    [SetUp]
    public void SetUp()
    {
        GameObject testObject = new GameObject();
        fadeIn = testObject.AddComponent<FadeIn>();
        youDiedText = new GameObject().AddComponent<TextMeshProUGUI>();
        restartText = new GameObject().AddComponent<TextMeshProUGUI>();
        backToMenuText = new GameObject().AddComponent<TextMeshProUGUI>();
        fadeIn.youDiedText = youDiedText;
        fadeIn.restartText = restartText;
        fadeIn.backToMenuText = backToMenuText;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(fadeIn.gameObject);
        Object.DestroyImmediate(youDiedText.gameObject);
        Object.DestroyImmediate(restartText.gameObject);
        Object.DestroyImmediate(backToMenuText.gameObject);
    }

    [Test]
    public void TestInitializeTextAlpha()
    {
        fadeIn.InitializeTextAlpha();
        
        Assert.AreEqual(0f, youDiedText.color.a);
        Assert.AreEqual(0f, restartText.color.a);
        Assert.AreEqual(0f, backToMenuText.color.a);
    }
}
