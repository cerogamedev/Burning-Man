using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan
{
    public class RangedFlyLogic : MonoBehaviour
    {
        public float FireFreq;
        private float _fireFreq;

        public GameObject bulletPrefab;
        public float FirePower;
        private Transform player;

        public float fireDistance;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            _fireFreq = FireFreq;
            int RandomSpawnPoint = Random.Range(-3,4);
            transform.position = new (RandomSpawnPoint,transform.position.y);
        }

        void Update()
        {
            _fireFreq -= Time.deltaTime;
            if (_fireFreq<=0 && Vector2.Distance(transform.position, player.transform.position)<fireDistance)
            {
                GameObject firedBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
                Vector2 direction = (player.transform.position -transform.position);
                rb.AddForce(direction * FirePower/10000f, ForceMode2D.Impulse);
                _fireFreq = FireFreq;
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, fireDistance);
        }
    }
}
