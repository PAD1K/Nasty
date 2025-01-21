using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCNotPossesedMovement : MonoBehaviour
{
    [SerializeField] private float _generateNewPointTime = 2f;
    private NavMeshAgent _npcNavMeshAgent;
    private NPCPossess _npcPossess;
    private bool _isStarted = true;

    void Awake()
    {
        if(!TryGetComponent<NPCPossess>(out _npcPossess))
        {
            Debug.Log("There is no NPCNotPossesedMovement component attached to object");
        }

        if(!TryGetComponent<NavMeshAgent>(out _npcNavMeshAgent))
        {
            Debug.Log("There is no NavMeshAgent component attached to object");
        }

        _npcNavMeshAgent.updateRotation = false;
        _npcNavMeshAgent.updateUpAxis = false;
        _npcNavMeshAgent.SetDestination(generateDestination());
    }

    void Update()
    {
        if (_isStarted) {
            StartCoroutine("GeneratePoint");
        }

        if (!_npcNavMeshAgent.pathPending && _npcNavMeshAgent.remainingDistance < 0.5f && !_npcPossess.isPossessed())
        {
            _npcNavMeshAgent.SetDestination(generateDestination());
        }
    }

    Vector3 generateDestination() 
    {
        float destinationX = Random.Range(-16f,16f);
        float destinationY = Random.Range(-8f,8f);

        return new Vector3(destinationX, destinationY, 0f);
    }

    IEnumerator GeneratePoint()
    {
        _isStarted = false; 
        _npcNavMeshAgent.ResetPath();

        if (_npcPossess.isPossessed()) {
            StopCoroutine("GeneratePoint");
            yield return new WaitForSeconds(0);
        }

        _npcNavMeshAgent.SetDestination(generateDestination());
        yield return new WaitForSeconds(_generateNewPointTime);
        _isStarted = true;
    }
}
