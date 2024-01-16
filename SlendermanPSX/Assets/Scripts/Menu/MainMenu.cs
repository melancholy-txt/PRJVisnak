using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu, options, loading;
    public string sceneName;

    public void OpenOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }
    public void GoBack()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        menu.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
