using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float _movementSpeedMultiplier = 1;

    public void Move(Vector2 direction)
    {
        _playerRb.AddForce(direction * _movementSpeedMultiplier, ForceMode2D.Force);
    }

    public void Destroy()
    {
        if (_playerRb != null)
        {
            Destroy(_playerRb.gameObject);
        }
    }

    public void SpawnAtPosition(Vector2 position)
    {
        if (_playerRb != null)
        {
            _playerRb.position = position;

            _playerRb.linearVelocity = Vector2.zero;
            _playerRb.angularVelocity = 0f;
        }
    }
}
