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
    [SerializeField]
    private AudioClip stressMusicClip;

    private EnveloppeManager em;

    private void Start()
    {
        em = FindObjectOfType<EnveloppeManager>();
        timeRemaining = timeToWork;
    }

    void Update()
    {
        DisplayTimer();
        if (timerIsRunning.Value)
        {
            if(timeRemaining > 0)
            {

                if(timeRemaining <= stressMusicClip.length && 
                    audioSource.clip != stressMusicClip &&
                    em.GetRemainingEnveloppesCount() >= 4)
                {
                    audioSource.clip = stressMusicClip;
                    audioSource.Play();
                }

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
        if (timerIsRunning.Value == true) return;

        if (audioSource.clip != mainMusicClip)
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
