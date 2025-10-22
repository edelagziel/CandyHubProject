using UnityEngine;

public abstract class BaseShip : MonoBehaviour
{
    public bool IsRevealed { get; private set; } = false;
    
    public ShipType Type { get; private set; } = ShipType.Paper;

    public ShipData Data;
    SpriteRenderer render;

    private void Awake()
    {
        render=gameObject.GetComponent<SpriteRenderer>();
        render.enabled = false;
    }
    public virtual void DestroyShip()
    {
        print("destroy ship");
        render.enabled = false;
    }

    public virtual void RevealShip()
    {
        IsRevealed = true;
        render.enabled = true;
        render.sprite= Data.revealedTypeSprite;
        

    }
    public virtual void SetType(ShipType type)
    {
        Type = type;
    }
}








  
