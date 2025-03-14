using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    private void Awake()
    {
        _settingsCanvas.gameObject.SetActive(false);
        _exitCanvas.gameObject.SetActive(false);
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
}
