using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    public ColorEnum colorBrickValue;
    public MeshRenderer meshRenderer;
    public Collider brickCollider;
    public void SetColor(ColorEnum colorEnum)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = colorData.GetColorData(colorEnum);
        colorBrickValue = colorEnum;
    }
    public ColorEnum GetColorBrick()
    {
        Debug.Log(GetColorBrick());
        return colorBrickValue;
    }

    private IEnumerator CoReAppear()
    {
        yield return new WaitForSeconds(5f);
        meshRenderer.enabled = true;
        brickCollider.enabled = true;
    }

    internal void TurnOff()
    {
        meshRenderer.enabled = false;
        brickCollider.enabled = false;
        StartCoroutine(CoReAppear());
    }
}
