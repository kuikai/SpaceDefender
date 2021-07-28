using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEffectVolume : MonoBehaviour
{
    Slider _Slider;
    void Start()
    {
        _Slider = GetComponent<Slider>();

        if (FindObjectOfType<MusicPlayer>())
        {

            _Slider.value = FindObjectOfType<MusicPlayer>().GetEffectVolume();
            //Debug.Log("liser value" + _Slider.value);
        }
        else
        {
          //  Debug.LogError("Music player not find");
        }
        
    }


    public void SetEffect_Volume(float volume)
    {

        FindObjectOfType<MusicPlayer>().SetEffectVolume(volume);

    }

}
