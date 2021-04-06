using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player")]
    [SerializeField] float moveSpeed;
    [SerializeField] float LeftPaddingX, rightPaddingX, buttomPaddingY, topPaddingY ;
    [SerializeField] int health;
    [SerializeField] AudioClip PLayerDeath;
    [SerializeField] [Range(0, 1)] float DeathSoundVolime = 0.75f;
    [SerializeField] AudioClip HitSound;
    [SerializeField] GameObject shield;

    [SerializeField] float ShiledArmor = 1000;


    [Header("Projectile")]
    [SerializeField] GameObject purpleLazer;
    [SerializeField] float ProjectileSpeed;
    [SerializeField] float ProjectilefiringPeriod = 0.6f;
    [SerializeField] AudioClip ProjectileSound;

    [SerializeField] GameObject LazerRayHitEffect;
    [SerializeField] GameObject PlayerExplotion;
    [SerializeField] Transform ProjectilStartPosition1;
    [SerializeField] Transform ProjectilStartPosition2;
    [SerializeField] Transform ProjectilStartPosition3;


    [SerializeField] float DirectionLeft = 10;

    [SerializeField] float Directionright = 10;
    Level level;
     
    Coroutine firingCorotine;
    Coroutine FiringCorotineRight;
    Coroutine FiringCorotineLeft;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    public bool shiledOn = false;

    public float PowerUPTime;
    public bool thereShootOn = false;
    
    
    public float ShootDirction;


    private float Tpast;
    public Sprite Test;

    void Start()
    {
        
        Test = Resources.Load<Sprite>("Power Up");
    
        SetUpMoveBoundaries();
    }
    // Update is called once per frame
    void Update()
    {     
        Move();
        Fire(); 

    }

    IEnumerator TimeThreeShoorPower()
    {
        yield return new WaitForSeconds(PowerUPTime);
        thereShootOn = false;
               StopCoroutine(FiringCorotineLeft);
        StopCoroutine(FiringCorotineRight);
        Debug.Log("hallooo");
    }
    public void Timer()
    {
       
    }

    private void Fire()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(firingCorotine != null){
             
                StopCoroutine(firingCorotine);
                if (thereShootOn == true)
                {
                    StopCoroutine(FiringCorotineLeft);
                    StopCoroutine(FiringCorotineRight);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            firingCorotine =  StartCoroutine(FireContinuously());
            if (thereShootOn == true)
            {
                FiringCorotineLeft = StartCoroutine(FireContinuouslyLeft());
                FiringCorotineRight = StartCoroutine(FireContinuouslyRight());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(firingCorotine);
            if (thereShootOn == true)
            {
                StopCoroutine(FiringCorotineLeft);
                StopCoroutine(FiringCorotineRight);
            }
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject FroteEffect = gameObject.transform.GetChild(2).gameObject;
            FroteEffect.GetComponent<ParticleSystem>().Play();
            GameObject laser = Instantiate(purpleLazer, ProjectilStartPosition1.position, Quaternion.identity) as GameObject;

            Vector3 ShootPostion = ProjectilStartPosition2.position;

            laser.GetComponent<Projectile>().shootPostion = ShootPostion;
            laser.GetComponent<Projectile>().speed = ProjectileSpeed;
            laser.GetComponent<Projectile>().SetUp(DegreeToVector2(90), ProjectileSpeed);

            //laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position);
            yield return new WaitForSeconds(ProjectilefiringPeriod);
        }
    }

    IEnumerator FireContinuouslyLeft()
    {
        while (true)
        {
            GameObject FroteEffect = gameObject.transform.GetChild(0).gameObject;
            FroteEffect.GetComponent<ParticleSystem>().Play();
            GameObject laser = Instantiate(purpleLazer, ProjectilStartPosition2.position, Quaternion.identity) as GameObject;

            Vector3 ShootPostion = ProjectilStartPosition2.position;
            
            laser.GetComponent<Projectile>().shootPostion = ShootPostion;
            laser.GetComponent<Projectile>().speed = ProjectileSpeed;
            laser.GetComponent<Projectile>().SetUp(DegreeToVector2(DirectionLeft), ProjectileSpeed);
            Debug.Log(laser.GetComponent<Rigidbody2D>().velocity);
            Debug.Log(DegreeToVector2(DirectionLeft));
            Vector3 bulletdirection = DegreeToVector2(DirectionLeft);

            Vector3 ShootDirection = (ShootPostion.normalized + bulletdirection) - ShootPostion.normalized;

            laser.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 + GetAngelFromVector(ShootDirection)));
            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position);
            yield return new WaitForSeconds(ProjectilefiringPeriod);
        }
    }
    IEnumerator FireContinuouslyRight()
    {
        while (true)
        {
            GameObject FroteEffect = gameObject.transform.GetChild(1).gameObject;
            FroteEffect.GetComponent<ParticleSystem>().Play();
            GameObject laser = Instantiate(purpleLazer, ProjectilStartPosition3.position, Quaternion.identity) as GameObject;

            Vector3 ShootPostion = ProjectilStartPosition3.position;
      
            laser.GetComponent<Projectile>().shootPostion = ShootPostion;
            laser.GetComponent<Projectile>().speed = ProjectileSpeed;
            laser.GetComponent<Projectile>().SetUp(DegreeToVector2(Directionright), ProjectileSpeed);
            Debug.Log(laser.GetComponent<Rigidbody2D>().velocity);
            Debug.Log(DegreeToVector2(Directionright));
            Vector3 bulletdirection = DegreeToVector2(Directionright);
            
            Vector3 ShootDirection = (ShootPostion.normalized +bulletdirection ) - ShootPostion.normalized ;
   
            laser.transform.rotation = Quaternion.Euler(new Vector3(0, 0,-90+ GetAngelFromVector(ShootDirection)));

            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position);

            yield return new WaitForSeconds(ProjectilefiringPeriod);
        }
    }
    public float GetAngelFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax/1.2f);

        transform.position = new Vector2(newXPos, transform.position.y);
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + rightPaddingX;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - LeftPaddingX;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + buttomPaddingY;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - topPaddingY;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        CheackForSheildPowerUP(other);

        PowerUP powerUP = other.GetComponent<PowerUP>();

        if (powerUP != null)
        {
            switch (powerUP.Name)
            {
                case "3Shooter":
                    if (Input.GetMouseButton(0))
                    {
                        if (thereShootOn == false)
                        {
                            FiringCorotineLeft = StartCoroutine(FireContinuouslyLeft());
                            FiringCorotineRight = StartCoroutine(FireContinuouslyRight());
                        }
                    }
                    thereShootOn = true;
                   
                    StartCoroutine(TimeThreeShoorPower());
                    Destroy(other.gameObject);
                    break;

            }

        }


        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        Instantiate(LazerRayHitEffect, other.transform.position, Quaternion.identity);
        ProcesHit(damageDealer);
    }

    private void CheackForSheildPowerUP(Collider2D other)
    {
        ShieldPower PowerUpShields = other.GetComponent<ShieldPower>();

        if (PowerUpShields)
        {
            shiledOn = true;
            ShiledArmor = PowerUpShields.shield;
            Debug.Log("Set Shield active");
            shield.SetActive(true);
            Destroy(other.gameObject);
            // playerStates = PlayerStates.ShiledON;
        }
    }

    public void ProcesHit(DamageDealer damageDealer)
    {
        if (shiledOn == false)
        {
            health -= damageDealer.GetDamage();
            if (health >= 0)
            {
                FindObjectOfType<HealthDisplay>().Setheath();
            }
            else
            {
                FindObjectOfType<HealthDisplay>().SetHealth(0);
            }
        }else
        {
            ShiledArmor -= damageDealer.GetDamage();
            if (ShiledArmor <= 0)
            {
                shiledOn = false;
                shield.SetActive(false);
            }
        }

        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, DeathSoundVolime);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    public int Gethealth()
    {
        return health;
    }
    private void Die()
    {
        AudioSource.PlayClipAtPoint(PLayerDeath,Camera.main.transform.position, DeathSoundVolime);
        Instantiate(PlayerExplotion, transform.position, Quaternion.identity);
        FindObjectOfType<Level>().startWaitGameOver();
        Destroy(gameObject);       
    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

}
