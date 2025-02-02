using UnityEngine;

public class NPCHealth : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    private Rigidbody2D _npcRb;

    public float CurrentHealth
    {
        get { return _currentHealth; }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
    }

    void Awake() 
    {
        if(!TryGetComponent<Rigidbody2D>(out _npcRb))
        {
            Debug.Log("There is no Rigidbody2D component attached to object");
        }
        
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth = _currentHealth - damage;
        }
        else
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        if (_npcRb != null)
        {
            Destroy(_npcRb);
        }
    }
}
