﻿using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    
    public GameObject deadMenu;

    public int scene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            PauseMenu.Instance.Death();
        }
        
        if (other.gameObject.CompareTag("SafeArea"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.7f;
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            PauseMenu.Instance.LoadScene(scene);
        }

        if (other.gameObject.CompareTag("EndPortal"))
        {
            PauseMenu.Instance.LoadScene(scene);
            PauseMenu.Instance.finishMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SafeArea"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
        }
    }
}
