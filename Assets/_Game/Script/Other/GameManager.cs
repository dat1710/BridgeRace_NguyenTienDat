using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private List<ColorEnum> allColors = new List<ColorEnum>((ColorEnum[])System.Enum.GetValues(typeof(ColorEnum)));
    private List<ColorEnum> usedColors = new List<ColorEnum>();
    public GameObject[] levelPrefabs;
    public GameObject playerPrefab;
    public GameObject botPrefab;
    public Canvas startCanvas;
    private int currentLevelIndex;
    private GameObject currentLevelInstance;
    private UnityEvent onReplay;
    public UnityEvent onNextLevel;
    private UnityEvent onExitGame;
    private UnityEvent startUI;
    private GameObject[] spawnPoints = new GameObject[3];

    void Start()
    {
        if (onReplay == null)
            onReplay = new UnityEvent();
        if (onNextLevel == null)
            onNextLevel = new UnityEvent();
        if (onExitGame == null)
            onExitGame = new UnityEvent();
        if (startUI == null)
            startUI = new UnityEvent();
        startUI.AddListener(TurnOnMenu);
        onExitGame.AddListener(ExitGame);
        onNextLevel.AddListener(NextLevel);
        onReplay.AddListener(RePlay);
    }

    public void StartGame()
    {
        if (startCanvas != null)
        {
            startCanvas.gameObject.SetActive(false);
            LoadLevel(0);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        if (levelIndex >= levelPrefabs.Length)
        {
            return;
        }

        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
        }

        currentLevelInstance = Instantiate(levelPrefabs[levelIndex]);
        GameObject[] foundSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnCharacter");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i < foundSpawnPoints.Length && foundSpawnPoints[i] != null)
            {
                spawnPoints[i] = foundSpawnPoints[i];
            }
            else
            {
                spawnPoints[i] = null;
            }
        }
        if (spawnPoints.Length > 0 && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoints[0].transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            GameObject bot = Instantiate(botPrefab, spawnPoints[i].transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
    public void TurnOnMenu() 
    {
        startCanvas.gameObject.SetActive(true);
        Destroy(gameObject);
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void RePlay()
    {
        ResetGameEnvironment();
        LoadLevel(currentLevelIndex);
    }

    public void NextLevel()
    {
        ResetGameEnvironment();
        int nextLevelIndex = currentLevelIndex + 1;
        LoadLevel(nextLevelIndex);
    }

    private void ResetGameEnvironment()
    {
        ResetColors();
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Destroy(spawnPoint);
            }
        }
        foreach (var character in GameObject.FindGameObjectsWithTag("Character"))
        {
            Destroy(character);
        }
        foreach (var bot in GameObject.FindGameObjectsWithTag("Bot"))
        {
            Destroy(bot);
        }
    }

    public ColorEnum RandomColor()
    {
        ColorEnum randomColor = allColors[Random.Range(1, allColors.Count)];
        usedColors.Add(randomColor);
        allColors.Remove(randomColor);
        return randomColor;
    }

    public void ResetColors()
    {
        allColors = new List<ColorEnum>((ColorEnum[])System.Enum.GetValues(typeof(ColorEnum)));
        usedColors.Clear();
    }

}
