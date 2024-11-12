using UnityEngine;

public class PlayerController : MonoBehaviour {

    public DeathSequence deathSequence;
    
    void Start() {
        deathSequence = FindObjectOfType<DeathSequence>();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            deathSequence.TriggerDeathSequence();
        }
    }
}
