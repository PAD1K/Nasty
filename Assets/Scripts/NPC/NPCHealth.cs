using UnityEngine;

public class NPCHealth : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    private Rigidbody2D _npcRb;

    public float CurrentHealth => _currentHealth;

    public float MaxHealth => _maxHealth;

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
            _npcRb.gameObject.SetActive(false);
            DestroyRigidbody();
        }
    }

    public void DestroyRigidbody()
    {
        if (_npcRb != null)
        {
            Destroy(_npcRb);
        }
    }
}
