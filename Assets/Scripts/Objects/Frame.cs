using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 1.5f;
    public bool canvasCreated = false;
    public GameObject inFrame; 
    public GameObject key;

    public void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void FixedUpdate()
    {
        if (player == null) return;

        Inventory inventory = FindFirstObjectByType<Inventory>();
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && inventory.IncludeItemName("photo"))
        {
            if (!canvasCreated)
            {
                Item.CreateCanvas(this.gameObject);

                Transform canvasTransform = transform.Find("frameCanvas");
                if (canvasTransform != null)
                {
                    canvasTransform.localPosition = new Vector3(0.01f, 0.12f, 0.07f);
                    canvasTransform.gameObject.SetActive(true);
                }

                canvasCreated = true;
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

        if (Input.GetKeyDown(KeyCode.E) && inventory.IsSelectingItem("photo"))
        {
            inFrame.SetActive(true); 
            inventory.UseItem("photo"); 
            key.SetActive(true); 

            Transform canvas = transform.Find("frameCanvas");
            if (canvas != null)
            {
                Destroy(canvas.gameObject);
            }

            canvasCreated = false;
        }
    }
}
