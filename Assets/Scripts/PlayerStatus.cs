using Cinemachine;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    
    public GameObject deadMenu;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Debug.Log("Dead");
            deadMenu.SetActive(true);
            Time.timeScale = 0;
            PauseMenu.GameIsPause = true;
        }
        
        if (other.gameObject.CompareTag("SafeArea"))
        {
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.7f;
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
