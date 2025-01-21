using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _followTransform;
    [SerializeField] private BoxCollider2D _mapBounds;
    [SerializeField] private float _smoothSpeed = 0.5f;

    [SerializeField] private float _zoomSpeed = 0.5f; // Скорость изменения зума
    [SerializeField] private float _minZoom = 4f;    // Минимальный размер камеры
    [SerializeField] private float _maxZoom = 7f;   // Максимальный размер камеры
    private InputSystem_Actions _playerInputActions;

    private float _xMin, _xMax, _yMin, _yMax;
    private float _camY,_camX;
    private float _camWidth;
    private Camera _mainCam;
    private Vector3 _smoothPos;

    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();

        if(!TryGetComponent<Camera>(out _mainCam))
        {
            Debug.Log("There is no Camera component attached to object");
        }

        _xMin = _mapBounds.bounds.min.x;
        _xMax = _mapBounds.bounds.max.x;
        _yMin = _mapBounds.bounds.min.y;
        _yMax = _mapBounds.bounds.max.y;
        _camWidth = _mainCam.orthographicSize * _mainCam.aspect;
    }

    private void OnEnable()
    {
         _playerInputActions.Player.CameraZoom.Enable();
        _playerInputActions.Player.CameraZoom.performed += OnZoom;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.CameraZoom.performed -= OnZoom;
        _playerInputActions.Player.CameraZoom.Disable();
    }

    void FixedUpdate()
    {
        _camY = Mathf.Clamp(_followTransform.position.y, _yMin + _mainCam.orthographicSize, _yMax - _mainCam.orthographicSize);
        _camX = Mathf.Clamp(_followTransform.position.x, _xMin + _camWidth, _xMax - _camWidth);
        _smoothPos = Vector3.Lerp(this.transform.position, new Vector3(_camX, _camY, this.transform.position.z), _smoothSpeed);
        this.transform.position = _smoothPos;
    }

    private void OnZoom(InputAction.CallbackContext context)
    {
        Vector2 scrollInput = context.ReadValue<Vector2>();
        _mainCam.orthographicSize -= scrollInput.y * _zoomSpeed;
        _mainCam.orthographicSize = Mathf.Clamp(_mainCam.orthographicSize, _minZoom, _maxZoom);

        _camWidth = _mainCam.orthographicSize * _mainCam.aspect;
    }
}