using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private BooleanValue isGameRunning;
    [SerializeField]
    private BooleanValue isProcessingLetter;

    [SerializeField]
    private GameObject panelPause;
    [SerializeField]
    private GameObject timerPanel;

    private bool isOnPause;
    private bool prePauseGameRunning;
    private EnveloppeManager em;

    [SerializeField]
    private AudioSource audioSourceMusic;

    [SerializeField]
    private List<GameObject> tasEnveloppes;
    [SerializeField]
    private List<GameObject> tasEnveloppesGlow;

    private bool glowing = false;

    private void Start()
    {
        isOnPause = false;
        em = FindObjectOfType<EnveloppeManager>();
    }

    // Update is called once per frame
    void Update()
    {



        //On récupère la position de la souris à l'écran
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        // On raycast à la position de la souris pour un collicer 2d
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            DisplayGlow(hit.collider.gameObject);
            if (Input.GetMouseButtonDown(0) && !isOnPause)
            {
                ProcessClick(hit.collider.gameObject);
            }
        }
        else if (glowing)
            StopGlow();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOnPause)
            {
                SetUpPause();
            }
            else
            {
                UnSetPause();
            }
        }
    }

    private void ProcessClick(GameObject go)
    {
        if (go.CompareTag("Enveloppe") && !isProcessingLetter.Value)
        {
           em.DrawEnveloppe();
        }
    }

    public void SetUpPause()
    {
        isOnPause = true;
        panelPause.SetActive(true);
        timerPanel.SetActive(false);
        audioSourceMusic.Pause();
         prePauseGameRunning = isGameRunning.Value; 
        isGameRunning.Value = false;
    }

    public void UnSetPause()
    {
        isOnPause = false;
        timerPanel.SetActive(true);
        panelPause.SetActive(false);
        audioSourceMusic.Play();
        isGameRunning.Value = prePauseGameRunning;
    }

    private void DisplayGlow(GameObject go)
    {
        if (go.CompareTag("Enveloppe") && !isProcessingLetter.Value)
        {
            for (int i = 0; i<tasEnveloppes.Count; i++)
            {
                if (tasEnveloppes[i].activeSelf)
                {
                    tasEnveloppesGlow[i].SetActive(true);
                    glowing = true;
                }
            }
        }
    }

    private void StopGlow()
    {
        foreach (GameObject go in tasEnveloppesGlow)
        {
            if (go.activeSelf)
                go.SetActive(false);
        }
        glowing = false;
    }
}
