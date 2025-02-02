using UnityEngine;

public class NPCPossess : MonoBehaviour
{
    private bool _isPossessed = false;

    public void changePossessNPC () {
        _isPossessed = !_isPossessed;
    }

    public bool isPossessed() {
        return _isPossessed;
    }
}
