using System.Collections;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class NPCNotPossesedMovement : MonoBehaviour
{
    [SerializeField] private float _generateNewPointTime = 2f;
    [SerializeField] private PossessionSlider _possessionSlider;
    protected NavMeshAgent _npcNavMeshAgent;
    private bool _isPossessed = false;
    private bool _isStarted = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _npcNavMeshAgent = GetComponent<NavMeshAgent>();
        _npcNavMeshAgent.updateRotation = false;
        _npcNavMeshAgent.updateUpAxis = false;
        _npcNavMeshAgent.SetDestination(generateDestination());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStarted) {
            StartCoroutine("GeneratePoint");
        }

        if (!_npcNavMeshAgent.pathPending && _npcNavMeshAgent.remainingDistance < 0.5f && !_isPossessed)
        {
            _npcNavMeshAgent.SetDestination(generateDestination());
        }
    }

    public void npcPossesed () {
        _isPossessed = true;
        _possessionSlider.Possessed();
        _isStarted = false;
        StopCoroutine("GeneratePoint");
        _npcNavMeshAgent.ResetPath();
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
        _npcNavMeshAgent.SetDestination(generateDestination());
        yield return new WaitForSeconds(_generateNewPointTime);
        _isStarted = true;
    }
}
