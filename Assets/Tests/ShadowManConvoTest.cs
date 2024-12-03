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
    public GameObject placeToStay;

    public GameObject sittingMan;

    public Canvas canvas;
    public TextMeshPro FToTalk;

    public GameObject continuePanel;

    public TextMeshPro manConvo;
    public TMP_Text JulieConvo;


    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        sittingMan = new GameObject();
        placeToStay = new GameObject();

        shadowManConvo = sittingMan.AddComponent<ShadowManConvo>();

        placeToStay.transform.position = new Vector3(100, 0, 100);
        player.transform.position = new Vector3(200, 0, 200);

        canvas = new GameObject().AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        FToTalk = new GameObject().AddComponent<TextMeshPro>();
        FToTalk.transform.SetParent(canvas.transform);

        shadowManConvo.FToTalk = FToTalk;
        shadowManConvo.player = player;

        continuePanel = new GameObject();
        continuePanel.SetActive(false);

        manConvo = new GameObject().AddComponent<TextMeshPro>();
        JulieConvo = new GameObject().AddComponent<TextMeshPro>();

        player.AddComponent<Animator>();

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

    [UnityTest]
    public IEnumerator ShadowManConvoTest_ShowNextMessage()
    {
        shadowManConvo.manConvo = manConvo;
        shadowManConvo.JulieConvo = JulieConvo;
        shadowManConvo.isManSpeaking = false;
        shadowManConvo.initializeMessages();
        shadowManConvo.currentManMessageIndex = 0;
        shadowManConvo.currentJulieMessageIndex = 0;
        shadowManConvo.player = player;

        shadowManConvo.showNextMessage();
        yield return null;
        Assert.AreEqual(JulieConvo.text, shadowManConvo.messagesJulie[shadowManConvo.currentJulieMessageIndex - 1]);

        shadowManConvo.showNextMessage();
        yield return null;
        Assert.AreEqual(manConvo.text, shadowManConvo.messagesHomme[shadowManConvo.currentManMessageIndex - 1]);


        shadowManConvo.currentManMessageIndex = shadowManConvo.messagesHomme.Count;
        shadowManConvo.currentJulieMessageIndex = shadowManConvo.messagesJulie.Count;
        shadowManConvo.showNextMessage();
        Assert.IsTrue(player.GetComponent<Animator>().enabled);
    }

    [UnityTest]
    public IEnumerator ShadowManConvoTest_DisablePlayerMovement()
    {
        shadowManConvo.player = player;
        shadowManConvo.PlaceToStay = placeToStay;
        shadowManConvo.disablePlayerMovement();
        yield return null;
        Assert.IsFalse(player.GetComponent<Animator>().enabled);
    }
}
