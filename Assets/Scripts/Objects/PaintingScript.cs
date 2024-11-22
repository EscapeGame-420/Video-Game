using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 7.0f;
    public bool isCandleNear = false;
    // Start is called before the first frame update
    void Start()
    {
        // Si le joueur n'est pas assign� manuellement dans l'inspecteur, trouvez-le automatiquement
        if (player == null)
        {
             player = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType <Inventory>();
        if (!(distance <= activationDistance && inventory.IncludeItem("greenFlameCandle") && !GetComponent<Animator>().enabled) ) return;

        //if(!transform.Find("PaintingCanvas")) Item.CreateCanvas(this.gameObject);
        //GetComponent<Animator>().enabled = true;
        Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");

        if (inventory.IsSelectingItem("greenFlameCandle")){
            GetComponent<Animator>().enabled = true;
            inventory.UseItem("greenFlameCandle");
        }
        
    }
}
