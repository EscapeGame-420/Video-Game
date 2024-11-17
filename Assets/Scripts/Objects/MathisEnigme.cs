using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathisEnigme : MonoBehaviour
{
    private string prefabPath = "UI/Canvas";
    private int enigmeCounter = 0;
    private Canvas canvas;
    private Coroutine flameCoroutine;
    private bool hasRun = false;

    [SerializeField]
    private SpriteRenderer bigFlame;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private GameObject aiguille;

    [SerializeField]
    private Animator candleAnimator;

    [SerializeField]
    private float activationDistance = 1.5f;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 canvasOffset = new Vector3(0, 1, 0);



    // Start is called before the first frame update
    void Start()
    {
        GameObject canvaToAdd = Resources.Load<GameObject>(prefabPath);
        GameObject newObject = Instantiate(canvaToAdd, transform.position, transform.rotation);
        newObject.transform.SetParent(transform);
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.AddComponent<LookAtCam>();
        canvas.transform.position = transform.position + canvasOffset;
        bigFlame.enabled = false;
        aiguille.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (candleAnimator.GetInteger("candleCount") == 2)
        {
            if (!hasRun)
            {
                flameCoroutine = StartCoroutine(ShowFlame(3f));
                aiguille.SetActive(true);
                canvas.enabled = false;
                fire.SetActive(false);
                hasRun = true;
            }   
        }
        else
        {
            if (distance <= activationDistance)
            {
                canvas.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gameObject.CompareTag("Good") && fire.activeSelf)
                    {
                        int enigmeCounter = candleAnimator.GetInteger("candleCount");
                        fire.SetActive(false);
                        enigmeCounter--;
                        candleAnimator.SetInteger("candleCount", enigmeCounter);
                        Debug.Log(enigmeCounter);
                    }
                    else if (gameObject.CompareTag("Good"))
                    {
                        int enigmeCounter = candleAnimator.GetInteger("candleCount");
                        fire.SetActive(true);
                        enigmeCounter++;
                        candleAnimator.SetInteger("candleCount", enigmeCounter);
                        Debug.Log(enigmeCounter);
                    }
                    else if (gameObject.CompareTag("Bad") && fire.activeSelf)
                    {
                        int enigmeCounter = candleAnimator.GetInteger("candleCount");
                        fire.SetActive(false);
                        enigmeCounter++;
                        candleAnimator.SetInteger("candleCount", enigmeCounter);
                        Debug.Log(enigmeCounter);
                    }
                    else if (gameObject.CompareTag("Bad"))
                    {
                        int enigmeCounter = candleAnimator.GetInteger("candleCount");
                        fire.SetActive(true);
                        enigmeCounter--;
                        candleAnimator.SetInteger("candleCount", enigmeCounter);
                        Debug.Log(enigmeCounter);
                    }
                }
            }
            else
            {
                canvas.enabled = false;
            }
        }


    }

    private IEnumerator ShowFlame(float seconds)
    {
        bigFlame.enabled = true;
        yield return new WaitForSeconds(seconds);
        bigFlame.enabled = false;
    }
}
