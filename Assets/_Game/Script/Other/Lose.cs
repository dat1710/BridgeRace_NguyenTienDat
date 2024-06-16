using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public Button replayButton;
    public Button exitButton;
    private void Start()
    {
        replayButton.onClick.AddListener(ReplayLevel);
        exitButton.onClick.AddListener(QuitGame);
    }

    public void ReplayLevel()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.RePlay();
            Destroy(gameObject);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
