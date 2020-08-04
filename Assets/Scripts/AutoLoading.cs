using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLoading : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.isLoadScene = false;
        
        StartCoroutine(LoadAsynchronously(2));
        Time.timeScale = 1;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float waitTime = 0;
        
        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            waitTime += 0.0015f;

            if (waitTime >= 1)
            {
                operation.allowSceneActivation = true;
            }
            else
            {
                slider.value = waitTime;   
            }

            yield return null;
        }
    }
}
