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

}
