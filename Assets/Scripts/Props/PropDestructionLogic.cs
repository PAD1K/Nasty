using System;
using UnityEngine;

public class PropDestructionLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _destructionState;
    private Rigidbody2D _rigidbody;

    [Header("Shake logic")]
    [SerializeField] private float _shakeForce;
    [SerializeField] private float _shakeSpeed;
    [SerializeField] private float _shakeCooldown;
    private float _currentShakeCooldown;
    private Vector2 _defaultPosition;

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody2D>(out _rigidbody))
        {
            Debug.LogWarning($"Please attack rigidbody to {gameObject.name}");
        }

        _defaultPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(_currentShakeCooldown > 0)
        {
            _currentShakeCooldown -= Time.fixedDeltaTime;

            transform.position = new Vector2(_defaultPosition.x + Mathf.Sin((_shakeCooldown - _currentShakeCooldown) * _shakeSpeed) * _shakeForce, transform.position.y);

            if(_currentShakeCooldown <= 0)
            {
                transform.position = _defaultPosition;
            }
        }
    }

    public void DestroyProp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _destructionState.SetActive(true);
    }

    public void ShakeObject()
    {
        // FIXME: change to animation
        _currentShakeCooldown = _shakeCooldown;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Prop need to be destroyed when OnCollision with any object, not only player
        //DestroyProp();
    }
}
