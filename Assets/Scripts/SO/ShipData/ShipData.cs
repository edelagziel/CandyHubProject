using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Scriptable Objects/ShipData")]
public class ShipData : ScriptableObject
{
    public string shipName;
    public int AmmountShipTile;
    public string shipDescription;
    public Sprite revealedTypeSprite;
}
