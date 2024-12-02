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

    // La clé de piano à afficher
    public GameObject pianoKey;  

    // Les 17 boutons représentant les touches du piano
    public Button[] pianoButtons;  
    private int currentButtonIndex = 0;  // L'index du bouton que l'utilisateur doit cliquer

    private bool isCanvasActive = false;  

    void Start()
    {
        // Assurer que la clé de piano est cachée au début
        pianoKey.SetActive(false);

        // Ajouter les listeners sur les boutons piano
        for (int i = 0; i < pianoButtons.Length; i++)
        {
            int buttonIndex = i;  // Pour éviter les problèmes de closure dans le loop
            pianoButtons[i].onClick.AddListener(() => OnPianoButtonClick(buttonIndex));
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, target.position);

        // Si le joueur est suffisamment proche du piano, afficher le prompt
        if (distance <= activationDistance)
        {
            promptCanvas.SetActive(true);  // Affiche le canvas prompt

            // Si la touche "E" est pressée, on alterne l'état du canvas
            if (Input.GetKeyDown(keyToPress))
            {
                ToggleCanvas();  // Gère uniquement l'ouverture/fermeture du canvas
            }
        }
        else
        {
            promptCanvas.SetActive(false);  // Masque le prompt si le joueur est trop loin
        }
    }

    // Fonction pour alterner l'état du canvas
    void ToggleCanvas()
    {
        if (!isCanvasActive)
        {
            canvasToOpen.SetActive(true);  // Affiche le canvas
            pianoKey.SetActive(false);  // La clé est toujours masquée au début

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraController.enabled = false;  // Désactive la caméra
        }
        else
        {
            canvasToOpen.SetActive(false);  // Masque le canvas
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraController.enabled = true;  // Réactive la caméra
        }

        // Alterne l'état du canvas
        isCanvasActive = !isCanvasActive;
    }

    // Fonction appelée lorsqu'un bouton du piano est cliqué
    void OnPianoButtonClick(int buttonIndex)
    {
        // Si le bouton cliqué correspond à la séquence attendue
        if (buttonIndex == currentButtonIndex)
        {
            currentButtonIndex++;  // Passe au bouton suivant

            // Si tous les boutons ont été cliqués dans l'ordre, afficher la clé et fermer le canvas
            if (currentButtonIndex == pianoButtons.Length)
            {
                // Affiche la clé de piano uniquement après toute la séquence réussie
                pianoKey.SetActive(true);
                Debug.Log("Sequence complete! Piano key displayed."); // Message de débogage
                CloseCanvas();  // Ferme le canvas automatiquement
            }
        }
        else
        {
            // Si un bouton incorrect est cliqué, réinitialiser la séquence
            Debug.Log("Incorrect button clicked. Resetting sequence."); // Message de débogage
            currentButtonIndex = 0;  // Réinitialise la séquence
            pianoKey.SetActive(false);  // La clé reste masquée
        }
    }

    // Fonction pour fermer le canvas
    void CloseCanvas()
    {
        canvasToOpen.SetActive(false);  // Masque le canvas
        isCanvasActive = false;  // Met à jour l'état du canvas
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraController.enabled = true;  // Réactive la caméra
    }
}