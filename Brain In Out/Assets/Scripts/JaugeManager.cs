using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugeManager : MonoBehaviour
{

    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyEffect(TypeEnum type, int value)
    {
        switch (type)
        {
            case TypeEnum.AMOUR:
                jaugeAmour.Value += value;
                break;
            case TypeEnum.TRAVAIL:
                jaugeTravail.Value += value;
                break;
            case TypeEnum.SOCIAL:
                jaugeSocial.Value += value;
                break;
        }
        //TODO Refresh UI
    }
}
