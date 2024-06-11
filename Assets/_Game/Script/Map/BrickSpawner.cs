using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject parent;
    private void Start()
    {
        float y = cube.transform.position.y;
        float z = parent.transform.position.z;
        for (int i = -20; i <= 20; i=i+4)
        {
            for (int j = -20;j<= 0; j=j+4)
            {
                Brick newBrick = Instantiate(brickPrefab,new Vector3(i,y,j + z), Quaternion.identity);
                ColorEnum colorEnum = (ColorEnum)Random.Range(1, System.Enum.GetValues(typeof(ColorEnum)).Length);
                GameObject brickGameObject = newBrick.gameObject;
                newBrick.SetColor(colorEnum);
                newBrick.transform.SetParent(parent.transform);
            }
        }
    }
}
