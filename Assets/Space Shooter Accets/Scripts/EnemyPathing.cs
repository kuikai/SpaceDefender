using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
     WaveConfig waveConfig;
     List<Transform> Waypoints;
     
    int waypointIndex = 0;

    void Start()
    {
        Waypoints = waveConfig.GetWayPoints();
        transform.position = Waypoints[waypointIndex].transform.position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        Waypoints = waveConfig.GetWayPoints();
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= Waypoints.Count - 1)
        {
            var targetPosition = Waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
         //   Debug.Log("target"+ targetPosition);
          //  Debug.Log("transform" + transform.position);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
