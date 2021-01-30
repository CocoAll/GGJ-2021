using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class EnveloppeManager : MonoBehaviour
{

    //List des enveloppes de bases qui seront récurrentes
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppes;

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

    private void Start()
    {
        listeRefoule = new List<EnveloppeObject>();
    }

    //Remplissage de la liste d'enveloppes a traiter
    public void FillEnveloppes()
    {
        enveloppes = new List<EnveloppeObject>();
        Debug.Log("FillEnveloppes of EnveloppeManager");
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
                if(eo.typeReffoule == overwhelmedType.value)
                {
                    enveloppes.Add(eo);
                }
            }
        }
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
        Debug.Log("DrawEnveloppes of EnveloppeManager");
        if (enveloppes == null || isProcessingEnveloppe.Value || enveloppes.Count == 0) return;

        this.currentEnveloppe.value = enveloppes[enveloppes.Count-1];
        this.enveloppes.RemoveAt(enveloppes.Count - 1);
        this.isProcessingEnveloppe.Value = true;
        this.onEnveloppeDraw.Raise();
    }

    public int GetRemainingEnveloppesCount()
    {
        Debug.Log("GetRemainingEnveloppesCount of EnveloppeManager");
        return this.enveloppes.Count;
    }

    public void ReinsertCurrentLetter()
    {
        Debug.Log("ReinsertCurrentLetter of EnveloppeManager");
        currentEnveloppe.value.remiseDansLeTas = true;
        this.enveloppes.Insert(0, currentEnveloppe.value);
    }
    
    public void RefouleCurrentLetter()
    {
        Debug.Log("ReinsertCurrentLetter of EnveloppeManager");
        if (currentEnveloppe.value.reffouleEffect >= 0) return;

        EnveloppeObject eo = Instantiate(currentEnveloppe.value);
        eo.refoule = true;
        this.listeRefoule.Add(eo);
    }
}
