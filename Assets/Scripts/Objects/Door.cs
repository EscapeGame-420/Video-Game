using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    Animator animator;
    public static float lockopened = 0;
    private Transform canvasTransform; // Reference to the created canvas
    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }

    void Update()
    {
        if (player == null) return;
        animator = GetComponent<Animator>();
        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (!(distance <= activationDistance && lockopened==3)) return;

        // Check if the canvas has already been created
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
            canvasTransform = transform.Find(this.gameObject.name+"Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(0,1,-0.3f); // Set the desired position
            }
        }

        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && lockopened== 3)
        {
            GetComponent<Animator>().enabled = true;
            Destroy(canvasTransform.gameObject);
    
        }
        }
        public void OpenLock()
        {
            lockopened = lockopened + 1;
        }
        }
    
