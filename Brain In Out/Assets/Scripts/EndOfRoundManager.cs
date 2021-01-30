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
        string ret = "Score du round :\n\n";
        ret += "Score of Love at the beggining of the round : " + previousAmourValue + " - Current Score :" + jaugeAmour.Value + "\n\n";
        ret += "Score of Social at the beggining of the round : " + previousSocialValue + " - Current Score :" + jaugeSocial.Value + "\n\n";
        ret += "Score of Work at the beggining of the round : " + previousTravailValue + " - Current Score :" + jaugeTravail.Value + "\n\n";
        ret += "\n\n\n";
        if(em.GetRemainingEnveloppesCount() == 0)
        {
            ret += "Well done, all the letters have been processed, you deserved a bonus:\n";
            ret += "+2 to all gauges";
        }
        else
        {
            ret += "You didn't managed to process everything, you deserved a malus :\n";
            ret += "-" + em.GetRemainingEnveloppesCount() + " to all gauges";
        }
        scoreText.text = ret;
    }

    public void ApplyBonusMalus()
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
