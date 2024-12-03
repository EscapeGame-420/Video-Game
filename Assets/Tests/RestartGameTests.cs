using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;

public class RestartGameTests
{
    private RestartGame restartGame;
    private Button restartButton;
    private GameObject testObject;

    [SetUp]
    public void SetUp()
    {
        testObject = new GameObject();
        restartButton = testObject.AddComponent<Button>();
        restartGame = testObject.AddComponent<RestartGame>();

        restartGame.GetType().GetField("restartButton", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(restartGame, restartButton);
        SceneManager.LoadScene("Level1");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObject);
    }

    [UnityTest]
    public IEnumerator TestRestart()
    {
        restartButton.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual("Level1", SceneManager.GetActiveScene().name);
    }
}
