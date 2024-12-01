using NUnit.Framework;
using UnityEngine;

public class AITests
{
    private GameObject npcObject;
    private AI ai;
    private AudioSource audioSource;
    private AudioClip testGrowlClip;
    private AudioClip testFootstepClip;

    [SetUp]
    public void Setup()
    {
        npcObject = new GameObject();
        ai = npcObject.AddComponent<AI>();
        audioSource = npcObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f;

        testGrowlClip = AudioClip.Create("Growl", 1, 1, 44100, false);
        ai.sndGrowl = testGrowlClip;
        ai.audioSource = audioSource;

        testFootstepClip = AudioClip.Create("Footstep", 1, 1, 44100, false);
        ai.sndFootstepLeft = testFootstepClip;
        ai.sndFootstepRight = testFootstepClip;
    }

    [Test]
    public void PlayGrowlSoundTest()
    {
        ai.PlayGrowlSound();
        float delayTime = 0.1f;
        new WaitForSeconds(delayTime);
        Assert.IsTrue(audioSource.isPlaying);
    }

    [Test]
    public void StopFootStepTest()
    {
        audioSource.PlayOneShot(testFootstepClip);
        Assert.IsTrue(audioSource.isPlaying);
        ai.StopFootStep();
        Assert.IsFalse(audioSource.isPlaying);
    }

    [Test]
    public void ResetAttackSoundTest()
    {
        ai.attackSoundPlayed = true;
        ai.ResetAttackSound();
        Assert.IsFalse(ai.attackSoundPlayed);
    }

    [Test]
    public void PlayFootStepTest()
    {
        ai.nextStepTime = Time.time - ai.stepCooldown;
        ai.PlayFootStep();
        Assert.IsTrue(audioSource.isPlaying);
    }
}
