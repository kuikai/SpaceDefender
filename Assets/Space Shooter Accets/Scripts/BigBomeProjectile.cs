using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBomeProjectile : MonoBehaviour
{
    [SerializeField] float TimeBeforeExplotion = 3;
    [SerializeField] Vector3 ProjectileStartSize;
    [SerializeField] float AmoutOfprojectilesInExpoltion = 30;
    [SerializeField] float ExplotionProjectileSpeed = 10;
    [SerializeField] GameObject ExplotionProjectile;
    // Start is called before the first frame update
    [SerializeField] AudioClip ProjectileSound;
    private float counter = 0;
    void Start()
    {
        ProjectileStartSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(10,10,10), 
            (counter/TimeBeforeExplotion) * Time.deltaTime);
        counter += Time.deltaTime;
        Debug.Log(counter);

        if(counter > TimeBeforeExplotion)
        {
            ExPlotion();
            counter = 0;
            transform.localScale = ProjectileStartSize;
        }
    }

    public void ExPlotion()
    {
        float degreesBetweenEachShoot = 360 / AmoutOfprojectilesInExpoltion;
        float currentShootingDrectiom = 0;
        for (int i = 0; i < AmoutOfprojectilesInExpoltion; i++)
        {

            
            GameObject Projectil = Instantiate(ExplotionProjectile, transform.position, Quaternion.identity);

            Projectil.tag = gameObject.tag;
            Projectil.layer = gameObject.layer;
            Projectil.GetComponent<Projectile>().SetUp(DegreeToVector2(currentShootingDrectiom), ExplotionProjectileSpeed);
          
            Vector3 bulletdirection = DegreeToVector2(currentShootingDrectiom);

            Vector3 ShootDirection = (transform.position.normalized + bulletdirection) - transform.position.normalized;

            Projectil.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90 + GetAngelFromVector(ShootDirection)));
           
            
             AudioSource.PlayClipAtPoint(ProjectileSound, transform.position, MusicPlayer.GetVolume());           
            
            currentShootingDrectiom += degreesBetweenEachShoot;
        }

        Destroy(gameObject);

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

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
