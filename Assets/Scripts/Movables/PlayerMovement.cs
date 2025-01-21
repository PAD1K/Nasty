using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float _movementSpeedMultiplier = 1;

    public void Move(Vector2 direction)
    {
        _playerRb.AddForce(direction * _movementSpeedMultiplier, ForceMode2D.Force);
    }
}
