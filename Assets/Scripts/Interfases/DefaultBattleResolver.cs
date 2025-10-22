public class BasicBattleResolver : IBattleResolver
{
    private BattleRulesSet rulesSet;

    public BasicBattleResolver(BattleRulesSet rulesSet)
    {
        this.rulesSet = rulesSet;
    }

    public FightRes Resolve(ShipType playerChoice, ShipType shipType)
    {
        foreach (var rule in rulesSet.rules)
        {
            if (rule.playerType == playerChoice && rule.enemyType == shipType)
            {
                return rule.result;
            }
        }

        return FightRes.Lose;//defult for any problem mathing
    }
}
