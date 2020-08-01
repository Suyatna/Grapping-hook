using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUi;
    public GameObject settingMenu;
    public GameObject resolutionMenu;
    public GameObject loadingScreen;

    public Slider slider;
    
    public int level;
    
    private int[] _slot;

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerData data = SaveSystem.LoadPlayer();
            foreach (var variable in data.slot)
            {
                Debug.Log(variable);
            }
        }
    }

    public void SavePlayer(int index)
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        _slot = new int[5];

        for (int i = 0; i < data.slot.Length; i++)
        {
            _slot[i] = data.slot[i];
        }
        
        _slot[index] = level;
        SaveSystem.SavePlayer(_slot);
    }

    public void LoadLevel(int index)
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        StartCoroutine(LoadAsynchronously(data.slot[index]));
        
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

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Time.timeScale = 1;
        GameIsPause = false;
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

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
