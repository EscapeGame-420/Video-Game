using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulieMovement : MonoBehaviour
{
    Animator julieAnimControl;

    float axisH, axisV;
    List<string> animationParamControls = new List<string> { "isWalking", "isWalkingBackward" };

    private void Awake()
    {
        julieAnimControl = GetComponent<Animator>();
        julieAnimControl.SetBool("isIdle", true);
    }
    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");

        // code répétitif, doit changer ou créer fonction
        if(axisV > 0)
        {
            transform.Translate(Vector3.forward * 2f * axisV * Time.deltaTime);
            foreach(string param in animationParamControls)
            {
                julieAnimControl.SetBool(param, param.Equals("isWalking"));
            }
        }

        else if(axisV < 0)
        {
            transform.Translate(Vector3.forward * 2f * axisV * Time.deltaTime);
            foreach (string param in animationParamControls)
            {
                julieAnimControl.SetBool(param, param.Equals("isWalkingBackward"));
            }
        }

        else
        {
            foreach (string param in animationParamControls)
            {
                julieAnimControl.SetBool(param, param.Equals("isIdle"));
            }
        }
    }
}
