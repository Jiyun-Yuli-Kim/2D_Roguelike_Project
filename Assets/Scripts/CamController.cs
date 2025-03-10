using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;

public class CamController : MonoBehaviour, IInitializable
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.Instance.camController = this;
        // Init();
    }

    public void SceneInitialize()
    {
        Init();
    }

    public void Init()
    {
        SetSubject(); // 카메라 추적 대상 설정
    }

    public void SetSubject()
    {
        cameras[0].Follow = GameManager.Instance.spawner.player.transform;
        cameras[0].LookAt = GameManager.Instance.spawner.player.transform;
    }

    public void PlayerCamOn()
    {
        cameras[0].Priority = 11;
    }
    public void MapViewCamOn()
    {
        cameras[0].Priority = 9;
    }
}



