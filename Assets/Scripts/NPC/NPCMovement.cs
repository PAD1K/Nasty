using UnityEngine;

public class NPCMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _npcRb;
    [SerializeField] private float _movementSpeedMultiplier = 1;

    public void Move(Vector2 direction)
    {
        _npcRb.AddForce(direction * _movementSpeedMultiplier, ForceMode2D.Force);
    }
}
