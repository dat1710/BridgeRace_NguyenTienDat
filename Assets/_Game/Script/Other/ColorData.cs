using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

[CreateAssetMenu(fileName = "ColorData", menuName = "Scriptable Objects/Color Data")]
public class ColorData : ScriptableObject
{
    public List<Material> colors;
    public Material GetColorData(ColorEnum brickColor)
    {
        return colors[(int)brickColor];
    }
}

public enum ColorEnum
{
    None = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
}