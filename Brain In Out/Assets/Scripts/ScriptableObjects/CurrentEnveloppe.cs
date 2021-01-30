using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Current Enveloppe", menuName = "ScriptableObject/CurrentEnveloppe", order = 2)]
public class CurrentEnveloppe : ScriptableObject
{
    public EnveloppeObject value;
}
