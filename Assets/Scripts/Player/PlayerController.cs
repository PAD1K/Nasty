using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IMovable _movementController;
    private NPCPossess _npcPossess = null;
    [SerializeField] float _timeToPossess = 0.5f;
    private float _timeLeftForPossession = 0.0f;
    private InputSystem_Actions _playerInputActions;

    void Awake()
    {
        _playerInputActions = new InputSystem_Actions();

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

    public void ChangeMovementController(IMovable movementController, NPCPossess npcPossess)
    {
        if(movementController == null)
        {
            Debug.Log("MovementController is null");
            return;
        }

        npcPossess.possessNPC();
        _movementController = movementController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _npcPossess = other.GetComponent<NPCPossess>();
        _npcPossess.displayPossessionSlider();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_npcPossess != null) {
            _timeLeftForPossession += Time.deltaTime;

            _npcPossess.updatePossessionSlider(_timeLeftForPossession/_timeToPossess);

            if (_timeLeftForPossession >= _timeToPossess)
            {
                ChangeMovementController(other.GetComponent<IMovable>(), _npcPossess);
                _timeLeftForPossession = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_npcPossess != null) {
            _npcPossess.hidePossessionSlider();
            _timeLeftForPossession = 0f;
            _npcPossess = null;
        }
    }
}
