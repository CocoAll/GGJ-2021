using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class EnveloppeManager : MonoBehaviour
{

    //List des enveloppes de bases qui seront récurrentes
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppes;
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppesAmour2;
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppesTravail2;
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppesComplet;

    [SerializeField]
    private List<EnveloppeObject> listeRefoule;

    //List des enveloppes à traiter pendant un round
    [SerializeField]
    private List<EnveloppeObject> enveloppes;
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    //Liste des jauges
    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    [SerializeField]
    private BooleanValue isProcessingEnveloppe;

    [SerializeField]
    private SignalSender onEnveloppeDraw;

    [SerializeField]
    private TypeEnumValue overwhelmedType;

    [SerializeField]
    private GameObject tas1Enveloppe;
    [SerializeField]
    private GameObject tas2Enveloppe;
    [SerializeField]
    private GameObject tas3Enveloppe;
    [SerializeField]
    private GameObject tas4Enveloppe;
    [SerializeField]
    private GameObject tas5Enveloppe;

    //gestion de la corbeille
    [SerializeField]
    private CorbeilleManager corbeilleManager;

    private bool workLvl2 = false;
    private bool loveLvl2 = false;

    private void Start()
    {
        listeRefoule = new List<EnveloppeObject>();
    }

    //Remplissage de la liste d'enveloppes a traiter
    public void FillEnveloppes()
    {
        enveloppes = new List<EnveloppeObject>();
        foreach (ListEnveloppes le in decksEnveloppes)
        {
            int nbCurrentBaseEnveloppes = 0;
            while (nbCurrentBaseEnveloppes < le.nbToDraw)
            {
                EnveloppeObject eo = le.enveloppes[Random.Range(0, le.enveloppes.Count)];
                if (!enveloppes.Contains(eo))
                {
                    eo.remiseDansLeTas = false;
                    enveloppes.Add(eo);
                    nbCurrentBaseEnveloppes++;
                }
            }
        }

        CheckOverwhelmingValid();
        if(overwhelmedType.value != TypeEnum.NEUTRE)
        {
            foreach(EnveloppeObject eo in listeRefoule)
            {
                if(eo.typeRefoule == overwhelmedType.value)
                {
                    enveloppes.Add(eo);
                    corbeilleManager.DespawnBoullette();
                }
            }
        }

        //Shuffle de la liste
        for (int i = enveloppes.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            EnveloppeObject tmp = enveloppes[i];
            enveloppes[i] = enveloppes[r];
            enveloppes[r] = tmp;
        }

        SetUpTasEnveloppe();
    }

    private void CheckOverwhelmingValid()
    {
        if (overwhelmedType.value == TypeEnum.NEUTRE) return;

        switch (overwhelmedType.value)
        {
            case TypeEnum.AMOUR:
                if(jaugeAmour.Value > 20)
                {
                    overwhelmedType.value = TypeEnum.NEUTRE;
                }
                break;
            case TypeEnum.SOCIAL:
                if (jaugeSocial.Value > 20)
                {
                    overwhelmedType.value = TypeEnum.NEUTRE;
                }
                break;
            case TypeEnum.TRAVAIL:
                if (jaugeTravail.Value > 20)
                {
                    overwhelmedType.value = TypeEnum.NEUTRE;
                }
                break;
        }
    }

    public void DrawEnveloppe()
    {
        if (enveloppes == null || isProcessingEnveloppe.Value || enveloppes.Count == 0) return;

        this.currentEnveloppe.value = enveloppes[enveloppes.Count-1];
        this.enveloppes.RemoveAt(enveloppes.Count - 1);
        this.isProcessingEnveloppe.Value = true;
        SetUpTasEnveloppe();
        this.onEnveloppeDraw.Raise();
    }

    public int GetRemainingEnveloppesCount()
    {
        return this.enveloppes.Count;
    }

    public void ReinsertCurrentLetter()
    {
        currentEnveloppe.value.remiseDansLeTas = true;
        this.enveloppes.Insert(0, currentEnveloppe.value);
    }
    
    public void RefouleCurrentLetter()
    {
        corbeilleManager.GenerateBoullette();
        if (currentEnveloppe.value.refouleEffect >= 0) return;

        EnveloppeObject eo = Instantiate(currentEnveloppe.value);
        eo.refoule = true;
        this.listeRefoule.Add(eo);
    }

    public void SetUpTasEnveloppe()
    {
        if(enveloppes.Count == 0)
        {
            tas1Enveloppe.SetActive(false);
            tas2Enveloppe.SetActive(false);
            tas3Enveloppe.SetActive(false);
            tas4Enveloppe.SetActive(false);
            tas5Enveloppe.SetActive(false);
        } else if (enveloppes.Count == 1)
        {
            tas1Enveloppe.SetActive(true);
            tas2Enveloppe.SetActive(false);
            tas3Enveloppe.SetActive(false);
            tas4Enveloppe.SetActive(false);
            tas5Enveloppe.SetActive(false);
        }
        else if (enveloppes.Count < 7)
        {
            tas1Enveloppe.SetActive(false);
            tas2Enveloppe.SetActive(true);
            tas3Enveloppe.SetActive(false);
            tas4Enveloppe.SetActive(false);
            tas5Enveloppe.SetActive(false);
        }
        else if (enveloppes.Count < 15)
        {
            tas1Enveloppe.SetActive(false);
            tas2Enveloppe.SetActive(false);
            tas3Enveloppe.SetActive(true);
            tas4Enveloppe.SetActive(false);
            tas5Enveloppe.SetActive(false);
        }
        else if (enveloppes.Count < 25)
        {
            tas1Enveloppe.SetActive(false);
            tas2Enveloppe.SetActive(false);
            tas3Enveloppe.SetActive(false);
            tas4Enveloppe.SetActive(true);
            tas5Enveloppe.SetActive(false);
        }
        else
        {
            tas1Enveloppe.SetActive(false);
            tas2Enveloppe.SetActive(false);
            tas3Enveloppe.SetActive(false);
            tas4Enveloppe.SetActive(false);
            tas5Enveloppe.SetActive(true);
        }
    }

    public void ChangeDeckEnveloppe(string categorie, int lvl)
    {
        if (categorie == "amour" && lvl == 2)
        {
            decksEnveloppes = decksEnveloppesAmour2;
            loveLvl2 = true;
        }
        if (categorie == "travail" && lvl == 2)
        {
            decksEnveloppes = decksEnveloppesTravail2;
            workLvl2 = true;
        }
        if (workLvl2 && loveLvl2)
        {
            decksEnveloppes = decksEnveloppesComplet;
        }
    }
}
