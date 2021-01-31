using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public void UpdateListenerVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
