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
        visibilityChecker.elementThatChangeVisibility = elementThatChangesVisibility;
        visibilityChecker.deleteOrShow = "delete";

        Assert.IsNotNull(elementThatChangesVisibility);

        cameraObject.transform.position = new Vector3(0, 1080, 0);
        // cameraObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        visibilityChecker.CheckVisibility();

        //Object.Destroy(elementThatChangesVisibility);
        yield return null;

        try
        {
            //Assert.IsNull(elementThatChangesVisibility);
            Assert.IsTrue(elementThatChangesVisibility == null);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An error occurred: {ex.Message}");
            throw;
        }
        
    }
}