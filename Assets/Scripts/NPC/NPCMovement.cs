using UnityEngine;

public class NPCMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _npcRb;
    [SerializeField] private float _movementSpeedMultiplier = 5;

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _npcRb.linearVelocity = Vector2.zero;    
        }

        _npcRb.linearVelocity = direction * _movementSpeedMultiplier;
    }
}
