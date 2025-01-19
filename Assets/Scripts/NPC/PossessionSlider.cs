using UnityEngine;
using UnityEngine.UI;

public class PossessionSlider : MonoBehaviour
{
    [SerializeField] Image _possessionSlider;
    private bool _isPossessed = false;
    void Awake() {
        // _possessionSlider = GetComponent<Image>();
    }

    public void DisplaySlider() {
        if (!_isPossessed) {
            this.gameObject.SetActive(true);
        }
    }

    public void HideSlider() {
        if (!_isPossessed) {
            _possessionSlider.fillAmount = 0f;
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateSlider(float time) {
        if (!_isPossessed) {
            _possessionSlider.fillAmount = time;
            Debug.Log(_possessionSlider.fillAmount);
        }
    }

    public void Possessed () {
        _isPossessed = true;
        _possessionSlider.fillAmount = 0f;
        this.gameObject.SetActive(false);
    }
}
