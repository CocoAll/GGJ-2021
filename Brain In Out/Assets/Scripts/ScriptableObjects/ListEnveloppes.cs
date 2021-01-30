using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Enveloppe", menuName = "ScriptableObject/ListEnveloppes", order = 1)]
public class ListEnveloppes : ScriptableObject
{
    public int nbToDraw;
    public List<EnveloppeObject> enveloppes;
}
