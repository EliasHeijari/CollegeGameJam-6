using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
        [SerializeField, Range(1, 10)] float walkingSpeed = 3.0f;
        [Range(0.1f, 5)] public float CrouchSpeed = 1.0f;
        [SerializeField, Range(2, 20)] float runningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;

        [Space(20)]

        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]

        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        [HideInInspector] public float WalkingValue;

        public event EventHandler OnJump;
        public bool isGrounded { get; private set; } = false;
        public bool IsWalking { get; private set; }
        public bool IsRunning { get; private set; }

        [Header("Sprinting")]
        [SerializeField] private Image sprintBar;
        private float sprintingMeterMax = 6f;
        private float sprintingMeter;
        bool wasRunning;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallFOV = cam.fieldOfView;
            RunningValue = runningSpeed;
            WalkingValue = walkingSpeed;
        }

        void Update()
        {
            // Perform raycast to check if the player is grounded
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, characterController.height / 2 + 0.1f);

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            if (sprintingMeter > 0f && !wasRunning)
                isRunning = CanRunning ? Input.GetKey(KeyCode.LeftShift) : false;
            else isRunning = false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;
            if (isRunning) RunningValue = Mathf.Lerp(RunningValue, runningSpeed, timeToRunning * Time.deltaTime);
            else RunningValue = WalkingValue;
            float movementDirectionY = moveDirection.y;

            if (isRunning && !wasRunning)
            {
                sprintingMeter -= Time.deltaTime;
                if (sprintingMeter <= 0)
                {
                    wasRunning = true;
                }
            }
            else
            {
                sprintingMeter += Time.deltaTime;
                if (sprintingMeter > 2f && wasRunning) wasRunning = false;
                if (sprintingMeter >= sprintingMeterMax) sprintingMeter = sprintingMeterMax;
            }

            sprintBar.fillAmount = sprintingMeter / sprintingMeterMax;

            // Modify the movement calculation to ensure it's normalized
            Vector3 desiredMove = (forward * vertical) + (right * horizontal);
            desiredMove = Vector3.ClampMagnitude(desiredMove, 1.0f); // Normalize the desired movement vector
            moveDirection = desiredMove * (isRunning ? RunningValue : WalkingValue);

            IsWalking = Mathf.Abs(moveDirection.x) > 0.15f && !isRunning || Mathf.Abs(moveDirection.z) > 0.15f && !isRunning;
            IsRunning = isRunning && moveDirection.magnitude > 0.15f;

            if (Input.GetButton("Jump") && canMove && isGrounded)
            {
                OnJump?.Invoke(this, EventArgs.Empty);
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (Cursor.lockState == CursorLockMode.Locked && canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

                if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
                else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
            }

        }

    }
}
