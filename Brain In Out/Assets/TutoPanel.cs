using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPanel : MonoBehaviour
{
    public List<Sprite> tutos = new List<Sprite>();
    public SpriteRenderer imageTarget;
    int index;

    private void Start()
    {
        index = 0;
        if (tutos.Count != 0)
        {
            imageTarget.sprite = tutos[index];
        }        
    }

    public void Next()
    {
        if(index + 1 < tutos.Count)
        {
            index++;
            imageTarget.sprite = tutos[index];
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Previous()
    {
        if (index - 1 >= 0)
        {
            index--;
            imageTarget.sprite = tutos[index];
        }
    }
}
