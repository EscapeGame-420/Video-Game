using UnityEngine;

public class VisibilityChecker : MonoBehaviour
{
    [SerializeField] private GameObject elementToCheck;
    [SerializeField] private string deleteOrShow;

    void Start()
    {
    }

    void Update()
    {

        Vector3 goscreen = Camera.main.WorldToScreenPoint(elementToCheck.transform.position);
        Debug.Log("GoPos " + goscreen);

        float distX = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f,0f));
        Debug.Log("distX " + distX);

        float distY = Vector3.Distance(new Vector3(0f, Screen.height / 2, 0f), new Vector3(0f, goscreen.y, 0f));
        Debug.Log("distY " + distY);

        if((distX > Screen.width || distY > Screen.height) && ShadowManConvo.isConvoFinished)
        {
            if(deleteOrShow == "delete")
            {
                Destroy(elementToCheck);
            }
            else if(deleteOrShow == "show")
            {
                elementToCheck.SetActive(true);
            }
        }
    }
}
