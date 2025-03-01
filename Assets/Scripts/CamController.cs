using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    public void PlayerCamOn()
    {
        cameras[0].Priority = 11;
    }
    public void MapViewCamOn()
    {
        cameras[0].Priority = 9;
    }
}
