using UnityEngine;
using UnityEngine.UI;

public class PossessionSlider : MonoBehaviour
{
    [SerializeField] Image _possessionSlider;
    private bool _isInPossession = false;

    public void DisplaySlider() {
        if (!_isInPossession) {
            this.gameObject.SetActive(true);
        }
    }

    public void HideSlider() {
        if (!_isInPossession) {
            _possessionSlider.fillAmount = 0f;
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateSlider(float time) {
        if (!_isInPossession) {
            _possessionSlider.fillAmount = time;
        }
    }

    public void DisableSlider () {
        _isInPossession = true;
        _possessionSlider.fillAmount = 0f;
        this.gameObject.SetActive(false);
    }
}
