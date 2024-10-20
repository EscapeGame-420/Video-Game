using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private Inventory inventory;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float activationDistance = 1.5f;
    [SerializeField]
    private Canvas canvas;

    
    void Start()
    {
        createCanvas(this.gameObject);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance){
            canvas.enabled = true;
            if (Input.GetKeyDown("e")){
                inventory.AddItem(gameObject.name);
                Debug.Log("Item picked up");
                Destroy(gameObject);
            }
        }
        else{
            canvas.enabled = false;
        }
    }

    private void UIKey  () {
        
    }

    private void createCanvas(GameObject itemObject)
    {
        // Create a new canvas object
        GameObject canvasObject = new GameObject("testCanvas");
        canvasObject.AddComponent<Canvas>();
        canvasObject.transform.position = itemObject.transform.position;

        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
        canvasRectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        canvasRectTransform.sizeDelta = new Vector2(1101, 514);
        canvasRectTransform.anchorMin = new Vector2(0, 0);
        canvasRectTransform.anchorMax = new Vector2(0, 0);
        canvasObject.AddComponent<LookAtCam>();
        canvasObject.transform.SetParent(itemObject.transform, true);


        // Create a new text object
        GameObject textObject = new GameObject("GrabText");
        RectTransform textRectTransform = textObject.AddComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(50, 50);
        textRectTransform.position = new Vector3(0, 65.5f, 0);

        textObject.AddComponent<CanvasRenderer>();
        TextMeshProUGUI grabText = textObject.AddComponent<TextMeshProUGUI>();
        grabText.text = "E";
        grabText.fontSize = 60;
        grabText.color = Color.black;
        
        textObject.transform.SetParent(canvasObject.transform, false);

        // Create a new Image object
        GameObject imageObject = new GameObject("GrabBackground");
        textObject.AddComponent<CanvasRenderer>();
        Image image = imageObject.AddComponent<Image>();
        image.color = Color.white;
        imageObject.transform.SetParent(canvasObject.transform, false);

    }
}
