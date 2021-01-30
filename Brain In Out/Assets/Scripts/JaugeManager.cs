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

    [SerializeField]
    private SpriteRenderer goJaugeAmour;
    [SerializeField]
    private SpriteRenderer goJaugeTravail;
    [SerializeField]
    private SpriteRenderer goJaugeSocial;

    private float goAmourScaleY;
    private float goTravailScaleY;
    private float goSocialScaleY;

    [SerializeField]
    private TypeEnumValue onOverwhelmed;

    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    private void Start()
    {
        goAmourScaleY = goJaugeAmour.transform.localScale.y;
        goTravailScaleY = goJaugeTravail.transform.localScale.y;
        goSocialScaleY = goJaugeSocial.transform.localScale.y;
        UpdateJauges();
    }

    //
    //Application de l'effet en fonction de l'evenement
    //
    public void ApplyEnveloppeEffect()
    {
        ApplyEffect(currentEnveloppe.value.typeEnveloppe, currentEnveloppe.value.enveloppeEffect);
    }

    public void AppplyLetterEffect()
    {
        if (!currentEnveloppe.value.refoule)
        {
            ApplyEffect(currentEnveloppe.value.typeLettre, currentEnveloppe.value.lettreEffect);
        }
        else
        {
            ApplyEffect(currentEnveloppe.value.typeRefoule, currentEnveloppe.value.refouleEffect);
        }
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
                jaugeAmour.Value = Mathf.Clamp(jaugeAmour.Value, 0, 100);
                
                if (jaugeAmour.Value < 20 && onOverwhelmed.value == TypeEnum.NEUTRE)
                {
                    onOverwhelmed.value = TypeEnum.AMOUR;
                }
                break;
            case TypeEnum.TRAVAIL:
                jaugeTravail.Value += value;
                jaugeTravail.Value = Mathf.Clamp(jaugeTravail.Value, 0, 100);

                if (jaugeTravail.Value < 20 && onOverwhelmed.value == TypeEnum.NEUTRE)
                {
                    onOverwhelmed.value = TypeEnum.TRAVAIL;                }
                break;
            case TypeEnum.SOCIAL:
                jaugeSocial.Value += value;
                jaugeSocial.Value = Mathf.Clamp(jaugeSocial.Value, 0, 100);

                if (jaugeSocial.Value < 20 && onOverwhelmed.value == TypeEnum.NEUTRE)
                {
                    onOverwhelmed.value = TypeEnum.SOCIAL;
                }
                break;
        }
        UpdateJauges();
    }

    public void UpdateJauges()
    {
        var newScale = this.goJaugeAmour.transform.localScale;
        newScale.y = goAmourScaleY * (jaugeAmour.Value / 100.0f);
        this.goJaugeAmour.transform.localScale = newScale;

        newScale = this.goJaugeTravail.transform.localScale;
        newScale.y = goTravailScaleY * (jaugeTravail.Value / 100.0f);
        this.goJaugeTravail.transform.localScale = newScale;

        newScale = this.goJaugeSocial.transform.localScale;
        newScale.y = goSocialScaleY * (jaugeSocial.Value / 100.0f);
        this.goJaugeSocial.transform.localScale = newScale;
    }
}
