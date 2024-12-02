using UnityEngine;

public class PianoController : MonoBehaviour
{
    public GameObject canvasToOpen; 
    public GameObject promptCanvas; 
    public KeyCode keyToPress = KeyCode.E; 
    public MonoBehaviour cameraController; 
    public Transform player; 
    public Transform target; 
    public float activationDistance = 2f; 

    void Update()
    {
        float distance = Vector3.Distance(player.position, target.position);

        if (distance <= activationDistance)
        {
            promptCanvas.SetActive(true); 

            
            if (Input.GetKeyDown(keyToPress))
            {
                bool isCanvasActive = !canvasToOpen.activeSelf;
                canvasToOpen.SetActive(isCanvasActive);

                if (isCanvasActive)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    cameraController.enabled = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    cameraController.enabled = true;
                }
            }
        }
        else
        {
            promptCanvas.SetActive(false); 
        }
    }
}
