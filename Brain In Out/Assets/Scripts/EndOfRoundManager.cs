using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundManager : MonoBehaviour
{

    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeSocial;
    [SerializeField]
    private IntValue jaugeTravail;

    private int previousAmourValue = 50;
    private int previousSocialValue = 50;
    private int previousTravailValue = 50;

    [SerializeField]
    private Text scoreText; 

    private EnveloppeManager em;

    private void Start()
    {
        em = FindObjectOfType<EnveloppeManager>();
    }

    public void BuildScoreTexte()
    {
        ApplyBonusMalus();

        string ret = "Round Summary :\n\n";

        if (em.GetRemainingEnveloppesCount() == 0)
        {
            ret += "Well done, all the letters have been processed, you deserved a bonus:\n";
            ret += "+2 to all\n\n";
        }
        else
        {
            ret += "You didn't managed to process everything, you deserved a malus :\n";
            ret += "-" + em.GetRemainingEnveloppesCount() + " to all\n\n";
        }

        ret += "    Love : " + jaugeAmour.Value + "% (" + 
            (jaugeAmour.Value - previousAmourValue  ==  0 ? "=" : 
            jaugeAmour.Value - previousAmourValue > 0 ? "+"+ (jaugeAmour.Value - previousAmourValue)+"%" :
            (jaugeAmour.Value - previousAmourValue) + "%") + ")\n\n";
        ret += "    Social : " + jaugeSocial.Value + "% (" +
            (jaugeSocial.Value - previousSocialValue == 0 ? "=" :
            jaugeSocial.Value - previousSocialValue > 0 ? "+" + (jaugeSocial.Value - previousSocialValue) + "%" :
            (jaugeSocial.Value - previousSocialValue) + "%") + ")\n\n";
        ret += "    Work : " + jaugeTravail.Value + "% (" +
            (jaugeTravail.Value - previousTravailValue == 0 ? "=" :
            jaugeTravail.Value - previousTravailValue > 0 ? "+" + (jaugeTravail.Value - previousTravailValue) + "%" :
            (jaugeTravail.Value - previousTravailValue) + "%") + ")\n\n";
        ret += "\n\n\n";

        scoreText.text = ret;

        previousAmourValue = jaugeAmour.Value;
        previousSocialValue = jaugeSocial.Value;
        previousTravailValue = jaugeTravail.Value;
}

    private void ApplyBonusMalus()
    {
        if (em.GetRemainingEnveloppesCount() == 0)
        {
            jaugeAmour.Value += 2;
            jaugeSocial.Value += 2;
            jaugeTravail.Value += 2;
}
        else
        {
            jaugeAmour.Value -= em.GetRemainingEnveloppesCount();
            jaugeSocial.Value -= em.GetRemainingEnveloppesCount();
            jaugeTravail.Value -= em.GetRemainingEnveloppesCount();
        }
        FindObjectOfType<JaugeManager>().UpdateJauges();
    }

}
