using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] private BrickSpawner cube;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") || other.CompareTag("Bot"))
        {
            cube.GetComponent<BrickSpawner>().enabled = true;
        }
    }
}
