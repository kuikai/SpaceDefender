using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelSettings : MonoBehaviour
{
    [SerializeField] GameObject Spawner;
    public float ScoreBossInstanceOrWin = 1000;
    [SerializeField] GameObject Bigboss;
    [SerializeField] Transform bossStartPosition;
    [SerializeField] int TimeToloadLevel1 = 3;
    [Header("Boss set Up")]
    [SerializeField] bool haveBoos;
    [SerializeField] int BossHealth;
    [Header("Level settings")]
    public float LevelTime = 5;
    private float levelStartScore;
    private float scoretowin;
    private GameSession gameSession;
    private bool BigBozzOn = false;
    public float test = 5;

    Player player;
    public float time;

    private int sceneIndex;
    private int loadTime;
    // gonna be privarelate
    public bool Endlevel = false;

    

    void Start()
    {
        
        player = FindObjectOfType<Player>();
        Endlevel = false;
        gameSession = FindObjectOfType<GameSession>();
        levelStartScore = FindObjectOfType<GameSession>().CorrentScore;
        scoretowin = ScoreBossInstanceOrWin + levelStartScore;

    }

   public bool GetBigBossOn()
   {
        return haveBoos;   
   }

    public void LoadLevel(int _sceneIndex, int _loadTime)
    {
        sceneIndex = _sceneIndex;
        loadTime = _loadTime;
        StartCoroutine(Loadlevel());

    }

    IEnumerator Loadlevel()
    {
        yield return new WaitForSeconds(loadTime);

        SceneManager.LoadScene(sceneIndex);

    }
    public void LoadLevel1Scene()
    {

        StartCoroutine(Loadlevel1());

    }
    IEnumerator Loadlevel1()
    {
        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(1);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name.ToString());

        time += Time.deltaTime;

        if(time > LevelTime)
        {
            Endlevel = true;
            if(SceneManager.GetActiveScene().name == "BossLevel")
            {
                InstanstBigBossTime();
                
            }
            else
            {
                EndLevel();
            }
        }
    }
    
    public void EndLevel()
    {
        FindObjectOfType<EnemySpawner>().SetLoopint(false);
        if(GameObject.FindGameObjectWithTag("Enemy") == false && player.PlayerEnd ==false)
        {
            FindObjectOfType<Player>().PlayerEnd = true;
        }
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CheakScoresStatus()
    {
        if (scoretowin <= gameSession.CorrentScore)
        {
            if (haveBoos)
            {
            //    Debug.Log("BIg bozztime");
                Spawner.SetActive(false);
                Instantiate(Bigboss, bossStartPosition.position, Quaternion.identity);
                haveBoos = false;
            }
        }
    }



    public void InstanstBigBossTime()
    {
        if (haveBoos)
        {
            Debug.Log("BIg bozztime");
            Spawner.SetActive(false);
            GameObject BigBoss =  Instantiate(Bigboss, bossStartPosition.position, Quaternion.identity);
            FindObjectOfType<BossAi>().SetHealth(BossHealth);
            haveBoos = false;
        }
    }



    public void loadNextlevel()
    {

    }



}
