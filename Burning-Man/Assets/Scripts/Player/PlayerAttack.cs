using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using UnityEngine;

namespace BurningMan
{
    public class PlayerAttack : MonoSingleton<PlayerAttack>
    {
        [SerializeField] private GameObject bulletPrefab;
        public Transform FirePoint;
        public float DestroyDuration;
        public int MaxBullet, CurrentBullet;
        public float ReloadDuration;
        public float BulletFireForce;

        void Start()
        {
            CurrentBullet = MaxBullet;
        }

        void Update()
        {
            if (PlayerController.Instance.isGrounded && CurrentBullet != MaxBullet){StartCoroutine(FillAmmo());}
        }
        public IEnumerator FillAmmo()
        {
            CurrentBullet+=1;
            yield return new WaitForSeconds(ReloadDuration);
        }
        public void FireBullet()
        {
            CurrentBullet-=1;
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down*BulletFireForce,ForceMode2D.Impulse);
            Destroy(bullet, DestroyDuration);
        }
    }
}
