using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulieVisionFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if(offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + player.rotation * offset;
        transform.position = desiredPosition;
        transform.LookAt(player.position + player.forward * 20f);



        // YAHYA: J'ai fait un petit code ici c'est un début pour le mouvement de la souris mais ca marche pas trop bien. pour Aya
        //float mouseX = Input.GetAxis("Mouse X") * 100.0f * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * 100.0f * Time.deltaTime;

        //transform.Rotate(Vector3.up, mouseX);
        //transform.Rotate(Vector3.ri);

    }
}
