using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;
using UnityEngine.UI;

public class LettreManager : MonoBehaviour
{
    [Header("UI Go")]
    [SerializeField]
    private GameObject lettreObject;
    [SerializeField]
    private GameObject enveloppeObject;
    [SerializeField]
    private Button remettreButton;
    [SerializeField]
    private Button refouleButton;

    [Header("Divers Scriptable Object")]
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    [SerializeField]
    private BooleanValue isProcessingEnveloppe;
    [SerializeField]
    private BooleanValue isGameRunning;

    [SerializeField]
    private SignalSender onRoundEnd;

    private EnveloppeManager enveloppeManager;

    [Header("Enveloppe/Lettre visuals")]
    [SerializeField]
    private GameObject tamponAmour;
    [SerializeField]
    private GameObject tamponTravail;
    [SerializeField]
    private GameObject tamponSocial;
    [SerializeField]
    private Text enveloppeTexte;
    [SerializeField]
    private Text lettreTexte;

    [Header("Jauges values")]
    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    private void Start()
    {
        enveloppeManager = FindObjectOfType<EnveloppeManager>();
        lettreObject.SetActive(false);
        enveloppeObject.SetActive(false);
    }

    //
    //Actions sur l'enveloppe
    //
    public void OuvrirLettre()
    {
        enveloppeObject.SetActive(false);
        SetUpLettre();
    }

    public void RemettreLettre()
    {
        isProcessingEnveloppe.Value = false;
        enveloppeManager.ReinsertCurrentLetter();
        enveloppeObject.SetActive(false);
    }

    public void RefoulerLettre()
    {
        enveloppeManager.RefouleCurrentLetter();
        isProcessingEnveloppe.Value = false;
        CheckRoundState();
        enveloppeObject.SetActive(false);
    }

    //
    //Actions sur la lettre
    //
    public void FermerLettre()
    {
        isProcessingEnveloppe.Value = false;
        CheckRoundState();
        lettreObject.SetActive(false);
    }

    //
    //Set up des données
    //
    public void SetUpEnveloppe()
    {
        TypeEnum tee = currentEnveloppe.value.typeEnveloppe;
        switch (tee)
        {
            case TypeEnum.AMOUR:
                tamponAmour.SetActive(true);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(false);
                break;
            case TypeEnum.TRAVAIL:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(true);
                tamponSocial.SetActive(false);
                break;
            case TypeEnum.SOCIAL:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(true);
                break;
            case TypeEnum.NEUTRE:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(false);
                break;
        }

        //Désactions des interactions des boutons si nécéssaires
        if (currentEnveloppe.value.refoule)
        {
            remettreButton.interactable = false;
            refouleButton.interactable = false;
        }else if (currentEnveloppe.value.remiseDansLeTas)
        {
            remettreButton.interactable = false;
        }
        

        //Reset des bouttons si nécéssaires
        if (remettreButton.interactable == false && !currentEnveloppe.value.remiseDansLeTas)
        {
            remettreButton.interactable = true;
        }
        if (refouleButton.interactable == false && !currentEnveloppe.value.refoule)
        {
            refouleButton.interactable = true;
        }

        enveloppeTexte.text = currentEnveloppe.value.titre;

        enveloppeObject.SetActive(true);
    }

    public void SetUpLettre()
    {
        lettreTexte.text = currentEnveloppe.value.contenu;
        lettreObject.SetActive(true);
    }

    //
    //Action de gestion fin de round
    //
    public void CheckRoundState()
    {
        if(enveloppeManager.GetRemainingEnveloppesCount() == 0)
        {
            isGameRunning.Value = false;
            onRoundEnd.Raise();
        }
    }

    public void OnRoundEnd()
    {
        lettreObject.SetActive(false);
        enveloppeObject.SetActive(false);
    }
}
