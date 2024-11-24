using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
<<<<<<< Updated upstream
=======
    public GameObject obstacle;
    
>>>>>>> Stashed changes

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
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
        if (!(distance <= activationDistance && inventory.IncludeItem("crowbar"))) return;

        // Check if the canvas has already been created
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
            Transform canvasTransform = transform.Find("woodPlankCanvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(1.67f, 2.37f, -0.5f); // Set the desired position
            }
        }

        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && inventory.IsSelectingItem("crowbar"))
        {
            Destroy(gameObject);
            inventory.UseItem("crowbar");
        }
    }
}