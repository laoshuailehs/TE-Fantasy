using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using GameLogic;

public class CameraVcam : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public GameObject  Player;
    void Start() {
        // vcam.Follow = Player.transform;
        // vcam.LookAt = Player.transform;
    }
}
