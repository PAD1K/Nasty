using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _npcArray;
    void Awake()
    {
        NPCMovement[] childrenObjects = GetComponentsInChildren<NPCMovement>();

        _npcArray = new GameObject[childrenObjects.Length];

        int index = 0;
        foreach (NPCMovement child in childrenObjects)
        {
            _npcArray[index] = child.gameObject;
            index++;
        }
    }
}
