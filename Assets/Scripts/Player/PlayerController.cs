using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
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
        // Debug.Log(_playerInputActions.Player.Move.ReadValue<Vector2>());
        _playerRb.AddForce(_playerInputActions.Player.Move.ReadValue<Vector2>(), ForceMode2D.Force);

        // if(Mathf.Abs(transform.position.x) >= 10)
        // {
        //     Debug.Log("X");
        //     _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x * -1, _playerRb.linearVelocity.y);
        // }
        // else if (Mathf.Abs(transform.position.y) >= 10)
        // {
        //     Debug.Log("Y");
        //     _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _playerRb.linearVelocity.y * -1);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "WallX")
        {
            Debug.Log("X");
            _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x * -1, _playerRb.linearVelocity.y);
        }
        else if(other.tag == "WallY")
        {
            Debug.Log("Y");
            _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, _playerRb.linearVelocity.y * -1);
        }
    }
}
