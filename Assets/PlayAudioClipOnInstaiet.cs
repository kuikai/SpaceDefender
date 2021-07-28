using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClipOnInstaiet : MonoBehaviour
{
   public  AudioClip AudioClip;

    MusicPlayer musicPlayer;
    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        AudioSource.PlayClipAtPoint(AudioClip, transform.position, musicPlayer.GetEffectVolumeConvertet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
