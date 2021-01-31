using System.Collections;
using System.Collections.Generic;
using Unity.Utils.PatternObserver;
using UnityEngine;

public class BetweenRoundManager : MonoBehaviour
{
    [SerializeField]
    private Sprite dayImage;
    [SerializeField]
    private Sprite nightImage;

    [SerializeField]
    private List<Sprite> imagesYeux;
    [SerializeField]
    private SpriteRenderer[] rendererYeux = new SpriteRenderer[1];

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private SignalSender startRound;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioClip magnetoClip;
    [SerializeField]
    private AudioClip startWorkClip;
    [SerializeField]
    private AudioClip musicClip;

    private IEnumerator AnimationDayPassing()
    {
        background.sprite = dayImage;

        //Shuffle de la liste
        for (int i = imagesYeux.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            Sprite tmp = imagesYeux[i];
            imagesYeux[i] = imagesYeux[r];
            imagesYeux[r] = tmp;
        }

        //Set up audio magneto
        musicSource.Stop();
        musicSource.volume *= 2.5f;
        musicSource.clip = magnetoClip;
        musicSource.loop = false;
        musicSource.Play();

        //Diffusion des images
        for (int i = 0; i < imagesYeux.Count; i++)
        {
            foreach(SpriteRenderer oeil in rendererYeux)
            {
                oeil.sprite = imagesYeux[i];
            }
            yield return new WaitForSeconds(0.2f);
        }
        //On reset le volume
        musicSource.volume /= 2.5f;

        //On remet le fond de base
        yield return new WaitForSeconds(0.2f);
        background.sprite = nightImage;

        //reset les sprite a null
        foreach (SpriteRenderer oeil in rendererYeux)
        {
            oeil.sprite = null;
        }
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(SetUpMusicPreparation());

        startRound.Raise();
    }

    public void DayPassing()
    {
        StartCoroutine(AnimationDayPassing());
    }

    private IEnumerator SetUpMusicPreparation()
    {
        musicSource.clip = startWorkClip;
        musicSource.Play();
        
        yield return new WaitForSeconds(startWorkClip.length);
        
        if(musicSource.clip == startWorkClip)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
