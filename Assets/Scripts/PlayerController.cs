using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{

    public class PlayerController : MonoBehaviour
    {
        private Vector2 inputVector;
        [Header("Settings")]
        [SerializeField] private float movementSpeed = 10;
        [SerializeField] private float rotateSpeed = 340;
        [SerializeField] private float leanAngle = 80;
        [SerializeField] private float leanTime = 33;
        [SerializeField] private Rect clamp = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
        [SerializeField] private float heightAboveWaves = 1.0f;
        [SerializeField] private bool allowYMovement = false;
        [SerializeField] private bool invertX = false;
        [SerializeField] private bool invertY = false;
        [SerializeField] private float forwardSpeed = 1;
        [SerializeField] private float acceleration = 0.01f;
        [SerializeField] private float currentTime = 0;
        [SerializeField] public float currentSpeed = 0;
        //[SerializeField] private float startTime = 0;
        [SerializeField] private float fireRate = 0.4f;

        [Header("Referenced GameObjects")]
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] public Transform playerModel = null;
        [SerializeField] private Waves waves = null;
        //[SerializeField] private Pool pool = null;
        //[SerializeField] private Bullet bullet = null;
        [SerializeField] private Transform[] firingPoints = null;
        [SerializeField] private Transform shield = null;

        [Header("State")]
        [SerializeField] private bool firing = false;
        [SerializeField] private float lastFireTime = 0;
        [SerializeField] private int firingPointIndex = 0;
        [SerializeField] private bool shielded = false;

        private void Awake()
        {
            currentSpeed = 0;
        }

        private void Update()
        {
            if (Game.instance.state.gameState == GameState.Playing)
            {
                UpdatePosition();
                UpdateFiring();

            }
            UpdateShipPosition();
            UpdateShipRotation();
            UpdateShipLean();
            UpdateShield();
        }

        private void UpdateShield()
        {
            shield.gameObject.SetActive(shielded);
        }

        private void UpdateFiring()
        {
            if(firing && Time.time - lastFireTime >= fireRate)
            {
                var firingPoint = firingPoints[firingPointIndex];

                var bullet = Game.instance.pool.GetPlayerBullet();
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.gameObject.SetActive(true);
                }
                
                bullet.Fire(firingPoint.position, firingPoint.forward, currentSpeed);
                lastFireTime = Time.time;
                firingPointIndex++;
                if(firingPointIndex >= firingPoints.Length)
                {
                    firingPointIndex = 0;
                }
            }
        }

        private void UpdatePosition()
        {
            var currentPos = transform.position;
            currentTime = Time.time - Game.instance.playingStartTime;
            currentSpeed = forwardSpeed + (currentTime * acceleration);
            currentPos.z += currentSpeed * Time.deltaTime;
            transform.position = currentPos;
        }

        private void UpdateShipPosition()
        {
            playerModel.localPosition += new Vector3(inputVector.x, allowYMovement ? inputVector.y : 0) * movementSpeed * Time.deltaTime;

            // Clamp input to the screen view port first
            Vector3 modelViewPos = mainCamera.WorldToViewportPoint(playerModel.position);
            modelViewPos.x = Mathf.Clamp(modelViewPos.x, clamp.xMin, clamp.xMax);
            modelViewPos.y = Mathf.Clamp(modelViewPos.y, clamp.yMin, clamp.yMax);
            Vector3 modelPos = mainCamera.ViewportToWorldPoint(modelViewPos);

            // Then apply any height adjustments
            var height = Mathf.Max(waves.GetHeight(playerModel.position) + heightAboveWaves, modelPos.y);
            modelPos.y = Mathf.Lerp(modelPos.y, height, Time.deltaTime * movementSpeed);
            playerModel.position = modelPos;
        }

        private void UpdateShipRotation()
        {
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(new Vector3(inputVector.x, inputVector.y, 1)), Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);
        }

        private void UpdateShipLean()
        {
            playerModel.localEulerAngles = new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, Mathf.LerpAngle(playerModel.localEulerAngles.z, -inputVector.x * leanAngle, leanTime * Time.deltaTime));
        }

        public void MovementEvent(InputAction.CallbackContext context)
        {
            if (Game.instance.state.gameState == GameState.Playing)
            {
                inputVector = context.ReadValue<Vector2>();
                if (Game.instance.preferences.invertX)
                    inputVector.x = 0 - inputVector.x;

                if (Game.instance.preferences.invertY)
                    inputVector.y = 0 - inputVector.y;
            }
        }

        public void FireEvent(InputAction.CallbackContext context)
        {
            if (Game.instance.state.gameState == GameState.Playing)
            {
                if(context.performed)
                {
                    firing = true;
                }
            }

            if (context.canceled)
            {
                firing = false;
            }
        }

        public void Explode()
        {
            Logger.Log(name, "Player controller explode");
            Game.instance.GameEndCrash();
        }
    }
}