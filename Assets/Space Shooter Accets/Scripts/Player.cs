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
    levelSettings levelSettings; 

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

    public bool RunningIntro = true;
    public bool RunningoutTro = false;

    GameCanvas GameCanvas;

    MusicPlayer musicPlayer;
    public bool PlayerEnd = false;

    private bool BossInlevel;
    private bool intro = true;


    float Time2 = 0;
    void Start()
    {
        levelSettings = FindObjectOfType<levelSettings>();
        BossInlevel = levelSettings.GetBigBossOn();

        musicPlayer = FindObjectOfType<MusicPlayer>();
        RunningoutTro = false;
        RunningIntro = false;
        GameCanvas = FindObjectOfType<GameCanvas>();
        Test = Resources.Load<Sprite>("Power Up");
        SetUpMoveBoundaries();
    }


    public void OutTro()
    {
        float step = 1 * Time.deltaTime; // calculate distance to move

        float playerXPosition = transform.position.x;

        if(playerXPosition > 2.7  &&  playerXPosition < 2.9)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.8f, 11, 0), 5 * Time.deltaTime);
        
            if(transform.position.y > 10.5)
            {
                if(BossInlevel == true)
                {
                    levelSettings.LoadLevel(1, 1);                
                }
                else
                {
                    levelSettings.loadNextLevel();
                }
            }
        }
        else   
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.8f, transform.position.y, 0), step);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerEnd == false)
        {
            Move();
        }
        else
        {
            OutTro();
        }
        if (!GameCanvas.IsPausePanelActiv && Time2 > 2)
        {
            Fire();
        }

        Time2 += Time.deltaTime;


    }

    IEnumerator TimeThreeShoorPower()
    {
        yield return new WaitForSeconds(PowerUPTime);
        thereShootOn = false;
        if (FiringCorotineLeft != null)
        {
            StopCoroutine(FiringCorotineLeft);
        }
        if(FiringCorotineRight != null)
        {
            StopCoroutine(FiringCorotineRight);
        }

        Debug.Log("hallooo");
    }
    public void Timer()
    {
       
    }
    public void PlayerDonIntro()
    {
        RunningoutTro = false;
        FindObjectOfType<EnemySpawner>().StartWaves();
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
            if(firingCorotine != null)
            {
                StopCoroutine(firingCorotine);
            }
  
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

            float EffectVolume = FindObjectOfType<MusicPlayer>().GetEffectVolume();
            EffectVolume = EffectVolume * -1;
            EffectVolume = EffectVolume / 40;
            //Debug.Log("Effect Voume" +EffectVolume);
            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position,musicPlayer.GetEffectVolumeConvertet());
            

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

            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position, musicPlayer.GetEffectVolumeConvertet());

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
        //    Debug.Log(laser.GetComponent<Rigidbody2D>().velocity);
           // Debug.Log(DegreeToVector2(Directionright));
            Vector3 bulletdirection = DegreeToVector2(Directionright);
            
            Vector3 ShootDirection = (ShootPostion.normalized +bulletdirection ) - ShootPostion.normalized ;
   
            laser.transform.rotation = Quaternion.Euler(new Vector3(0, 0,-90+ GetAngelFromVector(ShootDirection)));

            AudioSource.PlayClipAtPoint(ProjectileSound, transform.position, musicPlayer.GetEffectVolumeConvertet());

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
                    AudioSource.PlayClipAtPoint(other.GetComponent<PowerUP>().PowerUPicUpSound, Camera.main.transform.position, musicPlayer.GetEffectVolumeConvertet());
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

                case "HealthPack":
                    AudioSource.PlayClipAtPoint(other.GetComponent<PowerUP>().PowerUPicUpSound, Camera.main.transform.position, musicPlayer.GetEffectVolumeConvertet());

                    if (health >= 200)
                    {
                        health = 300;
                    }
                    else
                    {
                        health += other.GetComponent<PowerUP>().Health;
                    }
                    FindObjectOfType<HealthDisplay>().Setheath();
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
            AudioSource.PlayClipAtPoint(other.GetComponent<ShieldPower>().PicUpClipSound, Camera.main.transform.position, musicPlayer.GetEffectVolumeConvertet());

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
        }else{
            ShiledArmor -= damageDealer.GetDamage();
            if (ShiledArmor <= 0)
            {
                shiledOn = false;
                shield.SetActive(false);
            }
        }

   
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, musicPlayer.GetEffectVolumeConvertet());
      
        /*
        AudioSource hitsound = new AudioSource();
        hitsound.clip = HitSound;
        hitsound
        hitsound.Play();
    */
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
       
        AudioSource.PlayClipAtPoint(PLayerDeath,Camera.main.transform.position,musicPlayer.GetEffectVolumeConvertet());
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
