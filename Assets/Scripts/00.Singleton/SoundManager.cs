using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBGMs
{
    TitleBGM,
    Stage1BGM
}
    
public enum ESFXs
{
    SelectSFX,
    CancelSFX,
    StartSFX,
    ShootSFX,
    HitSFX,
    WinSFX,
    LoseSFX,
    DeathSFX,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _BGMAudio;
    [SerializeField] private AudioSource _SFXAudio;
    
    [SerializeField] private AudioClip[] _bgms;
    [SerializeField] private AudioClip[] _sfxs;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void PlayBGM(EBGMs bgm)
    {
        if (_BGMAudio.isPlaying)
        {
            _BGMAudio.Stop();
        }

        _BGMAudio.PlayOneShot(_bgms[(int)bgm]);  
        _BGMAudio.loop = true;
    }

    public void StopBGM()
    {
        _BGMAudio.Stop();
    }

    public void PauseBGM()
    {
        if (!_BGMAudio.isPlaying)
        {
            return;
        }
        _BGMAudio.Pause();
    }

    public void PlaySFX(ESFXs sfx)
    {
        _SFXAudio.PlayOneShot(_sfxs[(int)sfx]);               
    }

    public void StopSFX()
    {
        _SFXAudio.Stop();
    }
}
