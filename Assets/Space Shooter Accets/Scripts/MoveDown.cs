using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    Rigidbody2D RD;
    public float speed;
    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RD.velocity = new Vector2(0, -speed/200);
    }
}
