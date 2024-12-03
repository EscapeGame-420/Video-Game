using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ItemTest : MonoBehaviour
{
    private GameObject itemObject;
    private Item item;
    private GameObject player;
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(itemObject);
        Object.DestroyImmediate(player);
    }
    [SetUp]
    public void SetUp()
    {
        itemObject = new GameObject("item");
        item = itemObject.AddComponent<Item>();

        itemObject.transform.position = Vector3.one;

    }
    [Test]
    public void CreateCanvasTest()
    {
        Canvas canvas = Item.CreateCanvas(itemObject);

        Assert.IsNotNull(canvas);

        Assert.IsNotNull(canvas.GetComponent<RectTransform>());
        Assert.IsNotNull(canvas.GetComponent<LookAtCam>());
    }
}