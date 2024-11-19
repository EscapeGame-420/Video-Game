// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
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

public class BookClickHandler : MonoBehaviour
{
    public SecretRoomTrigger secretRoomTrigger;

    private void OnMouseDown()
    {
        secretRoomTrigger.OnBookSelected();
    }
}
