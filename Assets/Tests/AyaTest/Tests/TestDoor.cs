using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
 
public class DoorControllerNassimRoomTests
{
    private GameObject doorObject;
    private GameObject playerObject;
    private DoorControllerNassimRoom doorController;
 
    [SetUp]
    public void SetUp()
    {
        doorObject = new GameObject("Door");
        playerObject = new GameObject("Player");
       
        doorController = doorObject.AddComponent<DoorControllerNassimRoom>();
       
       
        doorObject.transform.position = new Vector3(0, 0, 0);
        playerObject.transform.position = new Vector3(3, 0, 0);  
 
        doorController.interactableObject = playerObject.transform;
        doorController.openRot = 90f;
    }
 
    [TearDown]
    public void TearDown()
    {
        Object.Destroy(doorObject);
        Object.Destroy(playerObject);
    }

    [Test]
    public void TestToggleDoor_WhenPlayerIsInRange_DoorOpens()
    {
        doorController.opening = false;
       
       
        Input.GetKeyDown(KeyCode.E);
       
       
        doorController.ToggleDoor();
        Assert.IsTrue(doorController.opening, "La porte devrait être ouverte.");
    }
}