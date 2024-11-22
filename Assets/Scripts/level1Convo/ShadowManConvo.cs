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
            "Mais où sommes-nous ? Qui êtes-vous exactement ?",
            "Comment diable avons-nous fini ici ?",
            "Depuis combien de temps êtes-vous prisonnier ici ?",
            "Il faut absolument que je trouve un moyen de sortir d'ici.",
            "Il doit bien y avoir une solution, il suffit de la trouver...",
            "Avez-vous déjà essayé de fuir cet endroit ?",
            "Avez-vous remarqué quelque chose d'inhabituel dans cette pièce ?",
            "Cette peinture sur le mur... Elle me semble étrange.",
            "Pensez-vous qu'il pourrait y avoir quelque chose de caché derrière ?",
            "Nous devons unir nos forces pour trouver une solution et nous enfuir d'ici."
        };


        // messagesHomme = new List<string>
        // {
        //     "Vous êtes dans le sous-sol d'un kidnappeur. Moi aussi, je me suis fait kidnapper.",
        //     "Je ne sais pas exactement comment nous sommes arrivés ici. Tout est flou.",
        //     "J'ai perdu espoir de sortir après 4 jours.",
        //     "J'ai cherché partout, mais je n'ai trouvé aucune issue.",
        //     "Il y a quelque chose d'étrange à propos de cet endroit...",
        //     "Oui, plusieurs fois, mais en vain.",
        //     "La seule chose étrange que j'ai remarquée, c'est cette peinture sur le mur.",
        //     "Exactement, cette peinture semble trop récente, trop propre.",
        //     "Peut-être qu'il y a quelque chose derrière.",
        //     "Je préfère rester ici et attendre que quelqu'un vienne nous chercher. Mais rien ne vous empêche de chercher une issue."
        //     // or something like "ma place est ici dans ce sous-sol, il est trop tard pour moi" to make the player feel like they have to escape alone
        //     // et peut etre rajouter plus de temps a la sentence du mec pour faire genre qu<il a arret/ de compter
        //     // je suis ici depuis tellement longtemps que j<ai arrete de compter
        // };
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
            "Je suis piégé ici pour l'éternité... même si je le voulais, je ne pourrais pas partir. Mon âme est liée à cet endroit."
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

