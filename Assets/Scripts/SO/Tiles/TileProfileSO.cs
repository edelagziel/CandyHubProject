using UnityEngine;

public enum TileType { None, Rock, Paper, Scissors }

[CreateAssetMenu(fileName = "Tile Profile", menuName = "Scriptable Objects/Tle (past) ")]

public class TileProfileSO : ScriptableObject
{
    public TileType TileType;
    public Sprite Icon;
}
