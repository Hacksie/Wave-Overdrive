using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float currentSpeed = 0;
        [SerializeField] private float startTime = 0;



        [Header("Referenced GameObjects")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform playerModel;
        [SerializeField] private Waves waves;

        void Start()
        {
            startTime = Time.time;
        }
        void Update()
        {
            UpdatePosition();
            UpdateShipPosition();
            UpdateShipRotation();
            UpdateShipLean();
        }

        private void UpdatePosition()
        {
            var currentPos = transform.position;
            currentTime = Time.time - startTime;
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
            inputVector = context.ReadValue<Vector2>();
            if (invertX)
                inputVector.x = 0 - inputVector.x;

            if (invertY)
                inputVector.y = 0 - inputVector.y;
        }
    }
}