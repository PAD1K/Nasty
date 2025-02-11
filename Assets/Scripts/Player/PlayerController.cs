using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IMovable _movementController;
    [SerializeField] float _timeToPossess = 0.5f;
    [SerializeField] private PossessionSlider _possessionProgressSlider;
    private float _timeLeftForPossession = 0.0f;
    private InputSystem_Actions _playerInputActions;
    private bool _isCaptured = false;
    private NPCHealth _npcHealth = null;
    private Collider2D _collider = null;
    private SpriteRenderer _spriteRenderer = null;
    private Rigidbody2D _rigidbody = null;

    void Awake()
    {
        _playerInputActions = new InputSystem_Actions();

        _playerInputActions.Player.Enable();

        if(!TryGetComponent<IMovable>(out _movementController))
        {
            Debug.Log("There is no IMovable component attached to object");
        }

        if (!TryGetComponent<Collider2D>(out _collider))
        {
            Debug.Log("There is no Collider2D component attached to object");
        }

        if (!TryGetComponent<SpriteRenderer>(out _spriteRenderer))
        {
            Debug.Log("There is no SpriteRenderer component attached to object");
        }

        if (!TryGetComponent<Rigidbody2D>(out _rigidbody))
        {
            Debug.Log("There is no SpriteRenderer component attached to object");
        }
    }

    void Update()
    {
        _movementController?.Move(_playerInputActions.Player.Move.ReadValue<Vector2>());

        if (_playerInputActions.Player.Jump.WasPressedThisFrame())
        {
            MonoBehaviour movableComponent = _movementController as MonoBehaviour;
            if (movableComponent == null)
            {
                return;
            }

            ReleaseNpc(movableComponent.gameObject.transform.position);
        }
    }

    public void ChangeMovementController(IMovable movementController, NPCPossess npcPossess)
    {
        if(movementController == null || this._isCaptured)
        {
            return;
        }

        npcPossess.changePossessNPC();
        _possessionProgressSlider.DisableSlider();
        _movementController = movementController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Prop")
        {
            // TODO: remove DestroyProp when posses logic will be implemented
            // other.gameObject.GetComponent<PropDestructionLogic>().DestroyProp();
            // other.gameObject.GetComponent<PropDestructionLogic>().ShakeObject();
        }

        _possessionProgressSlider.DisplaySlider();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _timeLeftForPossession += Time.deltaTime;

        _possessionProgressSlider.UpdateSlider(_timeLeftForPossession/_timeToPossess);

        if (_timeLeftForPossession >= _timeToPossess)
        {
            Capture(other);
        }
    }

    public void Capture(Collider2D npc)
    {
        ChangeMovementController(npc.GetComponent<IMovable>(), npc.GetComponent<NPCPossess>());
        _isCaptured = true;
        _timeLeftForPossession = 0f;
        SetPlayerVisible(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _possessionProgressSlider.HideSlider();
        _timeLeftForPossession = 0f;
    }

    private void SetPlayerVisible(bool isVisible, Vector2? position = null)
    {
        _collider.enabled = isVisible;
        _spriteRenderer.enabled = isVisible;

        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.simulated = isVisible;
        }

        if (isVisible && position.HasValue)
        {
            transform.position = new Vector2(position.Value.x, position.Value.y);
        }
    }

    public void ReleaseNpc(Vector2 position)
    {
        if (!_isCaptured)
        {
            return;
        }

        _isCaptured = false;

        MonoBehaviour npcMovableComponent = _movementController as MonoBehaviour;
        if (npcMovableComponent == null)
        {
            return;
        }

        ChangeMovementController(gameObject.GetComponent<IMovable>(), npcMovableComponent.gameObject.GetComponent<NPCPossess>());
        SetPlayerVisible(true, position);
        _possessionProgressSlider.EnableSlider();
    }
}
