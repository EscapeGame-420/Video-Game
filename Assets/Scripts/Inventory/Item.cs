using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
