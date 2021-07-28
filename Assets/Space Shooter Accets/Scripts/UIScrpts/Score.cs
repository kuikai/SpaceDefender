using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        SetScore(FindObjectOfType<GameSession>().CorrentScore);
        HighScore.AddScore(FindObjectOfType<GameSession>().CorrentScore);
    }
    

    public void ResetScore()
    {
        scoreText.text = "0";

    }
    public void SetScore(int score)
    {

        scoreText.text = score.ToString();
    }
  
}
