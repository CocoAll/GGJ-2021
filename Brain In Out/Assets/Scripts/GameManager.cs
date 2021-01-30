using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private IntValue jaugeAmour;
    [SerializeField]
    private IntValue jaugeTravail;
    [SerializeField]
    private IntValue jaugeSocial;

    [SerializeField]
    private SignalSender startRound;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of Game Manager");
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update of Game Manager");
        if (jaugeAmour.Value <= 0 ||
        jaugeTravail.Value <= 0 ||
        jaugeSocial.Value <= 0)
        {
            //Game Over
        }
    }

    public void StartRound()
    {
        Debug.Log("StartRound of Game Manager");
        startRound.Raise();
    }

    private void ManageRoundEnd()
    {

    }
    
    private void ManageGameEnd()
    {

    }


}
