using UnityEngine;

public class VisibilityChecker : MonoBehaviour
{
    [SerializeField] public GameObject elementThatChangeVisibility;
    [SerializeField] public string deleteOrShow;

    void Start()
    {
    }

    void Update()
    {
        CheckVisibility();
    }

    public void CheckVisibility(){
        if(elementThatChangeVisibility == null) return;

        Vector3 goscreen = Camera.main.WorldToScreenPoint(elementThatChangeVisibility.transform.position);
        Debug.Log("GoPos " + goscreen);

        float distX = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f,0f));
        Debug.Log("distX " + distX);

        float distY = Vector3.Distance(new Vector3(0f, Screen.height / 2, 0f), new Vector3(0f, goscreen.y, 0f));
        Debug.Log("distY " + distY);

        if((distX > Screen.width || distY > Screen.height) && true)
        {
            if(deleteOrShow == "delete")
            {
                Destroy(elementThatChangeVisibility);
            }
            else if(deleteOrShow == "show")
            {
                elementThatChangeVisibility.SetActive(true);
            }
        }
    }
}
