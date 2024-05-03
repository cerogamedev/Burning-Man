using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningMan.Controller
{
    public class CameraControll : MonoBehaviour
    {
        public Transform player;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private float leftLimit;
        private float rightLimit;
        private Camera mainCamera;

        Vector3 velocity = Vector3.zero;


        void Start()
        {
            mainCamera = Camera.main;
            // Kamera açýsýna göre sol ve sað sýnýrlarý hesapla
            leftLimit = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
            rightLimit = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
        }

        void FixedUpdate()
        {
            if (player.transform.position.y<transform.position.y)
            {
                // Player'ý y ekseninde smooth bir þekilde takip et
                Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
                transform.position = smoothedPosition;

                // Player'ýn kamera sýnýrlarýnýn dýþýna çýkmasýný engelleme
                float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            }
        }
    }
}
