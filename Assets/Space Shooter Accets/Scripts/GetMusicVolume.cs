using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetMusicVolume : MonoBehaviour
{

    Slider _Slider;

    void Start()
    {
        _Slider = GetComponent<Slider>();

        if (FindObjectOfType<MusicPlayer>())
        {

            _Slider.value = FindObjectOfType<MusicPlayer>().GetMusicVolume();
            Debug.Log("liser value" + _Slider.value);

        }
        else
        {

            Debug.LogError("Music player not find");

        }

        Debug.Log("liser value" + _Slider.value);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetMusicVolume(float volume)
    {
        FindObjectOfType<MusicPlayer>().SetMusicVolume(volume);
    }

}
