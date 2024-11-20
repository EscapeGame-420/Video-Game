using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShadowManConvo : MonoBehaviour
{
    [SerializeField] private TextMeshPro manConvo;
    [SerializeField] private TextMeshPro JulieConvo;
    private List<string> messagesJulie;
    private List<string> messagesHomme;
    private int currentManMessageIndex = 0;
    private int currentJulieMessageIndex = 0;
    private bool isManSpeaking = false;
    public static bool isConvoFinished = false;


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
        };

        StartDialogue();
    }

    void StartDialogue()
    {
        ShowNextMessage();
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
            JulieConvo.text = messagesJulie[currentJulieMessageIndex];
            currentJulieMessageIndex++;
            isManSpeaking = true;
        }

        if(currentManMessageIndex == messagesHomme.Count && currentJulieMessageIndex == messagesJulie.Count){
            isConvoFinished = true;
        }
        
        Invoke("ShowNextMessage", 1f);
    }
}

