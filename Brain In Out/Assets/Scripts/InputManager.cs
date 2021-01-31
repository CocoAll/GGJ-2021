using UnityEngine;

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

    private void Start()
    {
        isOnPause = false;
        em = FindObjectOfType<EnveloppeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnPause)
        {
            //On récupère la position de la souris à l'écran
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // On raycast à la position de la souris pour un collicer 2d
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null )
            {
                ProcessClick(hit.collider.gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
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
}
