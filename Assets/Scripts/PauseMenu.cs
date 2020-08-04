using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public static bool GameIsPause = false;

    public GameObject pauseMenuUi;
    public GameObject menuUi;
    public GameObject settingMenu;
    public GameObject resolutionMenu;
    public GameObject deadMenu;
    public GameObject finishMenu;
    public GameObject loadingScreen;
    public GameObject saveButton;

    public Slider slider;

    [Header("toggle")] public GameObject toggleFullScreen;

    private int _sceneIndex;

    private bool _isMainMenu = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (toggleFullScreen == null)
        {
            toggleFullScreen = GameObject.FindWithTag("Toggle");
        }
        else
        {
            Screen.fullScreen = toggleFullScreen.GetComponent<Toggle>().isOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isMainMenu)
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

    public void Death()
    {
        deadMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPause = true;
    }
    
    public void ResetScene()
    {
        GameManager.Manager.isLoadScene = false;
        
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
        _isMainMenu = false;
        
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Time.timeScale = 1;
        GameIsPause = false;
    }

    public void LoadToMainMenu()
    {
        _isMainMenu = true;
        
        StartCoroutine(LoadAsynchronously(0));
    }
    
    public void LoadFromData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        _sceneIndex = data.saveSceneIndex;
        
        Debug.Log("scene: " + _sceneIndex);
        
        StartCoroutine(LoadAsynchronously(_sceneIndex));
        Time.timeScale = 1;
        GameIsPause = false;
    }
    
    public void LoadSlot(string slot)
    {
        SaveSystem.LoadSlot = "/hook" + slot + ".fun";

        GameManager.Manager.isLoadScene = true;
        GameManager.Manager.slot = slot;
        
        _isMainMenu = false;
        
        LoadFromData();
    }
    
    public void SaveSlot(string slot)
    {
        GameObject player = GameObject.FindWithTag("Player");
        
        SaveSystem.SaveSlot = "/hook" + slot + ".fun";
        
        player.GetComponent<Player>().SavePlayer();
    }

    public void ActivePauseMenu()
    {
        if (_isMainMenu)
        {
            menuUi.SetActive(true);
            settingMenu.SetActive(false);
            resolutionMenu.SetActive(false); 
        }
        else
        {
            pauseMenuUi.SetActive(true);
            settingMenu.SetActive(false);
            resolutionMenu.SetActive(false);   
        }
    }

    public void ActiveSettingMenu()
    {
        if (_isMainMenu)
        {
            pauseMenuUi.SetActive(false);
            settingMenu.SetActive(true);
            resolutionMenu.SetActive(false);
        }
        else
        {
            pauseMenuUi.SetActive(false);
            settingMenu.SetActive(true);
            resolutionMenu.SetActive(false);
            saveButton.SetActive(true);
        }
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

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
