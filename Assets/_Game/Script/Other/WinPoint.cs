using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject victoryPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            GameObject canvas = Instantiate(victoryPanel);
        }
        if (other.CompareTag("Bot"))
        {
            GameObject canvas = Instantiate(losePanel);
        }
    }
}
