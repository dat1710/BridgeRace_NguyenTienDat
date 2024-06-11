using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIN : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject victoryPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            GameObject canvas = Instantiate(victoryPanel);
            StopBot();
        }
        if (other.CompareTag("Bot"))
        {
            GameObject canvas = Instantiate(losePanel);
            StopBot();
        }
    }
    void StopBot()
    {
        AIScript[] bots = FindObjectsOfType<AIScript>();
        foreach (var bot in bots)
        {
            bot.enabled = false;
        }
    }
}
