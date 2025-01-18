using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRb;
    private InputSystem_Actions _playerInputActions;

    void Awake()
    {
        // Instantiating input system object
        _playerInputActions = new InputSystem_Actions();

        // Better enabling input actions in action map
        _playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _playerRb.AddForce(_playerInputActions.Player.Move.ReadValue<Vector2>(), ForceMode.Force);
    }
}
