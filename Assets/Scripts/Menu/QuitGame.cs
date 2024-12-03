using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void Quit()
    {
        #if UNITY_EDITOR
            // If in the Unity Editor, stop the play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If in a built game, quit the application
            Application.Quit();
        #endif
    }
}
