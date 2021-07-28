using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{

    public AudioMixer mixer;

    static float MasterVolume = 0;
    static float MusicVolume =0;
    static float EffectVolume =0;
    public bool MusicOn = true;

    public float MV;

    
    
    

    // Start is called before the first frame update
    void Start()
    {
        MV = MasterVolume;
        
        SetUpSingleton();
        
        Debug.Log("musicPLayer");
        
        MainVolume(MasterVolume);
    }
    private void SetUpSingleton()
    {
      if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
           // Debug.Log("hallo!");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

   

    public void PlayAduioSocueOnPoint(Vector3 Poistion)
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        MV = MasterVolume;
    }

    public float GetMasterVolume()
    {
        return MasterVolume;
    }

    public float GetMusicVolume()
    {
        return MusicVolume;
    }

    public float GetEffectVolume()
    {
        return EffectVolume;
    }

    public void MainVolume(float SliderVulue)
    {
        MasterVolume = SliderVulue;
        mixer.SetFloat("Master volume", SliderVulue);
    }
    public void SetMusicVolume(float SliderVulue)
    {
        MusicVolume = SliderVulue;
        mixer.SetFloat("Music volume", SliderVulue);

    }
    public void SetEffectVolume(float SliderVulue)
    {
        EffectVolume = SliderVulue;
        mixer.SetFloat("Effect volume", SliderVulue);

    }
    public void ToggleMusic()
    {
        if(MusicOn == true)
        {
            MusicOn = false;
            mixer.SetFloat("Music volume", -80);
        }
        else
        {
            MusicOn = true;
            mixer.SetFloat("Music volume", MusicVolume);

        }
    }

    public float GetEffectVolumeConvertet()
    {

        float volume = 40;

        
        volume = volume + (EffectVolume + MasterVolume);

        Debug.Log("Master Volume" + MasterVolume);

        return volume / 40;
    }

  public static float GetVolume()
    {
        float volume = 40;
        volume = volume + EffectVolume;


        return volume / 40;
    }
    
}
