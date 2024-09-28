using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulieVisionFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public int sensitivity = 30;
    private float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if(offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 desiredPosition = player.position + player.rotation  * offset;
        transform.position = desiredPosition;
        transform.LookAt(player.position + player.forward * 20f);



        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10f, 25f);

        transform.localRotation = Quaternion.Euler(xRotation, player.eulerAngles.y, 0f);

    }

    
}
