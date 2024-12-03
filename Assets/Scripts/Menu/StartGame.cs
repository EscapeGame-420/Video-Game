using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button button;

    void Start()
    {
        InitializeButton();
    }

    public void InitializeButton()
    {
        if (button != null)
        {
            button.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogError("Button not assigned in the inspector.");
        }
    }

    public void ChangeScene()
    {
        LoadLevel1();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
