using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class EnveloppeManager : MonoBehaviour
{

    //List des enveloppes de bases qui seront récurrentes
    [SerializeField]
    private List<ListEnveloppes> decksEnveloppes;

    //List des enveloppes à traiter pendant un round
    [SerializeField]
    private List<EnveloppeObject> enveloppes = new List<EnveloppeObject>();
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    [SerializeField]
    private BooleanValue isProcessingEnveloppe;

    [SerializeField]
    private SignalSender onEnveloppeDraw;

    //Remplissage de la liste d'enveloppes a traiter
    public void FillEnveloppes()
    {
        Debug.Log("FillEnveloppes of EnveloppeManager");
        foreach (ListEnveloppes le in decksEnveloppes)
        {
            int nbCurrentBaseEnveloppes = 0;
            while (nbCurrentBaseEnveloppes < le.nbToDraw)
            {
                EnveloppeObject eo = le.enveloppes[Random.Range(0, le.enveloppes.Count)];
                if (!enveloppes.Contains(eo))
                {
                    enveloppes.Add(eo);
                    nbCurrentBaseEnveloppes++;
                }
            }
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
        this.enveloppes.Add(currentEnveloppe.value);
    }
}
