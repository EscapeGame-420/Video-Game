using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 2.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false;
    private Transform canvasTransform;
    public string itemName;
    public string itemName1;
    public GameObject barrier;
    public GameObject flames;
    public GameObject steaming;
    bool isGassed = false;
    public GameObject shelf;
    public GameObject shelf1;

    void Start()
    {
        // If the player is not assigned manually in the inspector, find it automatically
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            barrier = GameObject.Find("Barrier");
            flames = GameObject.Find("Flames");
            steaming = GameObject.Find("Gas");
            shelf = GameObject.Find("Shelves");
            shelf1 = GameObject.Find("Shelves (1)");
            flames.SetActive(false);
            steaming.SetActive(false);
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
            canvasTransform = transform.Find(this.gameObject.name + "Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(-0.69f, 2.174f, 0.5f); // Set the desired position
            }
        }
        if (!(distance <= activationDistance && inventory.IncludeItem(itemName) || (inventory.IncludeItem(itemName1) && isGassed)))
        {
            canvasTransform.gameObject.SetActive(false);
            return;
        }
        else
        {
            canvasTransform.gameObject.SetActive(true);
        }

        // Check if the canvas has already been created

        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (Input.GetKeyDown("e") && (inventory.IsSelectingItem(itemName) || inventory.IsSelectingItem(itemName1)))
        {
            switch (isGassed)
            {
                case false:
                    inventory.UseItem(itemName);
                    isGassed = true;
                    barrier.SetActive(false);
                    steaming.SetActive(true);
                    StartCoroutine(StartFlameTimer());
                    break;
                case true:
                    inventory.UseItem(itemName1);
                    flames.SetActive(true);
                    steaming.SetActive(false);
                    break;
            }


        }
    }
    IEnumerator StartFlameTimer()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(5.0f);

        // Delete the shelf
        if (shelf != null)
        {
            shelf.SetActive(false);
            shelf1.SetActive(false);
            flames.SetActive(false);
            steaming.SetActive(false);
        }
    }
}