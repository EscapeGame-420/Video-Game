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
    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            
            if (distance <= activationDistance && Inventory.items.Contains("greenFlameCandle") && !GetComponent<Animator>().enabled)
            {
                GetComponent<Animator>().enabled = true;
                Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");
            }
            // if (distance <= activationDistance && isCandleNear)
            // {
            //     Debug.Log("Le joueur s'approche avec la bougie. Activation du tableau");
                
            // }
            // else if (distance <= activationDistance && !isCandleNear)
            // {
            //     Debug.Log("Le joueur s'approche sans la bougie");
            // }
        }
    }
}
