using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Battles", menuName = "Scriptable Objects/Battle Rules Set")]

public class BattleRulesSet : ScriptableObject
{
    [Serializable]
    public struct RuleEntry
    {
        public ShipType playerType;
        public ShipType enemyType;
        public FightRes result;
    }

    public List<RuleEntry> rules = new List<RuleEntry>();
}
