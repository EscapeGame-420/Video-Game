using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// Déclaration de la classe IARobot qui hérite de MonoBehaviour
public class Raycast : MonoBehaviour {
 
    // Variable sérialisée exposée dans l'inspecteur pour ajuster la vitesse du robot dans Unity
    [SerializeField] float speed = 5f;
 
    // Déclaration d'un rayon qui sera utilisé pour détecter les obstacles devant le robot
    Ray rayon;
 
    // Déclaration d'une variable pour stocker les informations de collision lorsque le rayon touche un objet
    RaycastHit hit;
 
    // Variables sérialisées pour assigner les capteurs gauche et droit du robot dans l'éditeur Unity
    [SerializeField] Transform leftSensor, rightSensor;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }
 
    // Méthode Update appelée à chaque frame du jeu (environ 60 fois par seconde)
    void Update () {
 
        // Création d'un rayon partant de la position du capteur gauche, orienté vers l'avant du robot
        rayon = new Ray(leftSensor.position, transform.TransformDirection(Vector3.forward));
 
        // Si le rayon touche un objet dans le monde
        if(Physics.Raycast(rayon, out hit,Mathf.Infinity))
        {
            // Affiche le nom de l'objet touché et la distance dans la console pour le capteur gauche
            Debug.Log("Left Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);
 
            // Si l'objet détecté est à moins de 1 unité de distance
            if(hit.distance < 2)
            {
                // Génère un angle aléatoire entre 100 et 300 degrés pour que le robot tourne
                float angle = Random.Range(100f, 300f);
                // Fait tourner le robot autour de l'axe vertical (Y) selon l'angle aléatoire généré
                transform.Rotate(Vector3.up * angle * (Time.deltaTime/4));
            }
        }
 
        // Visualise le rayon du capteur gauche dans la vue Scène de Unity (une ligne jaune de 10 unités)
        Debug.DrawRay(leftSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
 
        // Création d'un rayon partant de la position du capteur droit, orienté vers l'avant du robot
        rayon = new Ray(rightSensor.position, transform.TransformDirection(Vector3.forward));
 
        // Si le rayon touche un objet dans le monde
        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            // Affiche le nom de l'objet touché et la distance dans la console pour le capteur droit
            Debug.Log("Right Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);
 
            // Si l'objet détecté est à moins de 1 unité de distance
            if (hit.distance < 1)
            {
                // Génère un angle aléatoire entre 100 et 300 degrés pour que le robot tourne
                float angle = Random.Range(100f, 300f);
 
                // Fait tourner le robot autour de l'axe vertical (Y) selon l'angle aléatoire généré
                transform.Rotate(Vector3.up * angle * (Time.deltaTime/4));
            }
        }
 
        // Visualise le rayon du capteur droit dans la vue Scène de Unity (une ligne jaune de 10 unités)
        Debug.DrawRay(rightSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
 
        // Déplace le robot vers l'avant selon sa direction actuelle avec la vitesse définie
        transform.Translate(Vector3.forward * speed * (Time.deltaTime / 4));
        animator.SetBool("walking",true);
    }
}