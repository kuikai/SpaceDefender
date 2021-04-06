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
    public List<GameObject> Projectiles;
    [SerializeField] float Health = 100;
    [SerializeField] float speed = 100;
    [SerializeField] float VFXDeathTime = 1;
    [SerializeField] float TimeBetweenChangingFiringSequncen;
    [SerializeField] float AmoutFiringSequicen;
    [SerializeField] GameObject LazerRayHitEffect;
    private Animator ani;
    private BaseBulletStarter baseBulletStarter;
    private int ComingInMovementIndex = 0;
    private int LoobIndex = 0;
    public bool MovementReteinOn = false;
    private bool Firing = true;
    private int FiringSequnceIndex =0;
    void Start()
    {
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
        ani.SetBool("expl", true);
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
                    baseBulletStarter.OneShootOnePlace = false;
                    break;
                case 3:
                    baseBulletStarter.bulletPrefab = Projectiles[1];
                    baseBulletStarter.fireInSequence = true;
                    baseBulletStarter.fireDelay = 0.3F;
                    break;
                case 4:
                    baseBulletStarter.bulletPrefab = Projectiles[2];
                    baseBulletStarter.fireInSequence = false;
                    baseBulletStarter.OneShootOnePlace = true;
                    baseBulletStarter.fireDelay = 3;
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
