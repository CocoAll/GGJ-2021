using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private SignalSender startRound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartRound()
    {
        startRound.Raise();
    }

    private void ManageGameEnd()
    {

    }
}
