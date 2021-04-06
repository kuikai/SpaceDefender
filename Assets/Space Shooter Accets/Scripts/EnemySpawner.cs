using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
    [SerializeField] int startingWave = 0;

     IEnumerator Start()
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
