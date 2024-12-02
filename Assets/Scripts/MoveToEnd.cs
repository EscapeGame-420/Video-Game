using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnd : MonoBehaviour
{
    // Start is called before the first frame update
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Player")
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }
}
}
