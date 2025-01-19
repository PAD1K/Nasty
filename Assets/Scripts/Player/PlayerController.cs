using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IMovable _movementController;
    private InputSystem_Actions _playerInputActions;

    void Awake()
    {
        // Instantiating input system object
        _playerInputActions = new InputSystem_Actions();

        // Better enabling input actions in action map
        _playerInputActions.Player.Enable();

        if(!TryGetComponent<IMovable>(out _movementController))
        {
            Debug.Log("There is no IMovable component attached to object");
        }
    }

    void Update()
    {
        _movementController?.Move(_playerInputActions.Player.Move.ReadValue<Vector2>());
    }

    public void ChangeMovementController(IMovable movementController)
    {
        if(movementController == null)
        {
            Debug.Log("MovementController is null");
            return;
        }

        _movementController = movementController;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ChangeMovementController(other.GetComponent<IMovable>());
    }
}
