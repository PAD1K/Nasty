using UnityEngine;

public class NPCPossess : MonoBehaviour
{
    private bool _isPossessed = false;

    public void possessNPC () {
        _isPossessed = true;
    }


    public bool isPossessed() {
        return _isPossessed;
    }
}
