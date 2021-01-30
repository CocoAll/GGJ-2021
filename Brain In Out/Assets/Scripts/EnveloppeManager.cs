using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class EnveloppeManager : MonoBehaviour
{

    //List des enveloppes de bases qui seront récurrentes
    [SerializeField]
    private ListEnveloppes baseEnveloppes;

    //List des enveloppes à traiter pendant un round
    [SerializeField]
    private List<EnveloppeObject> enveloppes = new List<EnveloppeObject>();
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    [SerializeField]
    private BooleanValue isProcessingEnveloppe;

    [SerializeField]
    private SignalSender onEnveloppeDraw;

    private void Start()
    {
        FillEnveloppes();
    }

    //Remplissage de la liste d'enveloppes a traiter
    public void FillEnveloppes()
    {
        int nbCurrentBaseEnveloppes = 0;
        while (nbCurrentBaseEnveloppes < this.baseEnveloppes.nbToDraw)
        {
            EnveloppeObject eo = this.baseEnveloppes.enveloppes[Random.Range(0, this.baseEnveloppes.enveloppes.Count)];
            if (!enveloppes.Contains(eo))
            {
                enveloppes.Add(eo);
                nbCurrentBaseEnveloppes++;
            }
        }
    }

    public void DrawEnveloppe()
    {
        if (enveloppes == null || isProcessingEnveloppe.Value || enveloppes.Count == 0) return;

        this.currentEnveloppe.value = enveloppes[enveloppes.Count-1];
        this.enveloppes.RemoveAt(enveloppes.Count - 1);
        this.isProcessingEnveloppe.Value = true;
        this.onEnveloppeDraw.Raise();
    }

    public int GetRemainingEnveloppesCount()
    {
        return this.enveloppes.Count;
    }

    public void ReinsertCurrentLetter()
    {
        this.enveloppes.Add(currentEnveloppe.value);
    }
}
