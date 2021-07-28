using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 shootPostion;
    public Vector3 Direction;
    public float speed;
    void Start()
    {
        
    }
    public void SetUp(Vector2 Direction, float speed )
    {
        this.speed = speed;
        this.Direction = Direction;

    }
    // Update is called once per frame
    void Update()
    {
        transform.position += ((Direction / 50) * speed) * Time.deltaTime; ;
      //  Debug.Log(GetComponent<Rigidbody2D>().velocity);
    }
}
