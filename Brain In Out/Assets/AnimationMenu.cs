using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationMenu : MonoBehaviour
{
    public List<Sprite> imagesEnimations;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            foreach(Sprite img in imagesEnimations)
            {
                yield return new WaitForSeconds(0.4f);
                image.sprite = img;
            }
        }
    }
    
}
