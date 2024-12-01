using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
// Classe AI pour gérer le comportement du NPC avec un système d'états
public class AI : MonoBehaviour {

    // Composants pour le mouvement, l'animation et l'audio
    NavMeshAgent agent;      // Navigation agent reference
    Animator anim;           // Animator reference for NPC animations
    public AudioSource audioSource; // AudioSource reference for NPC sounds
    State currentState;      // Current state of the NPC

    public Transform player; // Reference to the player for interaction
    
    public AudioClip sndFootstepLeft, sndFootstepRight, sndGrowl, sndAttack;
    public float audibleDistance = 15f;  // distance a partir de laquelle on peut entendre les pas du monstre

    public float stepCooldown = 0.6f;
    public float nextStepTime = 0f;
    public bool switchFoot = false;
    public bool attackSoundPlayed = false;
 
    // Initialisation au démarrage de la scène
    void Start() {
        agent = GetComponent<NavMeshAgent>();    // Initialisation de l'agent de navigation
        anim = GetComponent<Animator>();         // Initialisation de l'animateur
        audioSource = GetComponent<AudioSource>(); // Initialisation du son

        audioSource.spatialBlend = 1.0f; // Set to 3D sound
        audioSource.maxDistance = audibleDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        currentState = new Idle(gameObject, agent, anim, player); // Le NPC commence en état "Idle"
    }
 
    // Mise à jour à chaque frame pour traiter l'état actuel
    void Update() {
        currentState = currentState.Process(); // Mise à jour de l'état actuel du NPC
    
        if(Vector3.Distance(player.position, transform.position) <= audibleDistance){
            // Quand l'ennemi marche
            if (currentState is Patrol) {
                PlayFootStep();
            } else if (currentState is Pursue) {  // l'orsqu'il voit la victime et la poursuie
                PlayGrowlSound(); // Grognement 
                PlayFootStep();
            } 
            //else if (currentState is Attack) {
            //     PlayAttackSound(); // lorsqu'il attaque
            // } 
            else {
                StopFootStep(); // lorsqu'il arrete de marcher
            }
        }
        else{
            StopFootStep();
        }
    
    }

    public void PlayGrowlSound() {
        if (!audioSource.isPlaying) {
            audioSource.volume = 1.0f; // volume pour le grognement
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(sndGrowl);
        }
    }

    public void PlayFootStep() {
        if (Time.time >= nextStepTime && !audioSource.isPlaying) {
            switchFoot = !switchFoot;
            
            audioSource.volume = 0.3f; // volume moins fort pour les pas
            audioSource.pitch = Random.Range(0.7f, 0.9f);
            
            if (switchFoot) {
                audioSource.pitch = 2.0f;
                audioSource.PlayOneShot(sndFootstepLeft);
            } else {
                audioSource.pitch = 2.0f;
                audioSource.PlayOneShot(sndFootstepRight);
            }
            nextStepTime = Time.time + stepCooldown;
        }
    }

    public void StopFootStep() {
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    public void ResetAttackSound(){
        // eviter le reset du son de l'attaque
        attackSoundPlayed = false;
    }
}