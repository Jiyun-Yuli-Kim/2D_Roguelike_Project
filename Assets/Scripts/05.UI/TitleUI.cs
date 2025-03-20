using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Button _settingsExitButton;
    
    [SerializeField] private Canvas _exitCanvas;
    [SerializeField] private Button _exitYesButton;
    [SerializeField] private Button _exitNoButton;
    
    [SerializeField] private AudioMixer  _audioMixer;
    [SerializeField] private Slider _BGMSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Button _SFXHandle;
    
    private bool isDragging = false;
    
    private void Awake()
    {
        _settingsCanvas.gameObject.SetActive(false);
        _exitCanvas.gameObject.SetActive(false);
        _BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        _SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        _audioMixer.SetFloat("BGMVolume", Mathf.Log10(0.8f) * 15);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(0.8f) * 15);
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(EBGMs.TitleBGM);
    }

    public void NewGame()
    {
        GameManager.Instance.StartGame();
    }

    public void OpenSettingsPopup()
    {
        _settingsCanvas.gameObject.SetActive(true);
    }
    
    public void CloseSettingsPopup()
    {
        _settingsCanvas.gameObject.SetActive(false);
    }
    
    public void OpenExitPopup()
    {
        _exitCanvas.gameObject.SetActive(true);
    }
    
    public void CloseExitPopup()
    {
        _exitCanvas.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void PlayStartSound()
    {
        SoundManager.Instance.PlaySFX(ESFXs.StartSFX);
    }
    
    public void PlaySelectSound()
    {
        SoundManager.Instance.PlaySFX(ESFXs.SelectSFX);
    }

    public void PlayCancelSound()
    {
        SoundManager.Instance.PlaySFX(ESFXs.CancelSFX);
    }

    public void SetBGMVolume(float volume) // 0.001 ~ 1
    {
        _audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 15); // -3 ~ 0
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 15);
    }

    public void SFXHandleClick()
    {
        SoundManager.Instance.PlaySFX(ESFXs.SelectSFX);
    }

    // // public void OnPointerDown(PointerEventData eventData)
    // // {
    // //     isDragging = true;
    // // }
    //
    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     // if (isDragging)
    //     // {
    //         SoundManager.Instance.PlaySFX(ESFXs.SelectSFX);
    //     // }
    //     //
    //     // isDragging = false;
    // }
}
