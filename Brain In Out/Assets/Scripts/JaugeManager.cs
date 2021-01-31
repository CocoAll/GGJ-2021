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
    [SerializeField]
    private SpriteRenderer goJaugeAmourTemp;
    [SerializeField]
    private SpriteRenderer goJaugeTravailTemp;
    [SerializeField]
    private SpriteRenderer goJaugeSocialTemp;
    [SerializeField]
    private Color increaseColor;
    [SerializeField]
    private Color decreaseColor;



    private float goAmourScaleY;
    private float goTravailScaleY;
    private float goSocialScaleY;
    private float lastAmourScaleY;
    private float lastTravailScaleY;
    private float lastSocialScaleY;

    [SerializeField]
    private TypeEnumValue onOverwhelmed;

    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    private void Start()
    {
        goAmourScaleY = goJaugeAmour.transform.localScale.y;
        goTravailScaleY = goJaugeTravail.transform.localScale.y;
        goSocialScaleY = goJaugeSocial.transform.localScale.y;
        lastAmourScaleY = goAmourScaleY / 2;
        lastTravailScaleY = goTravailScaleY / 2;
        lastSocialScaleY = goSocialScaleY/2;
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
        // ##### AMOUR ######

        // Both gauges are set to the correct scale
        var newScale = this.goJaugeAmour.transform.localScale;
        newScale.y = lastAmourScaleY;
        this.goJaugeAmour.transform.localScale = newScale;
        this.goJaugeAmourTemp.transform.localScale = newScale;
        // New scale is calculated
        newScale = this.goJaugeAmour.transform.localScale;
        newScale.y = goAmourScaleY * (jaugeAmour.Value / 100.0f);
        // If last value > new value, value has decreased and we display red modification
        if ((lastAmourScaleY * 100 / goAmourScaleY) > jaugeAmour.Value)
        {
            this.goJaugeAmourTemp.color = decreaseColor;
            this.goJaugeAmour.transform.localScale = newScale;
            lastAmourScaleY = newScale.y;
        }
        // Else if value < new value, value has increased and we display green modification
        else if ((lastAmourScaleY * 100 / goAmourScaleY) < jaugeAmour.Value)
        {
            this.goJaugeAmourTemp.color = increaseColor;
            this.goJaugeAmourTemp.transform.localScale = newScale;
            lastAmourScaleY = newScale.y;
        }

        // ##### TRAVAIL ######

        // Both gauges are set to the correct scale
        newScale = this.goJaugeTravail.transform.localScale;
        newScale.y = lastTravailScaleY;
        this.goJaugeTravail.transform.localScale = newScale;
        this.goJaugeTravailTemp.transform.localScale = newScale;
        // New scale is calculated
        newScale = this.goJaugeTravail.transform.localScale;
        newScale.y = goTravailScaleY * (jaugeTravail.Value / 100.0f);
        // If last value > new value, value has decreased and we display red modification
        if ((lastTravailScaleY * 100 / goTravailScaleY) > jaugeTravail.Value)
        {
            this.goJaugeTravailTemp.color = decreaseColor;
            this.goJaugeTravail.transform.localScale = newScale;
            lastTravailScaleY = newScale.y;
        }
        // Else if value < new value, value has increased and we display green modification
        else if ((lastTravailScaleY * 100 / goTravailScaleY) < jaugeTravail.Value)
        {
            this.goJaugeTravailTemp.color = increaseColor;
            this.goJaugeTravailTemp.transform.localScale = newScale;
            lastTravailScaleY = newScale.y;
        }

        // ##### SOCIAL ######

        // Both gauges are set to the correct scale
        newScale = this.goJaugeSocial.transform.localScale;
        newScale.y = lastSocialScaleY;
        this.goJaugeSocial.transform.localScale = newScale;
        this.goJaugeSocialTemp.transform.localScale = newScale;
        // New scale is calculated
        newScale = this.goJaugeSocial.transform.localScale;
        newScale.y = goSocialScaleY * (jaugeSocial.Value / 100.0f);
        // If last value > new value, value has decreased and we display red modification
        if ((lastSocialScaleY * 100 / goSocialScaleY) > jaugeSocial.Value)
        {
            this.goJaugeSocialTemp.color = decreaseColor;
            this.goJaugeSocial.transform.localScale = newScale;
            lastSocialScaleY = newScale.y;
        }
        // Else if value < new value, value has increased and we display green modification
        else if ((lastSocialScaleY * 100 / goSocialScaleY) < jaugeSocial.Value)
        {
            this.goJaugeSocialTemp.color = increaseColor;
            this.goJaugeSocialTemp.transform.localScale = newScale;
            lastSocialScaleY = newScale.y;
        }
    }
}
