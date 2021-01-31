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
    private AudioClip jingleAmour;
    [SerializeField]
    private AudioClip jingleTravail;
    [SerializeField]
    private AudioClip jingleSocial;
    [SerializeField]
    private CurrentEnveloppe currentEnveloppe;


    public void PlayGameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverClip);
    }

    public void PlayEndOfRound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(endOfRoundClip);
    }

    public void PlayEnveloppeJingle()
    {
        audioSource.Stop();
        switch (currentEnveloppe.value.typeEnveloppe)
        {
            case TypeEnum.AMOUR:
                audioSource.PlayOneShot(jingleAmour);
                break;
            case TypeEnum.SOCIAL:
                audioSource.PlayOneShot(jingleSocial);
                break;
            case TypeEnum.TRAVAIL:
                audioSource.PlayOneShot(jingleTravail);
                break;
        }
    }

}
