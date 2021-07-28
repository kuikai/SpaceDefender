using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallShips
{

    public class BaseBulletStarter : MonoBehaviour {



        public List<AudioClip> ProjectileAudioClips;
        public GameObject bulletPrefab; 
        [Tooltip("Lest of empty child GameObjects on the ship where bullet will appear")]
        public Transform[] bulletStartPoses;
        [Tooltip("If 0 than new sortingOrder will implemented for bullet")]
        public int bulletSortingOrder = 0;
        public float bulletSpeed;
        [Tooltip("Delay between each bullet if repeat fire mode")]
        public float fireDelay;
        public float SpritFireDelay;

        [Tooltip("Should bullets appear one after another or all at once. Use for ships with many bulletStartPoses")]
        public bool fireInSequence;
        public bool OneShootOnePlace;
        [Space(20)]
        public GameObject bombPrefab;
        public Transform bombStartPos;
        public float bombSpeed;

        bool repeatFire = false;
        bool repeatFireSprit = false;
        
        int fireIndex = 0;

        MusicPlayer musicPlayer;

        private void Start()
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();
        }

        void OneShot(int index)
        {
            if (IfIndexGood(index))
            {

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletStartPoses[index].position, Quaternion.identity);
                
                if (bulletSortingOrder != 0)
                {
                    bullet.GetComponent<SpriteRenderer>().sortingOrder = bulletSortingOrder;
                }
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletSpeed * (-bulletStartPoses[index].up);
            }
        }

        public void LaunchBomb()
        {
            GameObject bomb = (GameObject)Instantiate(bombPrefab, bombStartPos.position, bombStartPos.rotation);
            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            rb.velocity = bombSpeed * (-transform.up);
        }

        bool IfIndexGood(int index)
        {
            if (bulletStartPoses != null && index >= 0 && index < bulletStartPoses.Length)
            {
                return true;
            } else
            {
                Debug.LogWarning("index is out of range in bulletStartPoses");
                return false;
            }
        }

        public void StartRepeateFire()
        {
            if (!repeatFire)
            {
                repeatFire = true;
                fireIndex = 0;
                StartCoroutine(RepeateFire());
            }
        }

        public void StopRepeatFire()
        {
            repeatFire = false;
        }

        public void MakeOneShot()
        {
            if (!OneShootOnePlace)
            {
                for (int index = 0; index < bulletStartPoses.Length; index++)
                {
                    OneShot(index);
                   // AudioSource.PlayClipAtPoint(ProjectileAudioClips[0], transform.position, musicPlayer.GetEffectVolumeConvertet());
                }
            }
            else
            {
               
              //  AudioSource.PlayClipAtPoint(ProjectileAudioClips[0], transform.position, musicPlayer.GetEffectVolumeConvertet());
                OneShot(0);
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(RepeateFire());
        }

        IEnumerator RepeateFire()
        {
            while (repeatFire)
            {
                if (fireInSequence) {
                    OneShot(fireIndex);
                    if (++fireIndex >= bulletStartPoses.Length)
                        fireIndex = 0;
                } else
                {
                    MakeOneShot();
                }
                yield return new WaitForSeconds(fireDelay);
            }
        }

        IEnumerator SpritFire()
        {
            while (repeatFireSprit)
            {
                yield return new WaitForSeconds(SpritFireDelay);
            }
        }
    }
}
