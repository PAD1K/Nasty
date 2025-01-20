using UnityEngine;

public class NPCPossess : MonoBehaviour
{
    [SerializeField] private PossessionSlider _possessionSlider;
    
    private bool _isPossessed = false;

    public void possessNPC () {
        _isPossessed = true;
        _possessionSlider.Possessed();
    }


    public bool isPossessed() {
        return _isPossessed;
    }

    public void displayPossessionSlider() {
        _possessionSlider.DisplaySlider();
    }

    public void updatePossessionSlider(float time) {
         _possessionSlider.UpdateSlider(time);
    }

    public void hidePossessionSlider() {
         _possessionSlider.HideSlider();
    }
}
