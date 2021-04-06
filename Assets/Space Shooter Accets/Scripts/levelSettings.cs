using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSettings : MonoBehaviour
{
    [SerializeField] GameObject Spawner;
    [SerializeField] float ScoreBossInstanceOrWin = 1000;
    [SerializeField] GameObject Bigboss;
    [SerializeField] Transform bossStartPosition;
    [SerializeField] bool haveBoos;
    private float levelStartScore;
    private float scoretowin;
    private GameSession gameSession;
    private bool BigBozzOn = false;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        levelStartScore = FindObjectOfType<GameSession>().CorrentScore;

        scoretowin = ScoreBossInstanceOrWin + levelStartScore;
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    
    public void CheakScoresStatus()
    {
        if (scoretowin <= gameSession.CorrentScore)
        {
            if (haveBoos)
            {
                Debug.Log("BIg bozztime");
                Spawner.SetActive(false);
                Instantiate(Bigboss, bossStartPosition.position, Quaternion.identity);
                haveBoos = false;

            }
        }
    }
}
