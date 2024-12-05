using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    public static float lockopened = 0;
    private Transform canvasTransform; // Reference to the created canvas
    public Animator animatorDoor;
    public BoxCollider box;

    public AudioClip keyUnlockSound; // Sound for unlocking with a key
    public AudioClip doorOpenSound;  // Sound for when game finishes (door opens)
    private AudioSource audioSource;

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.Find("Julie").transform;

        }
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (!(distance <= activationDistance && lockopened == 4)){ 
            return;
        }

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

            //Debug.LogError("too far");

        if (Input.GetKeyDown("e"))
        {
            if (canvasTransform == null){
                // Find the canvas dynamically if it hasn't been assigned
                try{

                foreach (Transform child in transform)
                {
                    if (child.name.Contains("Canvas"))
                    {
                        canvasTransform = child;
                        break;
                    }
                }
                }catch{
                    Debug.LogError("Canvas not found");
                }
                Destroy(canvasTransform.gameObject);
            }
            animatorDoor.SetBool("IsIdle", true);
            box.enabled = false;  

            audioSource.PlayOneShot(doorOpenSound);
        }
        }
        public void OpenLock()
        {
            lockopened = lockopened + 1;

            audioSource.PlayOneShot(keyUnlockSound);
            //Debug.LogError("Lock opened: " + lockopened);
        }
        }
    
