using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float timeToWork = 60;
    private float timeRemaining = 0.0f;
    [SerializeField]
    private BooleanValue timerIsRunning;

    [SerializeField]
    private SignalSender onRoundEnd;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip mainMusicClip;

    private void Start()
    {
        timeRemaining = timeToWork;
    }

    void Update()
    {
        DisplayTimer();
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
                onRoundEnd.Raise();
            }
        }
    }

    public void ResetTimer()
    {
        timeRemaining = timeToWork;
    }

    public void StartTimer()
    {
        if(audioSource.clip != mainMusicClip)
        {
            audioSource.Stop();
            audioSource.clip = mainMusicClip;
            audioSource.Play();
        }
        timerIsRunning.Value = true;
    }

    private void DisplayTimer()
    {
        string timeRemainingFormatted = TimeSpan.FromSeconds(timeRemaining).ToString(@"mm\:ss");
        timerText.text = "WAKE UP IN\n" + timeRemainingFormatted;
    }
}
