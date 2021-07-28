using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
  
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level1");    
        FindObjectOfType<GameSession>().setScoreTo0();
     
    }

    public void loadHighScoreScene()
    {
        SceneManager.LoadScene("HighScore");
    }
    public void LoadGameover()
    {

        SceneManager.LoadScene("GameOver");

    }
    public void LoadStratMenu()
    {
        SceneManager.LoadScene("StartMenu");
        //  FindObjectOfType<Score>().ResetScore();
        GameSession.ResumeGame();
    }
    public void LoadOption()
    {
        SceneManager.LoadScene("Options");
    }
    public void loadQuitGame()
    {
        Application.Quit();
    }
    public void loadSceneBuyName(string name)
    {
        
    }

    public void startWaitGameOver()
    {
        StartCoroutine(GameOverWait());
    }
    IEnumerator GameOverWait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }
}
