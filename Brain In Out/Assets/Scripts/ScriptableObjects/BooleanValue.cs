using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boolean Value", menuName = "ScriptableObject/Boolean Value", order = 3)]
public class BooleanValue : ScriptableObject
{
    [SerializeField]
    private bool defaultValue;
    private bool value;
    public bool Value { get { return value; } set { this.value = value; } }

    private void OnEnable()
    {
        value = defaultValue;
    }
}
