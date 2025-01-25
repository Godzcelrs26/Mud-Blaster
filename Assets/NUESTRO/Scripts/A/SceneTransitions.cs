using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string gameScene;
    public string menuScene;
    public static SceneTransitions instance;

    public Animator animator;
    
    public string startTrigger = "Show";
    public string endTrigger = "End";
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void GoToGame()
    {
        StartCoroutine(LoadScene(gameScene));
    }

    public void GoToMenu()
    {
        StartCoroutine(LoadScene(menuScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        animator.SetTrigger(startTrigger);
        
        yield return new WaitForSecondsRealtime(1);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        animator.SetTrigger(endTrigger);
    }

    public void Quit()
    {
        Application.Quit();
    }

}