using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private Inventory inventory;

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")) {
            inventory.AddItem(gameObject.name);
            Debug.Log("Item picked up");
            Destroy(gameObject);
        }
    }

    private void UIKey  () {
        
    }
}
