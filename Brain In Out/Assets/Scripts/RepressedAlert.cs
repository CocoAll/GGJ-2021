using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepressedAlert : MonoBehaviour
{
    [SerializeField]
    private Text warningText;
    [SerializeField]
    private Color warningColor;
    [SerializeField]
    private Color transparent;
    [SerializeField]
    private TypeEnumValue overwhelmedType;


    private void Update()
    {
        if (overwhelmedType.value != TypeEnum.NEUTRE)
        {
            float timeDiff = Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup);
            if (timeDiff > 0 && timeDiff < 0.2f)
            {
                warningText.color = transparent;
            }
            else
                warningText.color = warningColor;
        }
    }
}
