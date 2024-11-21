using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShadowManConvo : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshPro manConvo;
    [SerializeField] private TMP_Text JulieConvo;
    [SerializeField] private TextMeshPro FToTalk;
    [SerializeField] private GameObject continuePanel;

    private List<string> messagesJulie;
    private List<string> messagesHomme;
    private int currentManMessageIndex = 0;
    private int currentJulieMessageIndex = 0;
    private bool isManSpeaking = false;
    private bool isPlayerNear;

    public static bool isConvoFinished = false;
    public static bool isConvoStarted = false;


    void Start()
    {
        messagesJulie = new List<string>
        {
            "Où suis-je ? Qui êtes-vous ?",
            "Comment sommes-nous arrivés ici ?",
            "Ça fait combien de temps que vous êtes ici ?",
            "Je dois absolument trouver un moyen de sortir d'ici.",
            "Il doit bien y avoir quelque chose que nous pouvons faire...",
            "Avez-vous déjà essayé de vous échapper ?",
            "Y a-t-il quelque chose d'inhabituel dans cette pièce ?",
            "La peinture sur le mur... elle semble bizarre.",
            "Pensez-vous qu'elle cache quelque chose ?",
            "Nous devons travailler ensemble pour trouver une solution."
        };

        messagesHomme = new List<string>
        {
            "Vous êtes dans le sous-sol d'un kidnappeur. Moi aussi, je me suis fait kidnapper.",
            "Je ne sais pas exactement comment nous sommes arrivés ici. Tout est flou.",
            "J'ai perdu espoir de sortir après 4 jours.",
            "J'ai cherché partout, mais je n'ai trouvé aucune issue.",
            "Il y a quelque chose d'étrange à propos de cet endroit...",
            "Oui, plusieurs fois, mais en vain.",
            "La seule chose étrange que j'ai remarquée, c'est cette peinture sur le mur.",
            "Exactement, cette peinture semble trop récente, trop propre.",
            "Peut-être qu'il y a quelque chose derrière.",
            "Je préfère rester ici et attendre que quelqu'un vienne nous chercher. Mais rien ne vous empêche de chercher une issue."
            // or something like "ma place est ici dans ce sous-sol, il est trop tard pour moi" to make the player feel like they have to escape alone
            // et peut etre rajouter plus de temps a la sentence du mec pour faire genre qu<il a arret/ de compter
            // je suis ici depuis tellement longtemps que j<ai arrete de compter
        };
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float isPlayerInFront = Vector3.Dot(transform.forward, player.transform.position - transform.position);
        isPlayerNear = distance < 4f && !isConvoStarted && isPlayerInFront > 0;

        FToTalk.gameObject.SetActive(isPlayerNear);
    }

    void StartDialogue()
    {
        ShowNextMessage();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T) && isPlayerNear){
            isConvoStarted = true;
            continuePanel.SetActive(true);
        }
        
        if(isConvoStarted && Input.GetKeyDown(KeyCode.T)){
            ShowNextMessage();
        }
    }

    void ShowNextMessage()
    {
        manConvo.text = "";
        JulieConvo.text = "";

        if(isManSpeaking && currentManMessageIndex < messagesHomme.Count){
            manConvo.text = "MAN: " + messagesHomme[currentManMessageIndex];
            currentManMessageIndex++;
            isManSpeaking = false;
        }
        else if(!isManSpeaking && currentJulieMessageIndex < messagesJulie.Count){
            JulieConvo.text = "YOU: " + messagesJulie[currentJulieMessageIndex];
            currentJulieMessageIndex++;
            isManSpeaking = true;
        }

        if(currentManMessageIndex == messagesHomme.Count && currentJulieMessageIndex == messagesJulie.Count){
            isConvoFinished = true;
        }
        
        //Invoke("ShowNextMessage", 1f);
    }
}

