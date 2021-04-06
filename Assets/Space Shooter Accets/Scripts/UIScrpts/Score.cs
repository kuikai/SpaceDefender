using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    // Start is called before the first frame update
   


     TextMeshProUGUI scoreText;
      GameSession gameSession;

 
    void Start()
    {
     
        scoreText = GetComponent<TextMeshProUGUI>();

        gameSession = FindObjectOfType<GameSession>();
       
    }

    public void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
     //   Debug.Log(gameSession.CorrentScore);
    }

    public void ResetScore()
    {
       

    }
  
    public void setScoreTo0()
    {
        scoreText.text = "0";
    }

    // Update is called once per frame
    public void AddToScore()
    {
    
    }

    public void GameOverScoreShow()
    {

    }
}
