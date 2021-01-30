using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float timeToWork = 60;
    private float timeRemaining = 0.0f;
    [SerializeField]
    private BooleanValue timerIsRunning;

    private void Start()
    {
        timeRemaining = timeToWork;
    }

    void Update()
    {
        if (timerIsRunning.Value)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning.Value = false;
            }
        }
    }
}
