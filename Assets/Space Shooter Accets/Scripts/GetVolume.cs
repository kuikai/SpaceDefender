using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetVolume : MonoBehaviour
{
    // Start is called before the first frame update

    Slider _Slider;
    
    void Start()
    {
        _Slider = GetComponent<Slider>();


        if (FindObjectOfType<MusicPlayer>())
        {

            _Slider.value = FindObjectOfType<MusicPlayer>().GetMasterVolume();
            Debug.Log("sdf value" + FindObjectOfType<MusicPlayer>().GetMasterVolume());
        }
        else
        {
            Debug.LogError("Music player not find");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainVolume(float volume)
    {
        FindObjectOfType<MusicPlayer>().MainVolume(volume);
    
    }

  
    
}

