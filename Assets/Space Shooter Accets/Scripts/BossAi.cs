using SmallShips;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> IntoWaypoints;
    public List<Transform> WaypoinysLoob;
    public List<AudioClip> ProjectileAudioClips;
    public List<GameObject> Projectiles;
    [SerializeField] float Health = 100;
    [SerializeField] float speed = 100;
    [SerializeField] float VFXDeathTime = 1;
    [SerializeField] float TimeBetweenChangingFiringSequncen;
    [SerializeField] float AmoutFiringSequicen;
    [SerializeField] GameObject LazerRayHitEffect;
    [SerializeField] int powerUpDropRate =5;
    [SerializeField] GameObject PowerUpDrop1;
    [SerializeField] GameObject PowerUpDrop2;

    [SerializeField] AudioClip BoosGetHitSound;
    private Animator ani;
    private BaseBulletStarter baseBulletStarter;
    private int ComingInMovementIndex = 0;
    private int LoobIndex = 0;
    public bool MovementReteinOn = false;
    private bool Firing = true;
    private int FiringSequnceIndex =0;

    MusicPlayer musicPlayer;

    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        ani = GetComponent<Animator>();
        baseBulletStarter = GetComponent<BaseBulletStarter>();
        baseBulletStarter.OneShootOnePlace = true;
        baseBulletStarter.fireInSequence = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }


    public void SetHealth(int health)
    {
        Health = health;
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

        if (UnityEngine.Random.Range(0, 100) < powerUpDropRate)
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

        // hit sound 
        AudioSource.PlayClipAtPoint(BoosGetHitSound, transform.position, musicPlayer.GetEffectVolumeConvertet());
    }

    private void Die()
    {
        ani.SetBool("expl", true);
        //   FindObjectOfType<levelSettings>().LoadLevel1Scene();
        FindObjectOfType<Player>().PlayerEnd = true;
    }

 

    public void MoveMent()
    {
        if (ComingInMovementIndex <= IntoWaypoints.Count - 1)
        {
            var tagetPostion = IntoWaypoints[ComingInMovementIndex].transform.position;
            var movementthisFrame = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tagetPostion, movementthisFrame);
        
            if (transform.position == tagetPostion && MovementReteinOn == false)
            {
                if (ComingInMovementIndex == IntoWaypoints.Count - 1)
                {
                    Debug.Log("DON");
                    StartCoroutine(FireSequence());
                    MovementReteinOn = true;
                }
                ComingInMovementIndex++;
            }
        }

        if (MovementReteinOn == true)
        {
            Movingloob();
        }
    }


    public void Movingloob()
    {
        if (LoobIndex <= WaypoinysLoob.Count - 1)
        {
            Debug.Log(LoobIndex);
            var tagetPostion = WaypoinysLoob[LoobIndex].transform.position;
            var movementthisFrame = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tagetPostion, movementthisFrame);
            if (transform.position == tagetPostion)
            {
                bool Isretting = false;

                if (LoobIndex == WaypoinysLoob.Count - 1)
                {
                    Debug.Log(WaypoinysLoob.Count - 1);
                    Debug.Log("rest");
                    LoobIndex = 0;
                    Isretting = true;
                }
                if (!Isretting)
                {
                    LoobIndex++;
                }
            }   
        }
    }


   IEnumerator FireSequence()
    {
        baseBulletStarter.StartRepeateFire();
        while (Firing)
        {

            FiringSequnceIndex++;
            switch (FiringSequnceIndex)
            {
                case 1:
                    baseBulletStarter.OneShootOnePlace = true;
                    
                    break;
                case 2:
                    baseBulletStarter.bulletPrefab = Projectiles[0];
                   // AudioSource.PlayClipAtPoint(ProjectileAudioClips[0], transform.position, musicPlayer.GetEffectVolumeConvertet());
                    baseBulletStarter.OneShootOnePlace = false;
                    break;
                case 3:
                    baseBulletStarter.bulletPrefab = Projectiles[1];
                    baseBulletStarter.fireInSequence = true;
                    baseBulletStarter.fireDelay = 0.3F;
                   // AudioSource.PlayClipAtPoint(ProjectileAudioClips[1], transform.position, musicPlayer.GetEffectVolumeConvertet());
                    break;
                case 4:
                    baseBulletStarter.bulletPrefab = Projectiles[2];
                    baseBulletStarter.fireInSequence = false;
                    baseBulletStarter.OneShootOnePlace = true;
                    baseBulletStarter.fireDelay = 3;
                   // AudioSource.PlayClipAtPoint(ProjectileAudioClips[2], transform.position, musicPlayer.GetEffectVolumeConvertet());
                    break;
                default:
                    break;
            }

            Debug.Log(FiringSequnceIndex);
           
            yield return new WaitForSeconds(TimeBetweenChangingFiringSequncen);
            if (FiringSequnceIndex == AmoutFiringSequicen)
            {
                baseBulletStarter.fireInSequence = false;
                FiringSequnceIndex = 0;
            }
        }
    }
}
