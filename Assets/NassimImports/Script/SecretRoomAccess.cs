// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SecretRoomAccess : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

using UnityEngine;

public class SecretRoomTrigger : MonoBehaviour
{
    public GameObject targetBook; // The book that triggers the secret
    public Transform pulledOutPosition; // Position where the book moves when pulled
    public Transform originalShelfPosition; // The original position of the shelf
    public Transform revealShelfPosition; // The position of the shelf when revealing the secret room
    public float bookAnimationSpeed = 2f; // Speed of the book animation
    public float shelfMoveSpeed = 2f; // Speed of the shelf movement

    private bool bookPulled = false;
    private bool shelfRevealed = false;

    private void Update()
    {
        if (bookPulled && !shelfRevealed)
        {
            MoveShelfToReveal();
        }
    }

    // Call this method when the target book is clicked
    public void OnBookSelected()
    {
        if (!bookPulled)
        {
            StartCoroutine(AnimateBook());
        }
    }

    private System.Collections.IEnumerator AnimateBook()
    {
        Vector3 originalBookPosition = targetBook.transform.position;

        // Pull the book out
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            targetBook.transform.position = Vector3.Lerp(originalBookPosition, pulledOutPosition.position, elapsedTime);
            elapsedTime += Time.deltaTime * bookAnimationSpeed;
            yield return null;
        }

        // Return the book
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            targetBook.transform.position = Vector3.Lerp(pulledOutPosition.position, originalBookPosition, elapsedTime);
            elapsedTime += Time.deltaTime * bookAnimationSpeed;
            yield return null;
        }

        bookPulled = true;
    }

    private void MoveShelfToReveal()
    {
        transform.position = Vector3.MoveTowards(transform.position, revealShelfPosition.position, shelfMoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, revealShelfPosition.position) < 0.01f)
        {
            shelfRevealed = true;
        }
    }
}
