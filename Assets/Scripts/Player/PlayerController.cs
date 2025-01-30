using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IMovable _movementController;
    [SerializeField] float _timeToPossess = 0.5f;
    [SerializeField] private PossessionSlider _possessionProgressSlider;
    private float _timeLeftForPossession = 0.0f;
    private InputSystem_Actions _playerInputActions;
    private bool _isCaptured = false;

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
        if(movementController == null || this._isCaptured)
        {
            return;
        }

        npcPossess.possessNPC();
        _isCaptured = true;
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
            ChangeMovementController(other.GetComponent<IMovable>(), other.GetComponent<NPCPossess>());
            _timeLeftForPossession = 0f;
            SetPlayerVisible(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _possessionProgressSlider.HideSlider();
        _timeLeftForPossession = 0f;
    }

    private void SetPlayerVisible(bool isVisible)
    {
        GetComponent<Collider2D>().enabled = isVisible;
        GetComponent<SpriteRenderer>().enabled = isVisible;
    }
}
