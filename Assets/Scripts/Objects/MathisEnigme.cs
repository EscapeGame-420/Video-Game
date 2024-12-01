using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathisEnigme : MonoBehaviour
{
    public string prefabPath = "UI/Canvas";
    public int enigmeCounter = 0;
    public Canvas canvas;
    public Coroutine flameCoroutine;
    public bool hasRun = false;

    [SerializeField]
    public SpriteRenderer bigFlame;

    [SerializeField]
    public GameObject fire;

    [SerializeField]
    public GameObject aiguille;

    [SerializeField]
    public Animator candleAnimator;

    [SerializeField]
    public float activationDistance = 1.5f;

    [SerializeField]
    public Transform player;

    [SerializeField]
    public Vector3 canvasOffset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        InitializeCanvas();
        InitializeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCandleState();
    }

    public void InitializeCanvas()
    {
        GameObject canvaToAdd = Resources.Load<GameObject>(prefabPath);
        GameObject newObject = Instantiate(canvaToAdd, transform.position, transform.rotation);
        newObject.transform.SetParent(transform);
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.AddComponent<LookAtCam>();
        canvas.transform.position = transform.position + canvasOffset;
    }

    public void InitializeObjects()
    {
        bigFlame.enabled = false;
        aiguille.SetActive(false);
    }

    public void HandleCandleState()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (candleAnimator.GetInteger("candleCount") == 2)
        {
            if (!hasRun)
            {
                ActivateFinalState();
            }
        }
        else
        {
            HandlePlayerInteraction(distance);
        }
    }

    public void ActivateFinalState()
    {
        flameCoroutine = StartCoroutine(ShowFlame(3f));
        aiguille.SetActive(true);
        canvas.enabled = false;
        fire.SetActive(false);
        hasRun = true;
    }

    public void HandlePlayerInteraction(float distance)
    {
        if (distance <= activationDistance)
        {
            canvas.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCandleState();
            }
        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void UpdateCandleState()
    {
        int enigmeCounter = candleAnimator.GetInteger("candleCount");

        if (gameObject.CompareTag("Good"))
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
                enigmeCounter--;
            }
            else
            {
                fire.SetActive(true);
                enigmeCounter++;
            }
        }
        else if (gameObject.CompareTag("Bad"))
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
                enigmeCounter++;
            }
            else
            {
                fire.SetActive(true);
                enigmeCounter--;
            }
        }

        candleAnimator.SetInteger("candleCount", enigmeCounter);
        Debug.Log(enigmeCounter);
    }

    public IEnumerator ShowFlame(float seconds)
    {
        bigFlame.enabled = true;
        yield return new WaitForSeconds(seconds);
        bigFlame.enabled = false;
    }
}
