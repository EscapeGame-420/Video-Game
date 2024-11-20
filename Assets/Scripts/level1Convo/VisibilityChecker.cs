using UnityEngine;

public class VisibilityChecker : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        // objectRenderer = GetComponent<Renderer>();
        // if (objectRenderer == null)
        // {
        //     Debug.LogError("No Renderer found on this GameObject.");
        // }
    }

    void Update()
    {
        // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z);
        // Debug.Log("screenCenter "+screenCenter);

        // Vector3 screenHeight = new Vector3(Screen.width / 2, Screen.height, Camera.main.transform.position.z);
        // Debug.Log("screenHeight " + screenHeight);

        // Vector3 screenWidth = new Vector3(Screen.width, Screen.height/2, Camera.main.transform.position.z);
        // Debug.Log("screenWidth " + screenWidth);

        Vector3 goscreen = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("GoPos " + goscreen);

        float distX = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f,0f));
        Debug.Log("distX " + distX);

        float distY = Vector3.Distance(new Vector3(0f, Screen.height / 2, 0f), new Vector3(0f, goscreen.y, 0f));
        Debug.Log("distY " + distY);

        if((distX > Screen.width || distY > Screen.height) && ShadowManConvo.isConvoFinished)
        {
            Destroy(gameObject);
        }
    }
}
