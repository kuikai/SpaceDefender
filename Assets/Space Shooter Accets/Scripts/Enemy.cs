    
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShoots;
    [SerializeField] float maxTimeBetweenShoots;
    [SerializeField] float ProjitileSpeed;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject LazerRayHitEffect;
    [SerializeField] GameObject DeadExplotion;
    [SerializeField] float VFXDeathTime  =1;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0,1)]float DeathSoundVolime = 0.75f;
    [SerializeField] int ScoreValue = 20;

    [SerializeField] int powerUpDropRate = 50;

    [SerializeField] GameObject PowerUpDrop1;
    [SerializeField] GameObject PowerUpDrop2;
      
    public void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {    
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        GameObject LazerHit = Instantiate(LazerRayHitEffect, other.transform.position, Quaternion.identity);
        Destroy(LazerHit, VFXDeathTime);
        ProcessHit(damageDealer);
       
    }
    
    private void ProcessHit(DamageDealer damageDealer)
    {
      
        Health -= damageDealer.GetDamage();
        damageDealer.Hit();       
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        FindObjectOfType<GameSession>().AddScore(50);
        AudioSource.PlayClipAtPoint(DeathSound,Camera.main.transform.position , DeathSoundVolime);
        GameObject Explotions =  Instantiate(DeadExplotion, transform.position, Quaternion.identity);
        
       
        if (UnityEngine.Random.Range(0,100) < powerUpDropRate)
        {
         //   Debug.Log(UnityEngine.Random.Range(0, 100));
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                Instantiate(PowerUpDrop1, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(PowerUpDrop2, transform.position, Quaternion.identity);
            }

        }
        Destroy(Explotions, VFXDeathTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            shoot();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
        } 
    }

    private void shoot()
    {
        GameObject EnemyProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        EnemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjitileSpeed);
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
