using UnityEngine;

public class NPCMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _npcRb;

    public void Move(Vector2 direction)
    {
        _npcRb.AddForce(direction*2, ForceMode2D.Force);
    }
}
