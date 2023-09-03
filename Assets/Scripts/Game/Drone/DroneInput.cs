using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YT.Game.Drone
{
    public enum ViewMode
    {
        FirstPerson,
        ThirdPerson
    }

    public class DroneInput : MonoBehaviour, IDroneInput
    {
        public event Action<ViewMode> OnViewModeChanged;
        public event Action OnBombDropped;

        public Vector2 LookDirection => _lookDirection;
        public Vector2 MoveDirection => _moveDirection;
        public ViewMode CurrentViewMode => _currentViewMode;
        public float SideRotation => _sideRotationSpeed;
        public float FlyUpSpeed => _flyUpSpeed;

        private DroneControls _input;
        private float _flyUpSpeed, _sideRotationSpeed;
        private ViewMode _currentViewMode;
        private Vector2 _moveDirection, _lookDirection;
        private InputAction _moveInput, _lookInput, _flyUpInput, _sideRotationInput;


        private void Awake()
        {
            _input = new DroneControls();
            _currentViewMode = ViewMode.ThirdPerson;
        }

        private void Start()
        {
            OnViewModeChanged?.Invoke(_currentViewMode);
        }

        private void OnEnable()
        {
            _moveInput = _input.Player.Move;
            _lookInput = _input.Player.Look;
            _moveInput.Enable();
            _lookInput.Enable();
            _input.Player.DropBomb.Enable();
            _input.Player.ChangeView.Enable();
            _input.Player.FlyUp.Enable();
            _input.Player.SideRotation.Enable();
        }

        private void OnDisable()
        {
            _moveInput.Disable();
            _lookInput.Disable();
            _input.Player.ChangeView.Disable();
            _input.Player.DropBomb.Disable();
            _input.Player.FlyUp.Disable();
            _input.Player.SideRotation.Disable();
        }

        private void Update()
        {
            _moveDirection = _moveInput.ReadValue<Vector2>();
            _lookDirection = _lookInput.ReadValue<Vector2>();
            _flyUpSpeed = _input.Player.FlyUp.ReadValue<float>();
            _sideRotationSpeed = _input.Player.SideRotation.ReadValue<float>();

            if (_input.Player.ChangeView.WasReleasedThisFrame())
                SwitchViewMode();

            if (_input.Player.DropBomb.WasReleasedThisFrame())
                OnBombDropped?.Invoke();
        }

        private void SwitchViewMode()
        {
            if (_currentViewMode == ViewMode.FirstPerson)
            {
                _currentViewMode = ViewMode.ThirdPerson;
            }
            else
            {
                _currentViewMode = ViewMode.FirstPerson;
            }

            OnViewModeChanged?.Invoke(_currentViewMode);
        }
    }
}