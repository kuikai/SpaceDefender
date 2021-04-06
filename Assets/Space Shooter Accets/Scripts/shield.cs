using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{

    public float Armor = 100;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return;}
        ProccesHit(damageDealer);    
    }

    public void ProccesHit(DamageDealer damageDealer)
    {
        Armor -= damageDealer.GetDamage();
        damageDealer.Hit();
    }
    
}
