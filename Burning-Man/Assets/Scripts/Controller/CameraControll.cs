using System.Collections;
using System.Collections.Generic;
using BurningMan.Manager;
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
            // Kamera a��s�na g�re sol ve sa� s�n�rlar� hesapla
            leftLimit = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
            rightLimit = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
            SceneLoader.Instance.loaderUI.SetActive(false);
        }

        void FixedUpdate()
        {
            if (player.transform.position.y<transform.position.y)
            {
                // Player'� y ekseninde smooth bir �ekilde takip et
                Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
                transform.position = smoothedPosition;

                // Player'�n kamera s�n�rlar�n�n d���na ��kmas�n� engelleme
                float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            }
        }
    }
}
