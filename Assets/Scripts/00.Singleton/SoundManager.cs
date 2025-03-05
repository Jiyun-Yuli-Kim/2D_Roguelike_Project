using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
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

    [SerializeField] private AudioSource _BGMAudio;
    [SerializeField] private AudioSource _SFXAudio;
    
    [SerializeField] private AudioClip[] _bgms;
    [SerializeField] private AudioClip[] _sfxs;
    
    public enum EBgms
    {
        BGM1,
        BGM2,
        MAX
    }
    
    public Dictionary<string, AudioClip> SFXs = new Dictionary<string, AudioClip>();

    public void PlayBGM(EBgms bgm)
    {
        _BGMAudio.PlayOneShot(_bgms[(int)bgm]);               
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

    public void PlaySFX(string sfx)
    {
        _SFXAudio.PlayOneShot(SFXs["sfx"]);               
    }

    public void StopSFX()
    {
        _SFXAudio.Stop();
    }
}
