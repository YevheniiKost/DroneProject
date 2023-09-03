using System;
using UnityEngine;

namespace YT.Game.Drone
{
    public class DroneMovementModule : MonoBehaviour, IDroneModule
    {
        [Space, Header("Controls")] [SerializeField]
        private float _flyUpSpeed = 5f;

        [SerializeField] private float _flyDownSpeed = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _sideRotationSpeed = 5f;
        [SerializeField] private float _idleMovementSpeed = 2;
        [SerializeField] private float _acceleration = 2f;
        
         private GameObject _followTransform;

        [Space, Header("Components")] [SerializeField]
        private Rigidbody _rigidbody;


        private Quaternion _nextRotation;
        private Quaternion _targetRotation;
        private Drone _drone;


        public void SetDrone(Drone drone)
        {
            _drone = drone;
            _followTransform = drone.Components.DroneFollowTarget.gameObject;
        }
        
        public void SetFollowTransform(GameObject followTransform)
        {
            _followTransform = followTransform;
        }

        private void Update()
        {
            var delta = Time.deltaTime;
            bool isGrounded = _drone.IsGrounded;

            ProcessUpAndDownMovement(delta, isGrounded);

            if (_drone.IsGrounded)
            {
                if (_rigidbody.isKinematic)
                    _rigidbody.isKinematic = false;
            }
            else
            {
                if (!_rigidbody.isKinematic)
                    _rigidbody.isKinematic = true;
                
                ProcessSideRotationInput(delta);
                ProcessMovementInput(delta);
                ProcessIdleMovement(delta);
            }
        }

        private void LateUpdate()
        {
            var delta = Time.deltaTime;
            bool isGrounded = _drone.IsGrounded;

            ProcessCameraRotationMovement(delta, isGrounded);
        }

        private void ProcessUpAndDownMovement(float delta, bool isGrounded)
        {
            if (InputUp())
            {
                FlyUp(delta);
            }
            else if (InputDown() && !isGrounded)
            {
                FlyDown(delta);
            }
        }

        private void ProcessSideRotationInput(float delta)
        {
            float sideRotationInput = _drone.Input.SideRotation;
            if (!_drone.IsSideRotating)
                return;

            var myTransform = transform;
            if (sideRotationInput > 0)
            {
                myTransform.Rotate(Vector3.up * (_sideRotationSpeed * delta));
            }
            else
            {
                myTransform.Rotate(Vector3.up * (-_sideRotationSpeed * delta));
            }

            _followTransform.transform.rotation = myTransform.rotation;
        }

        private void ProcessCameraRotationMovement(float delta, bool isGrounded)
        {
            if (_drone.Input.CurrentViewMode == ViewMode.FirstPerson)
                return;

            if (isGrounded)
                return;

            if (IsMoving())
            {
                var newRotation = Quaternion.Euler(0, _followTransform.transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, delta * _rotationSpeed);
            }
        }

        private float _currentAcceleration;
        
        private void ProcessMovementInput(float deltaTime)
        {
            var myTransform = transform;
            var moveDirection = _drone.Input.MoveDirection;
            
            if(IsMoving())
                _currentAcceleration = Mathf.Lerp(_currentAcceleration, 1, deltaTime * _acceleration);
            else
                _currentAcceleration = Mathf.Lerp(_currentAcceleration, 0, deltaTime * _acceleration);
            
            var forwardMovement = myTransform.forward * moveDirection.y * _currentAcceleration;
            var rightMovement = myTransform.right * moveDirection.x * _currentAcceleration;
            myTransform.position += (forwardMovement + rightMovement) * (_moveSpeed * deltaTime);
        }


        private bool InputUp()
        {
            return _drone.Input.FlyUpSpeed > 0;
        }

        private bool InputDown()
        {
            return _drone.Input.FlyUpSpeed < 0;
        }

        private void FlyUp(float deltaTime)
        {
            transform.position += Vector3.up * (_flyUpSpeed * deltaTime);
        }

        private void FlyDown(float delta)
        {
            transform.position += Vector3.down * (_flyDownSpeed * delta);
        }

        private void ProcessIdleMovement(float delta)
        {
            if (IsMoving())
                return;

            if (_drone.Input.FlyUpSpeed > Drone.MoveTrashold || _drone.Input.FlyUpSpeed < -Drone.MoveTrashold)
                return;

            var elapsedTime = Time.time;
            var noiseX = Mathf.PerlinNoise(elapsedTime, 0);
            var noiseY = Mathf.PerlinNoise(0, elapsedTime);

            noiseX = Mathf.Lerp(-1, 1, noiseX);
            noiseY = Mathf.Lerp(-1, 1, noiseY);
            var noise = Mathf.PerlinNoise(Time.time, 1);

            noise = Mathf.Lerp(-1, 1, noise);

            var myTransform = transform;
            var forwardMovement = myTransform.forward * noiseX;
            var rightMovement = myTransform.right * noiseY;
            myTransform.position += (forwardMovement + rightMovement) * (_idleMovementSpeed * delta);
        }

        private bool IsMoving()
        {
            return _drone.Input.MoveDirection.x > Drone.MoveTrashold || _drone.Input.MoveDirection.y > Drone.MoveTrashold
                || _drone.Input.MoveDirection.x < -Drone.MoveTrashold || _drone.Input.MoveDirection.y < -Drone.MoveTrashold;
        }

        private bool IsRotating()
        {
            return _drone.Input.LookDirection.x > Drone.MoveTrashold || _drone.Input.LookDirection.y > Drone.MoveTrashold;
        }
    }
}