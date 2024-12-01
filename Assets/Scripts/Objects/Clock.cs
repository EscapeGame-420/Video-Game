using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 2.0f;
    public string prefabPath = "UI/Canvas";
    public Canvas canvas;
    [SerializeField]
    public Vector3 canvasOffset = new Vector3(0.2f, -0.5f, 0f);
    public GameObject aiguille;
    public GameObject cle;
    public bool hasRun = false;
    public bool canPick = false;

    void Start()
    {
        InitializeClock();
    }

    void Update()
    {
        HandlePlayerInteraction();
    }

    public void InitializeClock()
    {
        GameObject canvaToAdd = Resources.Load<GameObject>(prefabPath);
        GameObject newObject = Instantiate(canvaToAdd, transform.position, transform.rotation);
        newObject.transform.SetParent(transform);

        cle.SetActive(false);
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.AddComponent<LookAtCam>();
        canvas.transform.position = transform.position + canvasOffset;

        GetComponent<Animator>().enabled = false;
    }

    public void HandlePlayerInteraction()
    {
        float distance = CalculateDistanceToPlayer();
        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (!hasRun)
        {
            if (distance <= activationDistance)
            {
                EnableCanvas();

                if (IsInteractionTriggered(inventory))
                {
                    ActivateClock(inventory);
                }
            }
            else
            {
                DisableCanvas();
            }
        }
        else
        {
            FinalizeClockInteraction();
        }
    }

    public float CalculateDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    public void EnableCanvas()
    {
        canvas.enabled = true;
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    public bool IsInteractionTriggered(Inventory inventory)
    {
        return Input.GetKeyDown("e") && inventory.IsSelectingItem("aiguille");
    }

    public void ActivateClock(Inventory inventory)
    {
        GetComponent<Animator>().enabled = true;
        inventory.UseItem("aiguille");
        aiguille.SetActive(true);
        hasRun = true;
        canPick = true;
    }

    public void FinalizeClockInteraction()
    {
        DisableCanvas();

        if (canPick)
        {
            cle.SetActive(true);
            canPick = false;
        }
    }
}
