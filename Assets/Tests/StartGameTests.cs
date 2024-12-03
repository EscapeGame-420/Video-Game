using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class StartGameTest
{
    private GameObject gameObject;
    private StartGame startGameScript;
    private Button testButton;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        startGameScript = gameObject.AddComponent<StartGame>();
        testButton = new GameObject().AddComponent<Button>();
        startGameScript.button = testButton;
        SceneManager.LoadScene("MenuPrincipale", LoadSceneMode.Single);
    }

    [TearDown]
    public void TearDown()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
        }

        if (testButton != null)
        {
            GameObject.Destroy(testButton.gameObject);
        }
    }

    [Test]
    public void InitializeButtonTest()
    {
        Assert.IsNotNull(startGameScript.button);
    }

    [UnityTest]
    public IEnumerator ChangeSceneTest()
    {
        bool sceneChanged = false;
        testButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level1");
            sceneChanged = true;
        });

        testButton.onClick.Invoke();
        yield return null;

        Assert.IsTrue(sceneChanged);
    }
}
