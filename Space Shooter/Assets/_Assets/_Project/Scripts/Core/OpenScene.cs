using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    public string sceneName;

    public void Open()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
