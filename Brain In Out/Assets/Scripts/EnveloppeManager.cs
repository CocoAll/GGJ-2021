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
    private SignalSender onCardDraw;

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
            if (!this.enveloppes.Contains(eo))
            {
                this.enveloppes.Add(eo);
                nbCurrentBaseEnveloppes++;
            }
        }
    }

    public void DrawEnveloppe()
    {
        this.currentEnveloppe.value = enveloppes[enveloppes.Count-1];
        this.enveloppes.RemoveAt(enveloppes.Count - 1);
        onCardDraw.Raise();
    }
}
