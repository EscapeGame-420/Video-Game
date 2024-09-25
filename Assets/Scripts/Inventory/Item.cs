using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] 
    private Inventory inventory;

    private void itemPickup(Collision other) {
        if (other.collider.CompareTag("Player")) {
            inventory.AddItem(gameObject);
            Debug.Log("Item picked up");
            Destroy(gameObject);
        }
    }

    private void UIKey  () {
        
    }
}
