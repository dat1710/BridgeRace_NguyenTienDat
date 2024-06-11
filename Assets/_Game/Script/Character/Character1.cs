using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Character1 : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    [SerializeField] private GameObject brick;
    [SerializeField] protected ColorData colorData;
    [SerializeField] private UnityEngine.Object childObject;
    [SerializeField] private Brick brickprefab;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    protected static int lastColorIndex = -1;
    protected static int colorCount = System.Enum.GetValues(typeof(ColorEnum)).Length;
    protected List<GameObject> listBrick = new List<GameObject>();
    protected ColorEnum colorPlayer;
    float height;
    protected int collectedBricks = 0;
    protected virtual void Start()
    {
        SetColorPlayer();
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
        else
        {
            return;
        }
    }
    protected virtual void SetColorPlayer()
    {
        ColorEnum newColor = GetUniqueColor();
        colorPlayer = newColor;
        Material playerMaterial = colorData.GetColorData(colorPlayer);
        skinnedMeshRenderer.material = playerMaterial;
    }

    protected ColorEnum GetUniqueColor()
    {
        lastColorIndex = (lastColorIndex + 1) % colorCount;
        if (lastColorIndex == 0)
        {
            lastColorIndex++;
        }

        return (ColorEnum)lastColorIndex;
    }
    public void EatBrick(Brick brick)
    {
        height = listBrick.Count * 0.2f;
        GameObject go = Instantiate(this.brick, bag.transform.position, bag.transform.rotation);
        go.transform.position = new(bag.transform.position.x, bag.transform.position.y + height, bag.transform.position.z);
        Material goMaterial = colorData.GetColorData(colorPlayer);
        MeshRenderer goMesh = go.GetComponent<MeshRenderer>();
        goMesh.material = goMaterial;
        go.transform.SetParent(bag.transform);
        listBrick.Add(go);
        brick.TurnOff();
    }
}
