using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] Button restartButton;


    void Start()
    {

        restartButton.onClick.AddListener(Restart);
    }

    void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}
