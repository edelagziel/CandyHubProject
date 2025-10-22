using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTypeVisual", menuName = "Scriptable Objects/ShipTypeVisual")]
public class ShipTypeVisual : ScriptableObject
{
    public List<ShipSpriteEntry> entries;

   
}




[System.Serializable]
public class ShipSpriteEntry
{
    public ShipType ShipType;
    public Sprite TypeImg;
}
