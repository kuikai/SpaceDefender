using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMusic : MonoBehaviour
{

    Toggle Toggle_Music;
    // Start is called before the first frame update
    void Start()
    {

    }

    


    public void Toggle_GameMusic()
    {
      
            FindObjectOfType<MusicPlayer>().ToggleMusic();
        
       


    }
}
