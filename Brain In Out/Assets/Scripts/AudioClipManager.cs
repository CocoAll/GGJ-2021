using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameOverClip;
    [SerializeField]
    private AudioClip endOfRoundClip;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buttonClick;

    [SerializeField]
    private AudioClip jingleAmour;
    [SerializeField]
    private AudioClip jingleTravail;
    [SerializeField]
    private AudioClip jingleSocial;
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;


    public void PlayGameOver()
    {
        audioSource.clip = gameOverClip;
        audioSource.Play();
    }

    public void PlayEndOfRound()
    {
        audioSource.clip = endOfRoundClip;
        audioSource.Play();
    }

    public void PlayEnveloppeJingle()
    {
        switch (currentEnveloppe.value.typeEnveloppe)
        {
            case TypeEnum.AMOUR:
                audioSource.clip = jingleAmour;
                break;
            case TypeEnum.SOCIAL:
                audioSource.clip = jingleSocial;
                break;
            case TypeEnum.TRAVAIL:
                audioSource.clip = jingleTravail;
                break;
        }
        audioSource.Play();
    }

    public void PlayButtonClick()
    {
        if ((audioSource.clip == endOfRoundClip || audioSource.clip == gameOverClip) && audioSource.isPlaying)
            return;

        audioSource.clip = buttonClick;
        audioSource.Play();
    }

}
