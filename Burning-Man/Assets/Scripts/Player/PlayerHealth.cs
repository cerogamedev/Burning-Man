using System.Collections;
using System.Collections.Generic;
using BurningMan.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BurningMan
{
    public class PlayerHealth : MonoSingleton<PlayerHealth>
    {
        [SerializeField] private float health, maxHealth;
        public Slider healthSlider;
        public TextMeshProUGUI HealthText;
        private PlayerController player;
        public float BounceDuration;
        public float BouncePower; 
        public float DamageableDuration;
        public bool canDamageable = true;
        private SpriteRenderer playerSprite;
        void Awake()
        {
            player = GameObject.FindObjectOfType<PlayerController>();
            playerSprite = player.GetComponent<SpriteRenderer>();
        }
        void Start()
        {
            maxHealth = 4;
            health = maxHealth;
        }
        void Update()
        {
            healthSlider.value = health/maxHealth;
            HealthText.text = health.ToString() + " / "+ maxHealth.ToString();
        }
        public void SetHealth(float setHealth)
        {
            health += setHealth;
            StartCoroutine(ScreenShake.Instance.StartShake(0.2f,0.05f));
            SoundManager.Instance.PlaySfx(SoundManager.Instance.LoseHearth);
            if (health==0)
            {
                PlayerController.Instance.ChangeState(new IDead());
            }
        }
        public float GetHealth()
        {
            return health;
        }
        public void SetMaxHealth(float maxHealthAmount)
        {
            maxHealth = maxHealthAmount;
        }

        void OnTriggerEnter2D(Collider2D other0)
        {
            if (other0.transform.tag == "AttackObject" && canDamageable)
            {
                SetHealth(-1);
                Vector2 forceDirection = (transform.position - other0.transform.position).normalized;
                StartCoroutine(ApplyForce(forceDirection, BounceDuration));
                StartCoroutine(CanTakeDamage());
            }
        }
        IEnumerator ApplyForce(Vector2 force, float duration)
        {
                player.rb.AddForce(force * 8000, ForceMode2D.Impulse);
                yield return new WaitForSeconds(duration);
                player.rb.velocity = Vector2.zero;
        }
        IEnumerator CanTakeDamage()
        {
                playerSprite.color = Color.red;
                canDamageable = false;
                yield return new WaitForSeconds(DamageableDuration);
                canDamageable = true;
                playerSprite.color = Color.white;
        }

    }
}
