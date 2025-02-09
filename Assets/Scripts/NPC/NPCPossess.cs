using UnityEngine;

public class NPCPossess : MonoBehaviour
{
    private bool _isPossessed = false;
    private NPCHealth _npcHealth = null;
    private PlayerController _playerController = null;

    public void changePossessNPC () {
        _isPossessed = !_isPossessed;
    }

    public bool isPossessed() {
        return _isPossessed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Prop")
        {
            // Need to implement damageToNpc
            // Smash(damageToNpc);
        }
    }

    private void Smash(float damageToNpc)
    {
        if(!TryGetComponent<NPCHealth>(out _npcHealth))
        {
            return;
        }

        Vector2 currentPosition = transform.position;
        _npcHealth.TakeDamage(damageToNpc);

        if (_npcHealth.CurrentHealth < 0)
        {
            if(!TryGetComponent<PlayerController>(out _playerController))
            {
                return;
            }

            _playerController.ReleaseNpc(currentPosition);
        }
    }
}
