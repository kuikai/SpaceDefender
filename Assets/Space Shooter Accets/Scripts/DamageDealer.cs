using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;

    Rigidbody2D RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }


}
