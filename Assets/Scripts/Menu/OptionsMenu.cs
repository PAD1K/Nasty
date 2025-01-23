using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] TMPro.TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;

    void Start()
    {
        GetResolutions();
    }
    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution _resolution = _resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetVSync(bool _isVSync)
    {
        if(_isVSync)
        {
            Application.targetFrameRate = 60;
            //QualitySettings.vSyncCount = 4;
        }
        else{
            Application.targetFrameRate = -1;
            //QualitySettings.vSyncCount = 0;
        }
    }

    void GetResolutions()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionindex = 0;
        for (int i = 0; i< _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionindex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionindex;
        _resolutionDropdown.RefreshShownValue();
    }
}
