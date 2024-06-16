using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public Button replayButton;
    public Button nextLevelButton;
    private void Start()
    {
        replayButton.onClick.AddListener(ReplayLevel);
        nextLevelButton.onClick.AddListener(NextLevel1);
    }
    public void ReplayLevel()
    {
        GameManager levelManager = FindObjectOfType<GameManager>();
        if (levelManager != null)
        {
            levelManager.RePlay();
            Destroy(gameObject);
        }
    }
    public void NextLevel1()
    {
        GameManager levelManager = FindObjectOfType<GameManager>();
        if (levelManager != null)
        {
            Debug.Log("NextLevel");
            levelManager.NextLevel();
            Destroy(gameObject);
        }
    }
}
