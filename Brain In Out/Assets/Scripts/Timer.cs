using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float timeToWork = 6000;
    private float timeRemaining = 0.0f;
    private bool timerIsRunning = true;

    private void Start()
    {
        timeRemaining = timeToWork;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
