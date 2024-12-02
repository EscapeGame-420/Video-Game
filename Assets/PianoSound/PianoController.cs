using UnityEngine;

public class PianoController : MonoBehaviour
{
    public GameObject canvasToOpen; // The main Canvas to interact with
    public GameObject promptCanvas; // The small prompt Canvas
    public KeyCode keyToPress = KeyCode.E; // Key to interact
    public MonoBehaviour cameraController; // Script that controls the camera
    public Transform player; // Player or camera transform
    public Transform target; // Target object
    public float activationDistance = 2f; 

    void Update()
    {
        // Calculate the distance between the player and the target
        float distance = Vector3.Distance(player.position, target.position);

        // Show or hide the prompt based on distance
        if (distance <= activationDistance)
        {
            promptCanvas.SetActive(true); // Show the prompt

            // Check if the key is pressed and toggle the main Canvas
            if (Input.GetKeyDown(keyToPress))
            {
                bool isCanvasActive = !canvasToOpen.activeSelf;
                canvasToOpen.SetActive(isCanvasActive);

                // Handle cursor and camera control
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
            promptCanvas.SetActive(false); // Hide the prompt
        }
    }
}
