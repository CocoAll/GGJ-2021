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

        musicSource.Stop();
        musicSource.volume *= 2.5f;
        musicSource.clip = magnetoClip;
        musicSource.Play();
        for (int i = 0; i < imagesYeux.Count; i++)
        {
            foreach(SpriteRenderer oeil in rendererYeux)
            {
                oeil.sprite = imagesYeux[i];
            }
            yield return new WaitForSeconds(0.2f);
        }
        musicSource.Stop();
        musicSource.volume /= 2.5f;
        yield return new WaitForSeconds(0.2f);
        background.sprite = nightImage;
        foreach (SpriteRenderer oeil in rendererYeux)
        {
            oeil.sprite = null;
        }
        yield return new WaitForSeconds(0.2f);
        startRound.Raise();
    }

    public void DayPassing()
    {
        StartCoroutine(AnimationDayPassing());
    }
}
