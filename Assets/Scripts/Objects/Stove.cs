using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    public Sprite spritetorch;
    private Transform canvasTransform; // Reference to the created canvas
    public GameObject fireparticleEffect;
    public string itemName;
    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.Find("Julie").transform;
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            fireparticleEffect = GameObject.Find("stoveFire");
            fireparticleEffect.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (!(distance <= activationDistance && inventory.IncludeItemName("torch"))){
            fireparticleEffect.SetActive(false);
        return;
        }else{
            fireparticleEffect.SetActive(true);
        }

        // Check if the canvas has already been created
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
             canvasTransform = transform.Find("stoveCanvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(0,0.72f, 0.56f); // Set the desired position
            }
        }
        
        
        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && inventory.IsSelectingItem("torch"))
        {
            inventory.UseItem("torch");
            Item item = new Item // Create NEW instance per slot
            {
                itemName = itemName,
                sprite = spritetorch
            };
            inventory.AddItem(item);
            Destroy(canvasTransform.gameObject);
        
        }
    }
}