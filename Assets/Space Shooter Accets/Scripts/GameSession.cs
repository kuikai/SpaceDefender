using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
   public int CorrentScore;
   levelSettings LevelSettings;
   public int tal = 0;
    
   private void Start()
   {
        Debug.Log(tal);
        tal++;
        if (LevelSettings == null)
        {
            Debug.Log("no level settings");
        }
        else
        {
            Debug.Log(LevelSettings.test);
        }
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
        if (LevelSettings == null)
        {
            LevelSettings = FindObjectOfType<levelSettings>();
            LevelSettings.CheakScoresStatus();
        }
        else
        {
            LevelSettings.CheakScoresStatus();
        }
    
        CorrentScore += points;

        FindObjectOfType<Score>().SetScore(CorrentScore);
      //  Debug.Log(CorrentScore);

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
    


    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    }
