using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] protected ColorData colorData;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    protected List<GameObject> listBrick = new List<GameObject>();
    protected ColorEnum colorPlayer;
    float height;
    protected int collectedBricks = 0;
    private GameManager color;
    protected virtual void Start()
    {
        color = FindObjectOfType<GameManager>();
        if (color != null)
        {
            colorPlayer = color.RandomColor();
            SetColorPlayer(colorPlayer);
        }
    }

    protected virtual void CollideWithBrick(Collider other)
    {
        if (!other.CompareTag("Brick")) return;
        Brick brick = other.GetComponent<Brick>();
        if (brick.colorBrickValue == colorPlayer)
        {
            EatBrick(brick);
            collectedBricks++;
        }
    }

    protected void SetColorPlayer(ColorEnum colorCharacter)
    {
        Material playerMaterial = colorData.GetColorData(colorCharacter);
        if (playerMaterial != null)
        {
            skinnedMeshRenderer.material = playerMaterial;
        }
    }

    public void EatBrick(Brick brick)
    {
        height = listBrick.Count * 0.2f;
        GameObject go = Instantiate(brickPrefab, bag.transform.position, bag.transform.rotation);
        go.transform.position = new Vector3(bag.transform.position.x, bag.transform.position.y + height, bag.transform.position.z);
        Material goMaterial = colorData.GetColorData(colorPlayer);
        MeshRenderer goMesh = go.GetComponent<MeshRenderer>();
        goMesh.material = goMaterial;
        go.transform.SetParent(bag.transform);
        listBrick.Add(go);
        brick.TurnOff();
    }
}
