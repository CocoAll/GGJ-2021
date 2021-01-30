using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntValue", menuName = "ScriptableObject/IntValue", order = 4)]
public class IntValue : ScriptableObject
{
    [SerializeField]
    private int defaultValue;
    private int value;
    public int Value { get { return value; } set { this.value = value; } }

    private void OnEnable()
    {
        value = defaultValue;
    }
}
