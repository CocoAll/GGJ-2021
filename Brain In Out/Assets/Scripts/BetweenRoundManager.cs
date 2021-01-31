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
    private SpriteRenderer[] rendererYeux = new SpriteRenderer[2];

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private SignalSender startRound;

    private IEnumerator AnimationDayPassing()
    {
        background.sprite = dayImage;
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < imagesYeux.Count; i++)
        {
            foreach(SpriteRenderer oeil in rendererYeux)
            {
                oeil.sprite = imagesYeux[i];
            }
            yield return new WaitForSeconds(0.2f);
        }
        background.sprite = nightImage;
        foreach (SpriteRenderer oeil in rendererYeux)
        {
            oeil.sprite = null;
        }
        startRound.Raise();
    }

    public void DayPassing()
    {
        StartCoroutine(AnimationDayPassing());
    }
}
