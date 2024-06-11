using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    int sceneIndex;
    int nextSceneIndex;
    Character1 character1;
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = sceneIndex + 1;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Victory()
    {
        SceneManager.LoadScene("Level 1");
    }


}