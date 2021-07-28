    
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
    [SerializeField] AudioClip HitSound;

    [SerializeField] [Range(0,1)]float DeathSoundVolime = 0.75f;
    [SerializeField] int ScoreValue = 20;
    [SerializeField] int powerUpDropRate = 50;
        
    [SerializeField] GameObject PowerUpDrop1;
    [SerializeField] GameObject PowerUpDrop2;
    [SerializeField] GameObject PowerUpDrop3;

    MusicPlayer musicPlayer;

    public void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
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
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, musicPlayer.GetEffectVolumeConvertet()*0.2f);
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
        AudioSource.PlayClipAtPoint(DeathSound,Camera.main.transform.position , musicPlayer.GetEffectVolumeConvertet());
        GameObject Explotions =  Instantiate(DeadExplotion, transform.position, Quaternion.identity);
       
        if (UnityEngine.Random.Range(0,100) < powerUpDropRate)
        {
            //   Debug.Log(UnityEngine.Random.Range(0, 100));
            int randmomNumber = UnityEngine.Random.Range(1, 4);
            switch (randmomNumber)
            {
                case 1:
                    Instantiate(PowerUpDrop1, transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(PowerUpDrop3, transform.position, Quaternion.identity);
                    break;
                case 3:

                    if (UnityEngine.Random.Range(0, 100) < powerUpDropRate)
                    {
                        Instantiate(PowerUpDrop2, transform.position, Quaternion.identity);
                    }

                 break;
            }
            /*
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                Instantiate(PowerUpDrop1, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(PowerUpDrop2, transform.position, Quaternion.identity);
            }
            */
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
        AudioSource.PlayClipAtPoint(clip, transform.position, musicPlayer.GetEffectVolumeConvertet());
    }
}
