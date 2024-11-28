using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 2.0f;
    private string prefabPath = "UI/Canvas";
    private Canvas canvas;
    [SerializeField]
    private Vector3 canvasOffset = new Vector3(0.2f, -0.5f, 0f);
    public GameObject aiguille;
    public GameObject cle;
    private bool hasRun = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Si le joueur n'est pas assign� manuellement dans l'inspecteur, trouvez-le automatiquement
        GameObject canvaToAdd = Resources.Load<GameObject>(prefabPath);
        GameObject newObject = Instantiate(canvaToAdd, transform.position, transform.rotation);
        newObject.transform.SetParent(transform);
        cle.SetActive(false);
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.AddComponent<LookAtCam>();
        canvas.transform.position = transform.position + canvasOffset;
        GetComponent<Animator>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Inventory inventory = FindFirstObjectByType <Inventory>();
        if(!hasRun){
            if(distance <= activationDistance){
                canvas.enabled = true;
                if (Input.GetKeyDown("e") && inventory.IsSelectingItem("aiguille")){
                    GetComponent<Animator>().enabled = true;
                    inventory.UseItem("aiguille");
                    aiguille.SetActive(true);
                    hasRun = true;
                }
            }
            else{
                canvas.enabled = false;
            }
        }else{
            canvas.enabled = false;
            cle.SetActive(true);
        }
        

        
        
    }
}
