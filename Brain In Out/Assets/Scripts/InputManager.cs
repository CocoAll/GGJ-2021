﻿using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private BooleanValue isGameRunning;
    [SerializeField]
    private BooleanValue isProcessingLetter;

    private EnveloppeManager em;

    private void Start()
    {
        em = FindObjectOfType<EnveloppeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    }

    private void ProcessClick(GameObject go)
    {
        if (go.CompareTag("Enveloppe") && !isProcessingLetter.Value)
        {
           em.DrawEnveloppe();
        }
    }
}
