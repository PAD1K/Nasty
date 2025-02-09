using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDestroyable
{
    [SerializeField] private Rigidbody2D _playerRb;

    public void Destroy()
    {
        if (_playerRb != null)
        {
            Destroy(_playerRb);
        }
    }   
}
