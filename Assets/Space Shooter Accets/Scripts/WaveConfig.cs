using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject PathPreFab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;


    public GameObject GetEnemyPrefab(){ return enemyPrefab;}

    public List<Transform> GetWayPoints() 
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform chiled in PathPreFab.transform)
        {
            waveWaypoints.Add(chiled);


        }
        return waveWaypoints; 
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNUmberOfEneime() { return numberOfEnemies; }

    public float GetSpeed() { return moveSpeed; }

}
