using System;
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
    private BooleanValue isGameRunning;
    [SerializeField]
    private BooleanValue isProcessingEnveloppe;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private SignalSender startRound;
    [SerializeField]
    private SignalSender gameOverSignal;

    private bool isGameOver;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        jaugeAmour.ResetValue();
        jaugeTravail.ResetValue();
        jaugeSocial.ResetValue();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if ((jaugeAmour.Value <= 0 ||
        jaugeTravail.Value <= 0 ||
        jaugeSocial.Value <= 0) && !isGameOver)
        {
            ManageGameEnd();
        }
    }

    public void StartRound()
    {
        isGameRunning.Value = true;
        isProcessingEnveloppe.Value = false;
        startRound.Raise();
    }

    private void StartGame()
    {
        isGameOver = false;
        startRound.Raise();
    }
    
    private void ManageGameEnd()
    {
        isGameOver = true;
        gameOverSignal.Raise();
        isGameRunning.Value = false;
        gameOverPanel.SetActive(true);
    }

}
