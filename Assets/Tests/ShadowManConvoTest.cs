using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class ShadowManConvoTest
{

    public ShadowManConvo shadowManConvo;
    public GameObject player;

    public GameObject sittingMan;

    public Canvas canvas;
    public TextMeshPro FToTalk;


    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        sittingMan = new GameObject();

        shadowManConvo = sittingMan.AddComponent<ShadowManConvo>();

        canvas = new GameObject().AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        FToTalk = new GameObject().AddComponent<TextMeshPro>();
        FToTalk.transform.SetParent(canvas.transform);

        shadowManConvo.FToTalk = FToTalk;
        shadowManConvo.player = player;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ShadowManConvoTest_InitializeMessages()
    {
        Assert.IsNull(shadowManConvo.messagesJulie);
        Assert.IsNull(shadowManConvo.messagesHomme);

        shadowManConvo.initializeMessages();

        Assert.IsNotNull(shadowManConvo.messagesJulie);
        Assert.IsNotNull(shadowManConvo.messagesHomme);
    }
}
