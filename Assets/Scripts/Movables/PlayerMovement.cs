using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _playerRb;

    public void Move(Vector2 direction)
    {
        _playerRb.AddForce(direction, ForceMode2D.Force);
    }
}
