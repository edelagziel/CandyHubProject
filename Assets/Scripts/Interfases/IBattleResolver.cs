using UnityEngine;

public interface IBattleResolver
{
    FightRes Resolve(ShipType playerChoice, ShipType shipType);
}

