using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
   public int CorrentScore;
    levelSettings LevelSettings;
   private void Awake()
    {
        LevelSettings = FindObjectOfType<levelSettings>();
        int num = FindObjectsOfType(GetType()).Length;
        if (num > 1)
        {
            Debug.Log("new gamesecces");
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int points)
    {
       
        CorrentScore += points;
        LevelSettings.CheakScoresStatus();
        // CorrentScore = CorrentScore += points;
       // Debug.Log("addAcore" + CorrentScore);
    }
    public void setScoreTo0()
    {
        CorrentScore = 0;
    }
    public int GetScore()
    {
        return CorrentScore;
    }
    // Update is called once per frame
    
}
