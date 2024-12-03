using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 1.5f;
    private bool canvasCreated = false;
    public GameObject inFrame; 
    public GameObject key;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (distance <= activationDistance && inventory.IncludeItemName("photo"))
        {
            if (!canvasCreated)
            {
                Item.CreateCanvas(this.gameObject);
                canvasCreated = true;

                Transform canvasTransform = transform.Find("frameCanvas");
                if (canvasTransform != null)
                {
                    canvasTransform.localPosition = new Vector3(0.01f, 0.12f, 0.07f); 
                    canvasTransform.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (canvasCreated)
            {
                Transform canvasTransform = transform.Find("frameCanvas");
                if (canvasTransform != null)
                {
                    Destroy(canvasTransform.gameObject);
                }
                canvasCreated = false;
            }
        }

        //Debug.Log("Le joueur s'approche avec la photo. Activation du tableau");

        if (Input.GetKeyDown(KeyCode.E) && inventory.IsSelectingItem("photo") && distance <= activationDistance)
        {
            inFrame.SetActive(true); 
            inventory.UseItem("photo"); 
            key.SetActive(true); 

            Transform canvasTransform = transform.Find("frameCanvas");
            if (canvasTransform != null)
            {
                Destroy(canvasTransform.gameObject);
            }
        }
    }
}
