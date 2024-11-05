using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testJump : MonoBehaviour
{
    // code from https://www.youtube.com/watch?v=vdOFUFMiPDU
    public Rigidbody rb;
    public LayerMask groundLayers;
    public float jumpForce = 10f;
    public CapsuleCollider col;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded(){
        return Physics.CheckCapsule(col.bounds.center, 
        new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
