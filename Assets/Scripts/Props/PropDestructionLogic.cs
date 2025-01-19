using System;
using UnityEngine;

public class PropDestructionLogic : MonoBehaviour
{
    [SerializeField] private GameObject _destructionState;
    public void DestroyProp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        _destructionState.SetActive(true);
    }
}
