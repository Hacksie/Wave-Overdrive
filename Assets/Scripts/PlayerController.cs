using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    
    public class PlayerController : MonoBehaviour
    {
        private Vector2 moveVector;
        [Header("Settings")]
        [SerializeField] private float movementSpeed = 10;
        [SerializeField] private float rotateSpeed = 340;
        [SerializeField] private float leanAngle = 80;
        [SerializeField] private float leanTime = 33;
        [SerializeField] private float clampX = 0.9f;
        [SerializeField] private float clampY = 0.9f;

        [Header("Referenced GameObjects")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform playerModel;
        [SerializeField] private Transform aimTarget;

        void Update()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateLean();
        }

        private void UpdatePosition()
        {
            playerModel.localPosition += new Vector3(moveVector.x, 0) * movementSpeed * Time.deltaTime;
            Vector3 modelViewPos = mainCamera.WorldToViewportPoint(playerModel.position);
            modelViewPos.x = Mathf.Clamp(modelViewPos.x,0, clampX);
            modelViewPos.y = Mathf.Clamp(modelViewPos.y, 0, clampY);
            playerModel.position = mainCamera.ViewportToWorldPoint(modelViewPos);  
        }

        private void UpdateLean()
        {
            Vector3 targetEulerAngels = playerModel.localEulerAngles;
            playerModel.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -moveVector.x * leanAngle, leanTime * Time.deltaTime));
        }

        private void UpdateRotation()
        {
            aimTarget.parent.position = Vector3.zero;
            aimTarget.localPosition = new Vector3(moveVector.x, moveVector.y, 1);
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);
        }

        public void MovementEvent(InputAction.CallbackContext context)
        {
            moveVector = context.ReadValue<Vector2>();
        }
    }
}