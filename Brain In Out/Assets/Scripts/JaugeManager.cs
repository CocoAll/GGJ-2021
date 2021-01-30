using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class JaugeManager : MonoBehaviour
{

    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    private TypeEnum overwhelmingEmotion = TypeEnum.NEUTRE;
    [SerializeField]
    private TypeEnumValue onOverwhelmed;

    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;


    //
    //Application de l'effet en fonction de l'evenement
    //
    public void ApplyEnveloppeEffect()
    {
        ApplyEffect(currentEnveloppe.value.typeEnveloppe, currentEnveloppe.value.enveloppeEffect);
    }

    public void AppplyLetterEffetct()
    {
        ApplyEffect(currentEnveloppe.value.typeLettre, currentEnveloppe.value.lettreEffect);
    }

    /*public void AppplyRefouleEffetct()
    {
        ApplyEffect(currentEnveloppe.value.typeLettre, currentEnveloppe.value.lettreEffect);
    }*/

    //
    //Fonction qui gere l'application des differents effets des lettres
    //
    private void ApplyEffect(TypeEnum type, int value)
    {
        switch (type)
        {
            case TypeEnum.AMOUR:
                jaugeAmour.Value += value;
                if(jaugeAmour.Value < 20 && overwhelmingEmotion == TypeEnum.NEUTRE)
                {
                    overwhelmingEmotion = type;
                    onOverwhelmed.value = TypeEnum.AMOUR;
                }
                break;
            case TypeEnum.TRAVAIL:
                jaugeTravail.Value += value;
                if (jaugeAmour.Value < 20 && overwhelmingEmotion == TypeEnum.NEUTRE)
                {
                    overwhelmingEmotion = type;
                    onOverwhelmed.value = TypeEnum.TRAVAIL;                }
                break;
            case TypeEnum.SOCIAL:
                jaugeSocial.Value += value;
                if (jaugeAmour.Value < 20 && overwhelmingEmotion == TypeEnum.NEUTRE)
                {
                    overwhelmingEmotion = type;
                    onOverwhelmed.value = TypeEnum.SOCIAL;
                }
                break;
        }
    }
}
