using UnityEngine;

public enum TileType { Rock, Paper, Scissors }

[CreateAssetMenu(fileName = "Tile Profile", menuName = "Tiles/Create tile profile")]
public class TileProfileSO : ScriptableObject
{
    public TileType TileType;
    public Sprite Icon;
}
