using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulieMovement : MonoBehaviour
{
    Animator julieAnimControl;
    AudioSource JulieAudioSource;

    public int sensitivity = 70;
    private float yRotation = 0f;

    [SerializeField] AudioClip sndLeftFoot, sndRightFoot;
    bool switchFoot = false;

    float axisH, axisV;
    List<string> animationParamControls = new List<string> { "isWalking", "isWalkingBackward", "isWalkingLeft", "isWalkingRight", "isIdle" };

    private void Awake()
    {
        julieAnimControl = GetComponent<Animator>();
        julieAnimControl.SetBool("isIdle", true);

        JulieAudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");

        // pour avancer et reculer
        if(axisV != 0)
        {
            transform.Translate(Vector3.forward * 2f * axisV * Time.deltaTime);
            changeMovement(axisV > 0 ? "isWalking" : "isWalkingBackward");
        }

        if(axisH != 0)
        {
            transform.Translate(Vector3.right * 2f * axisH * Time.deltaTime);
            changeMovement(axisH > 0 ? "isWalkingRight" : "isWalkingLeft");
        }

        if(axisH == 0 && axisV == 0)
        {
            changeMovement("isIdle");
        }
    }

    public void PlayFootStep()
    {
        if (!JulieAudioSource.isPlaying)
        {
            switchFoot = !switchFoot;

            if (switchFoot)
            {
                JulieAudioSource.pitch = 2f;
                JulieAudioSource.PlayOneShot(sndLeftFoot);
            }
            else
            {
                JulieAudioSource.pitch = 2f;
                JulieAudioSource.PlayOneShot(sndRightFoot);
            }
        }
    }

    private void changeMovement(string movementToActivate){
        foreach(string param in animationParamControls)
        {
            julieAnimControl.SetBool(param, param.Equals(movementToActivate));
        }
    }
}
