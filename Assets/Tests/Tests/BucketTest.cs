using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

public class BucketTest
{
    private GameObject bucketObject;
    private Bucket bucket;
    private GameObject playerObject;
    private Inventory inventory;

    [SetUp]
    public void SetUp()
    {
        bucketObject = new GameObject("Bucket");
        bucket = bucketObject.AddComponent<Bucket>();

        playerObject = new GameObject("Player");
        bucket.player = playerObject.transform;
        bucket.mixItems = new List<string>();
        inventory = playerObject.AddComponent<Inventory>();

        bucket.goodeffect = new GameObject("GoodEffect");
        bucket.badeffect = new GameObject("BadEffect");
        bucket.goodeffect.SetActive(false);
        bucket.badeffect.SetActive(false);
        bucket.bucketObject = new GameObject("BucketObject");
        bucket.y = 2.37f;
        bucket.x = 1.67f;
        bucket.z = -0.5f;
        bucket.itemName = "item";
        bucket.itemName1 = "item1";
    }

        [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(bucketObject);
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void TestCanNotActivateCanvas()
    {
        inventory.Init();
        Item itemtoadd = new Item { itemName = "eyeball" };
        inventory.AddItem(itemtoadd);
        Assert.IsTrue(bucket.CanNotActivateCanvas(8.0f, inventory));
        Assert.IsFalse(bucket.CanNotActivateCanvas(6.0f, inventory));
    }

    [Test]
    public void TestHandleCanvasCreation()
    {
        bucket.HandleCanvasCreation();
        Assert.IsTrue(bucket.canvasCreated);
        Assert.IsNotNull(bucket.canvasTransform);
        Assert.AreEqual(new Vector3(bucket.x, bucket.y, bucket.z), bucket.canvasTransform.localPosition);
    }

    [Test]
    public void TestCheckStatus()
    {
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("eyeball");
        bucket.mixItems.Add("eyeball");

        bucket.CheckStatus();

        Assert.IsTrue(bucket.goodeffect.activeSelf);
        Assert.IsFalse(bucket.badeffect.activeSelf);

        bucket.mixItems.Clear();
        bucket.goodeffect.SetActive(false);
        bucket.badeffect.SetActive(false);
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("eyeball");

        bucket.CheckStatus();
        
        Assert.IsFalse(bucket.goodeffect.activeSelf);
        Assert.IsTrue(bucket.badeffect.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestHandleInteraction()
    {
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("eyeball");
        bucket.mixItems.Add("eyeball");

        bucket.HandleInteraction(inventory);

        yield return null;

        Assert.IsTrue(inventory.IncludeItemName(bucket.itemName));
        Assert.IsFalse(bucket.bucketObject.activeSelf);

        bucket.bucketObject.SetActive(true);
        bucket.mixItems.Clear();
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("bones");
        bucket.mixItems.Add("eyeball");

        bucket.HandleInteraction(inventory);

        yield return null;

        Assert.IsTrue(inventory.IncludeItemName(bucket.itemName1));
        Assert.IsFalse(bucket.bucketObject.activeSelf);
    }
}