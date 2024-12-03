using UnityEngine;
using UnityEngine.UI;

public class PianoController : MonoBehaviour
{
    public GameObject canvasToOpen;  
    public GameObject promptCanvas;  
    public KeyCode keyToPress = KeyCode.E;  
    public MonoBehaviour cameraController; 
    public Transform player; 
    public Transform target;  
    public float activationDistance = 2f;

    public GameObject pianoKey;  

    
    public Button[] pianoButtons;  
    public int currentButtonIndex = 0;  

    public bool isCanvasActive = false;  

    void Start()
    {
      
        pianoKey.SetActive(false);

        for (int i = 0; i < pianoButtons.Length; i++)
        {
            int buttonIndex = i;  
            pianoButtons[i].onClick.AddListener(() => OnPianoButtonClick(buttonIndex));
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, target.position);

        
        if (distance <= activationDistance)
        {
            promptCanvas.SetActive(true);  

           
            if (Input.GetKeyDown(keyToPress))
            {
                ToggleCanvas(); 
            }
        }
        else
        {
            promptCanvas.SetActive(false);  
        }
    }

    public void ToggleCanvas()
    {
        if (!isCanvasActive)
        {
            canvasToOpen.SetActive(true);  
            pianoKey.SetActive(false);  

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            try{
                cameraController.enabled = false;  
            }
            catch{
                Debug.Log("No camera controller found");
            }
        }
        else
        {
            canvasToOpen.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            try{
                cameraController.enabled = true;  
            }
            catch{
                Debug.Log("No camera controller found");
                }
            }

      
        isCanvasActive = !isCanvasActive;
    }

  
    void OnPianoButtonClick(int buttonIndex)
    {
      
        if (buttonIndex == currentButtonIndex)
        {
            currentButtonIndex++;  

            if (currentButtonIndex == pianoButtons.Length)
            {
               
                pianoKey.SetActive(true);
                CloseCanvas();  
            }
        }
        else
        {
          
            currentButtonIndex = 0; 
            pianoKey.SetActive(false);  
            CloseCanvas();  
        }
    }

    public void CloseCanvas()
    {
        canvasToOpen.SetActive(false);  
        isCanvasActive = false; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        try{
            cameraController.enabled = true;  
        }
        catch{
            Debug.Log("No camera controller found");
        }
    }
}