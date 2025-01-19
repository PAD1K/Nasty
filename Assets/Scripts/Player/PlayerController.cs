using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IMovable _movementController;
    [SerializeField] private PossessionSlider _possessionSlider;
    [SerializeField] float _timeToPossess = 0.5f;
    private float _timeLeftForPossession = 0.0f;
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

        if (movementController is NPCMovement npcController) {
            npcController.possessNPC();
            _movementController = movementController;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _possessionSlider.DisplaySlider();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _timeLeftForPossession += Time.deltaTime;
        _possessionSlider.UpdateSlider(_timeLeftForPossession/_timeToPossess);

        if (_timeLeftForPossession >= _timeToPossess)
        {
            ChangeMovementController(other.GetComponent<IMovable>());
            _timeLeftForPossession = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _timeLeftForPossession = 0f;
        _possessionSlider.HideSlider();
    }
}
