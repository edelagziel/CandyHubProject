using UnityEngine;

public class MangerGame : MonoBehaviour
{
    [SerializeField] private BaseShip ship;
    [SerializeField] private BattleRulesSet rules;
    IBattleResolver resolver;
    private void Start()
    {
        ship.SetType(ShipType.Rock);
        resolver = new BasicBattleResolver(rules);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
            FightRes FightState= resolver.Resolve(ShipType.Rock,ship.Type);
            Debug.Log(FightState.ToString());
            ship.RevealShip();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ship.DestroyShip();
        }
    }
}

