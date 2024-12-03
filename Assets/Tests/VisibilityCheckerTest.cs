using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VisibilityCheckerTest
{
    public VisibilityChecker visibilityChecker;
    public GameObject cameraObject;
    public Camera testCamera;
    public GameObject elementThatChangesVisibility;
    public GameObject player;

    public string deleteOrShow;

    public GameObject candle;
    public GameObject candleShower;
    public VisibilityChecker visibilityCheckerToShow;


    [SetUp]
    public void Setup()
    {
        player = new GameObject();

        cameraObject = new GameObject();
        cameraObject.tag = "MainCamera";
        testCamera = cameraObject.AddComponent<Camera>();
        visibilityChecker = cameraObject.AddComponent<VisibilityChecker>();

        elementThatChangesVisibility = new GameObject();
        elementThatChangesVisibility.transform.position = new Vector3(0, 0, 10);

        cameraObject.transform.position = new Vector3(0, 0, 0);
        cameraObject.transform.LookAt(elementThatChangesVisibility.transform);

        candle = new GameObject();
        candle.transform.position = new Vector3(0, 0, 10);
        candle.SetActive(false);
        candleShower = new GameObject();
        visibilityCheckerToShow = candleShower.AddComponent<VisibilityChecker>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testCamera.gameObject);
        Object.DestroyImmediate(elementThatChangesVisibility);
    }

    [UnityTest]
    public IEnumerator VisibilityCheckerTestCheckVisibility()
    {
        // partie qui rend invisible l'objet
        visibilityChecker.elementThatChangeVisibility = elementThatChangesVisibility;
        visibilityChecker.deleteOrShow = "delete";
        ShadowManConvo.isConvoFinished = true;

        // partie qui rend visible l'objet
        visibilityCheckerToShow.elementThatChangeVisibility = candle;
        visibilityCheckerToShow.deleteOrShow = "show";

        Assert.IsNotNull(elementThatChangesVisibility);
        Assert.IsTrue(candle.activeSelf == false);

        cameraObject.transform.position = new Vector3(0, 1080, 0);

        visibilityChecker.CheckVisibility();
        visibilityCheckerToShow.CheckVisibility();

        yield return null;

        Assert.IsTrue(elementThatChangesVisibility == null);
        Assert.IsTrue(candle.activeSelf);
    }
}