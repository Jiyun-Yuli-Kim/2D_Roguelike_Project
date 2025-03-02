using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void Start()
    {
        GameManager.Instance.camController = this;
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
