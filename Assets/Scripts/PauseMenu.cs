using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUi;
    public GameObject settingMenu;
    public GameObject resolutionMenu;
    public GameObject saveMenu;

    public int level = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();                
            }
            else
            {
                Pause();
            }
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(level);
    }

    public void LoadLevel()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(data.level);
        Time.timeScale = 1;
        GameIsPause = false;
    }
    
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        GameIsPause = false;
    }

    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        GameIsPause = true;
    }

    public void ResetScene()
    {
        Time.timeScale = 1;
        GameIsPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        Time.timeScale = 1;
        GameIsPause = false;
        SceneManager.LoadScene("Gameplay");
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ActivePauseMenu()
    {
        pauseMenuUi.SetActive(true);
        settingMenu.SetActive(false);
        resolutionMenu.SetActive(false);
    }

    public void ActiveSettingMenu()
    {
        pauseMenuUi.SetActive(false);
        settingMenu.SetActive(true);
        resolutionMenu.SetActive(false);
    }
    
    public void ActiveResolutionMenu()
    {
        pauseMenuUi.SetActive(false);
        settingMenu.SetActive(false);
        resolutionMenu.SetActive(true);
    }

    public void SetResolutionSd()
    {
        Screen.SetResolution(640, 480, true);

    }

    public void SetResolutionHd()
    {
        Screen.SetResolution(1280, 720, true);
    }
}
