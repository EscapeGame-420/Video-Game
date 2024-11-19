using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class bucket : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    private bool canvasCreated = false; // Flag to track if the canvas has been created
    public float x =0;
    public float y =0;
    public float z =0;
    public Sprite sprite;
    public string itemName;
    public Sprite sprite1;
    public string itemName1;
    private Transform canvasTransform; 
    public List<string> mixItems;
    public GameObject bucketObject;


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
        // Check if the canvas has already been created
        if (!canvasCreated)
        {
            Item.CreateCanvas(this.gameObject);
            canvasCreated = true; // Set the flag to true after creating the canvas

            // Find the created canvas and set its position
            canvasTransform = transform.Find(this.gameObject.name + "Canvas");
            if (canvasTransform != null)
            {
                canvasTransform.localPosition = new Vector3(x, y, z); // Set the desired position
            }
        }

        if (!(distance <= activationDistance))
        {
            canvasTransform.gameObject.SetActive(false);
            return;
        }
        else
        {
            canvasTransform.gameObject.SetActive(true);
        }

        Debug.Log("Le joueur s'approche");

        if (Input.GetKeyDown("e"))
        {
            if (mixItems.Count == 3)
            {
                mixItems.Sort();
                switch (string.Join(",", mixItems))
                {
                    case "bones,eyeball,eyeball":
                        Item item = new Item
                        {
                            itemName = itemName,
                            sprite = sprite
                        };
                        inventory.AddItem(item);
                        bucketObject.SetActive(false);
                        break;
                    default:
                        Item defaultItem = new Item
                        {
                            itemName = itemName1,
                            sprite = sprite1
                        };
                        inventory.AddItem(defaultItem);
                        bucketObject.SetActive(false);
                        break;
                }
            }
            else
            {
                if (inventory.IncludeItem("bones") && inventory.IsSelectingItem("bones"))
                {
                    inventory.UseItem("bones");
                    mixItems.Add("bones");
                }
                else if (inventory.IncludeItem("eyeball") && inventory.IsSelectingItem("eyeball"))
                {
                    inventory.UseItem("eyeball");
                    mixItems.Add("eyeball");
                }
            }
        }
    }
}