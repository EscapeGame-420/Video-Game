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
        transform.LookAt(player.position + player.forward * 10f);
    }
}
