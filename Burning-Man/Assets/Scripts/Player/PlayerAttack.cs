using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BurningMan
{
    public class PlayerAttack : MonoSingleton<PlayerAttack>
    {
        [SerializeField] private GameObject bulletPrefab;
        public Transform FirePoint;
        public float DestroyDuration;
        public float MaxBullet, CurrentBullet;
        public float ReloadDuration;
        public float BulletFireForce;

        public Slider BulletSlider;
        public TextMeshProUGUI BulletText;

        void Start()
        {
            CurrentBullet = MaxBullet;
        }

        void Update()
        {
            if (PlayerController.Instance.isGrounded && CurrentBullet != MaxBullet){StartCoroutine(FillAmmo());}
            BulletText.text = CurrentBullet.ToString() + " / " + MaxBullet.ToString();
            BulletSlider.value = CurrentBullet / MaxBullet;
        }
        public IEnumerator FillAmmo()
        {
            CurrentBullet+=1;
            yield return new WaitForSeconds(ReloadDuration);
        }
        public void FireBullet()
        {
            StartCoroutine(ScreenShake.Instance.StartShake(0.3f,0.05f));

            CurrentBullet-=1;
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-0.1f,0.11f), -1)*BulletFireForce,ForceMode2D.Impulse);
            Destroy(bullet, DestroyDuration);
        }
    }
}
