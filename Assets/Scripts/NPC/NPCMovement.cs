using UnityEngine;

public class NPCMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Rigidbody2D _npcRb;
    [SerializeField] private float _movementSpeedMultiplier = 1;

    NPCNotPossesedMovement _npcNotPossesedMovement;

    void Awake()
    {
        if(!TryGetComponent<NPCNotPossesedMovement>(out _npcNotPossesedMovement))
        {
            Debug.Log("There is no NPCNotPossesedMovement component attached to object");
        }
    }

    public void Move(Vector2 direction)
    {
        _npcRb.AddForce(direction * _movementSpeedMultiplier, ForceMode2D.Force);
    }

    public void possessNPC () {
        _npcNotPossesedMovement.npcPossesed();
    }
}
