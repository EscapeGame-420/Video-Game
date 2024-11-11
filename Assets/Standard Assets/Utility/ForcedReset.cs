using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import pour l'UI
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(RawImage))] // Utilise RawImage au lieu de GUITexture
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // si le bouton "ResetObject" est pressé ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... rechargez la scène
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
