using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;
using UnityEngine.UI;

public class LettreManager : MonoBehaviour
{
    [Header("UI Go")]
    [SerializeField]
    private GameObject lettreObject;
    [SerializeField]
    private GameObject enveloppeObject;
    [SerializeField]
    private Button remettreButton;
    [SerializeField]
    private Button refouleButton;

    [Header("Divers Scriptable Object")]
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;

    [SerializeField]
    private BooleanValue isProcessingEnveloppe;
    [SerializeField]
    private BooleanValue isGameRunning;

    [SerializeField]
    private SignalSender onRoundEnd;

    private EnveloppeManager enveloppeManager;

    [Header("Enveloppe/Lettre visuals")]
    [SerializeField]
    private GameObject tamponAmour;
    [SerializeField]
    private GameObject tamponTravail;
    [SerializeField]
    private GameObject tamponSocial;
    [SerializeField]
    private Text enveloppeTexte;
    [SerializeField]
    private Text lettreTexte;

    [Header("Jauges values")]
    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    private List<string> namesList = new List<string> { "Alex", "Sam", "Camille", "Charlie", "Sasha", "Noor", "Amal", "Chams", "Billy", "Cameron", "Elliott", "Gabriel", "Jesse", "Reese", "Azato'th" };
    private List<string> friendsNamesList = new List<string> { "Amine", "Louis", "Theo", "Corentin", "Anna", "Anjuna", "Moïra", "Louise", "Paul", "Redoine", "Arnaud", "Jean-Michel Jam", "Nassim", "Thery", "Victor", "Ella", "Elisabeth", "Veronica", "Felix", "Kylian", "Anissa", "Loana", "Caroline", "Toby", "Marie", "Jeanne" };
    private List<string> irritatingBehaviourList = new List<string> { "broke a plate", "cooked the same meal", "forgot to turn off the lights", "chatted with that girl", "chatted with that boy", "used comic sans", "didn't answer me", "stepped on my foot", "made a bad joke" };
    private List<string> giftList = new List<string> { "a video game", "a pikachu plush", "2 tickets for the Stunfest festival", "a crowbar", "a flag of French Brittany", "a ring to rule them all", "a cupcake", "a kazoo", "a soda can", "a shoe from one of the neighbors' children" };
    private List<string> storeMerchList = new List<string> { "fish", "shoe", "DVD", "poster", "video game", "hair dryer", "sauce", "phone", "needle", "blanket", "glass", "balloon" };
    private List<string> videoThemeList = new List<string> { "cat", "dog", "animal", "tiktok", "compilation", "pewdiepie", "unboxing", "minecraft", "political", "fail" };
    private List<string> outdoorActivityList = new List<string> { "airport", "mall", "movies", "funfair", "gym", "beach" };
    private List<string> secretList = new List<string> { " do ASMR videos", " like the taste of dog food", " pass out when I eat tofu", " don't know the Lord of the Rings", "'ve never played World of Warcraft", " hate Halloween", " like to drink other peoples' tears", " hate jazz music", " made up the whole story about meeting Toby Fox" };
    private string friendNameTemp;
    bool sameFriendName = false;

    private void Start()
    {
        enveloppeManager = FindObjectOfType<EnveloppeManager>();
        lettreObject.SetActive(false);
        enveloppeObject.SetActive(false);
    }

    //
    //Actions sur l'enveloppe
    //
    public void OuvrirLettre()
    {
        enveloppeObject.SetActive(false);
        SetUpLettre();
    }

    public void RemettreLettre()
    {
        isProcessingEnveloppe.Value = false;
        enveloppeManager.ReinsertCurrentLetter();
        enveloppeObject.SetActive(false);
    }

    public void RefoulerLettre()
    {
        enveloppeManager.RefouleCurrentLetter();
        isProcessingEnveloppe.Value = false;
        CheckRoundState();
        enveloppeObject.SetActive(false);
    }

    //
    //Actions sur la lettre
    //
    public void FermerLettre()
    {
        isProcessingEnveloppe.Value = false;
        CheckRoundState();
        lettreObject.SetActive(false);
    }

    //
    //Set up des données
    //
    public void SetUpEnveloppe()
    {
        TypeEnum tee = currentEnveloppe.value.typeEnveloppe;
        switch (tee)
        {
            case TypeEnum.AMOUR:
                tamponAmour.SetActive(true);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(false);
                break;
            case TypeEnum.TRAVAIL:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(true);
                tamponSocial.SetActive(false);
                break;
            case TypeEnum.SOCIAL:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(true);
                break;
            case TypeEnum.NEUTRE:
                tamponAmour.SetActive(false);
                tamponTravail.SetActive(false);
                tamponSocial.SetActive(false);
                break;
        }

        //Désactions des interactions des boutons si nécéssaires
        if (currentEnveloppe.value.refoule)
        {
            remettreButton.interactable = false;
            refouleButton.interactable = false;
        }else if (currentEnveloppe.value.remiseDansLeTas)
        {
            remettreButton.interactable = false;
        }
        

        //Reset des bouttons si nécéssaires
        if (remettreButton.interactable == false && !currentEnveloppe.value.remiseDansLeTas)
        {
            remettreButton.interactable = true;
        }
        if (refouleButton.interactable == false && !currentEnveloppe.value.refoule)
        {
            refouleButton.interactable = true;
        }

        enveloppeTexte.text = FillRandomTextElements(true);

        enveloppeObject.SetActive(true);
    }

    public void SetUpLettre()
    {
        lettreTexte.text = FillRandomTextElements();
        lettreObject.SetActive(true);
    }

    //
    //Action de gestion fin de round
    //
    public void CheckRoundState()
    {
        if(enveloppeManager.GetRemainingEnveloppesCount() == 0)
        {
            isGameRunning.Value = false;
            onRoundEnd.Raise();
        }
    }

    public void OnRoundEnd()
    {
        lettreObject.SetActive(false);
        enveloppeObject.SetActive(false);
    }

    private string FillRandomTextElements(bool titre = false)
    {
        string letterText;
        if (titre)
            letterText = currentEnveloppe.value.titre;
        else
            letterText = currentEnveloppe.value.contenu;
        // crushName
        if (letterText.Contains("{crushName1}"))
        {
            string randomName = namesList[Random.Range(0, namesList.Count)];
            namesList.Remove(randomName);
            letterText = letterText.Replace("{crushName1}", randomName);
            randomName = namesList[Random.Range(0, namesList.Count)];
            namesList.Remove(randomName);
            if (namesList.Count == 0)
                namesList = new List<string> { "Alex", "Sam", "Camille", "Charlie", "Sasha", "Noor", "Amal", "Chams", "Billy", "Cameron", "Elliott", "Gabriel", "Jesse", "Reese", "Azato'th" };
            letterText = letterText.Replace("{crushName2}", randomName);

        }
        if (letterText.Contains("{friendName}"))
        {
            if (titre)
            {
                sameFriendName = true;
                friendNameTemp = friendsNamesList[Random.Range(0, friendsNamesList.Count)];
                letterText = letterText.Replace("{friendName}", friendNameTemp);
            }
            else if (sameFriendName)
            {
                sameFriendName = false;
                letterText = letterText.Replace("{friendName}", friendNameTemp);
            }
            else
            {
                string randomName = friendsNamesList[Random.Range(0, friendsNamesList.Count)];
                letterText = letterText.Replace("{friendName}", randomName);
            }
        }
        if (letterText.Contains("{videoTheme}"))
        {
            string randomVideoTheme = videoThemeList[Random.Range(0, videoThemeList.Count)];
            letterText = letterText.Replace("{videoTheme}", randomVideoTheme);
        }
        if (letterText.Contains("{activity}"))
        {
            string randomActivity = outdoorActivityList[Random.Range(0, outdoorActivityList.Count)];
            letterText = letterText.Replace("{activity}", randomActivity);
        }
        if (letterText.Contains("{secret}"))
        {
            string randomSecret = secretList[Random.Range(0, secretList.Count)];
            letterText = letterText.Replace("{secret}", randomSecret);
        }
        if (letterText.Contains("{gift}"))
        {
            string randomSecret = giftList[Random.Range(0, giftList.Count)];
            letterText = letterText.Replace("{gift}", randomSecret);
        }
        if (letterText.Contains("{irritatingBehaviour}"))
        {
            string randomSecret = irritatingBehaviourList[Random.Range(0, irritatingBehaviourList.Count)];
            letterText = letterText.Replace("{irritatingBehaviour}", randomSecret);
        }
        if (letterText.Contains("{storeMerch}"))
        {
            string randomSecret = storeMerchList[Random.Range(0, storeMerchList.Count)];
            letterText = letterText.Replace("{storeMerch}", randomSecret);
        }
        return letterText;
    }
}
