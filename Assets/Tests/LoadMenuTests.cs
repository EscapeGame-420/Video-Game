using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class LoadMenuSceneTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("MenuPrincipale");
    }

    [TearDown]
    public void TearDown()
    {
        SceneManager.LoadScene("MenuPrincipale");
    }

    [UnityTest]
    public IEnumerator TestLoadMenuTests()
    {
        var loadMenuScene = new GameObject().AddComponent<LoadMenuScene>();
        loadMenuScene.LoadMenu();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual("MenuPrincipale", SceneManager.GetActiveScene().name);
    }
}
