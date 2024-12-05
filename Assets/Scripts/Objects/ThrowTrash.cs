using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrash : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    private Transform canvasTransform; // Reference to the created canvas
    public GameObject bucket;
    public string itemName;
    private Bucket bucketScript;
    public GameObject badeffect;

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.Find("Julie").transform;
            if (player == null)
            {
                GameObject playerObject = GameObject.FindWithTag("Player");
            }
            bucket = GameObject.Find("bucket");
            bucketScript = GameObject.Find("bucketScript").GetComponent<Bucket>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
             canvasTransform = transform.Find(this.gameObject.name +"Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(0,0, 0); // Set the desired position
            }
        }
        if (!(distance <= activationDistance && inventory.IncludeItemName(itemName))){
            canvasTransform.gameObject.SetActive(false);
        return;
        }else{
            canvasTransform.gameObject.SetActive(true);
        }

        // Check if the canvas has already been created
        
        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && inventory.IsSelectingItem(itemName))
        {
            inventory.UseItem(itemName);
            bucketScript.mixItems = new List<string>();
            bucket.SetActive(true);
            badeffect.SetActive(false);
        }
    }
}