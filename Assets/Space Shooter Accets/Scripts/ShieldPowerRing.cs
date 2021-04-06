using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerRing : MonoBehaviour
{

    public float speed = 1;
    public int ShieldArmor = 200;
    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = FindObjectOfType<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(0, -speed/10);
        Move();
    }
    public void Move()
    {
        
    }

    
    
}
