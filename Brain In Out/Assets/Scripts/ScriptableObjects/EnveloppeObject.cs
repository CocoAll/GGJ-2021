using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enveloppe", menuName = "ScriptableObject/Enveloppe", order = 0)]
public class EnveloppeObject : ScriptableObject
{
    public string titre;
    [TextArea(3, 5)]
    public string contenu;
    public TypeEnum typeEnveloppe;
    public int enveloppeEffect;
    public TypeEnum typeLettre;
    public int lettreEffect;
}
