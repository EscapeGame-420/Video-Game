using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerLivingRoom : MonoBehaviour
{
   public GameObject door;  
    public float openRot = 90f;  
    public float closeRot = 0f;  
    public float speed = 2f;  
    public float interactionDistance = 5.0f;  
    public bool opening = false;  
    public Transform interactableObject; 

    public AudioClip doorSound;   // sound door opens or closes
    private AudioSource audioSource;
 
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (interactableObject != null)
        {
            float distance = Vector3.Distance(interactableObject.position, door.transform.position);

            //Debug.Log("Distance to door: " + distance);
            //Debug.Log("Player Position: " + interactableObject.position);  
            //Debug.Log("Door Position: " + door.transform.position);  

            if (distance <= interactionDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleDoor(); 
                    //Debug.Log("Door state changed!");  
                }
            }
            else
            {
                //Debug.Log("You are too far from the door to interact.");  
            }
        }
        else
        {
            //Debug.LogWarning("Interactable object is not assigned!");
        }

        Vector3 currentRot = door.transform.localEulerAngles;
        float targetRotY = opening ? openRot : closeRot;

        door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, targetRotY, currentRot.z), speed * Time.deltaTime);
    }

    public void ToggleDoor()
    {
        opening = !opening; 

        audioSource.PlayOneShot(doorSound); 
    }
}
