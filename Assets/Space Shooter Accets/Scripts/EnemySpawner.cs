using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
    [SerializeField] int startingWave = 0;

    private bool stopSpawing = false;
    /*
     IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }
    */

    public void Update()
    {
     //   Debug.Log(waveConfigs.Count);
    }

    public void StartWaves()
    {
        StartCoroutine(StratAllWaves());
    }

    public void SetLoopint(bool islooping)
    {
        looping = islooping;
    }
    IEnumerator StratAllWaves()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {      
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
           

            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesWave(currentWave));



            if (FindObjectOfType<levelSettings>().Endlevel == true)
            {
                stopSpawing = true;

                break;
                
            }
        }
    }

    private IEnumerator SpawnAllEnemiesWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNUmberOfEneime(); i++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }   
      
        if(stopSpawing == true){

            FindObjectOfType<Player>().PlayerEnd = true;
            
        }
     
    }

    private IEnumerator SpawmAllWavesRandom()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
             var currentWave = waveConfigs[Random.Range(0,waveConfigs.Count)];
             yield return StartCoroutine(SpawnAllEnemiesWave(currentWave));
        }
    }
}
