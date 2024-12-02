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
    [SerializeField] private GameObject PlaceToStay;

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
            "Mais où sommes-nous ? Qui êtes-vous exactement ?",
            "Comment diable avons-nous fini ici ?",
            "Depuis combien de temps êtes-vous prisonnier ici ?",
            "Il faut absolument que je trouve un moyen de sortir d'ici.",
            "Il doit bien y avoir une solution, il suffit de la trouver...",
            "Avez-vous déjà essayé de fuir cet endroit ?",
            "Avez-vous remarqué quelque chose d'inhabituel dans cette pièce ?",
            "Cette peinture sur le mur... Elle me semble étrange.",
            "Pensez-vous qu'il pourrait y avoir quelque chose de caché derrière ?",
            "Nous devons unir nos forces pour trouver une solution et nous enfuir d'ici.",
            "Savez-vous au moins ce qui m'attend en haut ?",
            "C'est tout ce que vous savez ?",
            "D'accord, je vais essayer de les trouver",
            "Je ferai attention, merci pour l'information"
        };

        messagesHomme = new List<string>
        {
            "Nous sommes enfermés dans le sous-sol d'un kidnappeur. Moi aussi, j'ai été enlevé.",
            "Je ne sais pas comment nous sommes arrivés ici. Mes souvenirs sont flous.",
            "J'ai arrêté de compter les jours depuis longtemps, cela fait une éternité que je suis ici.",
            "J'ai fouillé chaque recoin, mais je n'ai trouvé aucune issue.",
            "Cet endroit a vraiment quelque chose de très étrange...",
            "Oui, j'ai tenté de m'évader plusieurs fois, sans succès.",
            "La seule chose qui me semble vraiment étrange est cette peinture sur le mur.",
            "Elle semble trop récente, trop propre, comme si elle cachait quelque chose.",
            "Peut-être y a-t-il quelque chose dissimulé derrière cette peinture.",
            "Je suis piégé ici pour l'éternité... même si je le voulais, je ne pourrais pas partir. Mon âme est liée à cet endroit.",
            "Une fois que vous serez sorti du sous-sol, il faudra que vous sortiez par la porte principale",
            "La porte est fermée à clé, les trois clés sont cachées dans la maison, il faudra les trouver pour ouvrir la porte.",
            "Mais attention, le kidnappeur est très vigilant, il ne faudra pas qu'il vous attrape. Il rode dans la maison",
            "Bonne chance, et faites attention à vous"
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
            disablePlayerMovement();
        }
        
        if(isConvoStarted && Input.GetKeyDown(KeyCode.T)){
            ShowNextMessage();
            if(isConvoFinished){
                continuePanel.SetActive(false);
            }
        }
    }

    void ShowNextMessage()
    {
        manConvo.text = "";
        JulieConvo.text = "";

        if(isManSpeaking && currentManMessageIndex < messagesHomme.Count){
            manConvo.text = messagesHomme[currentManMessageIndex];
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
            enablePlayerMovement();
        }
    }
    void disablePlayerMovement(){
        //player.GetComponent<JulieMovement>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        player.transform.position = PlaceToStay.transform.position;
        player.transform.LookAt(transform);
    }

    void enablePlayerMovement(){
        //player.GetComponent<JulieMovement>().enabled = true;
        player.GetComponent<Animator>().enabled = true;
    }
}

