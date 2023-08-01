using UnityEngine;
using static Player;
public class BattleManager : MonoBehaviour
{
    public BattleData BattleData;
    [SerializeField] private UIController _UIController;
    private Player _p1 { get; set; }
    private Player _p2 { get; set; }

    private void Awake()
    {
        BattleData = BattleData.CreateNewBattleData();
        _UIController.Init(BattleData);
    }

    public void Register(Player player)
    {
        if (player.PlayerSide == Side.P1)
        {
            if (_p1 == null)
            {
                _p1 = player;
                _p1.PlayerData = BattleData.P1Data;
            }
            else
                Debug.LogError("P1 has been assigned");
        }
        else
        {
            if (_p2 == null)
            {
                _p2 = player;
                _p2.PlayerData = BattleData.P2Data;
            }
            else
                Debug.LogError("P2 has been assigned");
        }
        player.Init();
    }

    public void PrepareGame()
    {
        if (_p1 == null || _p2 == null)
        {
            Debug.LogWarning(_p1 == null ? "P1 has not assigned" : ""
                + _p2 == null ? "\n P2 has not assigned" : "");
            return;
        }

        _p1.PlayerData.CharacterEvent.PunchThrowed.AddListener(
            (punch) => _p2.CharacterController.TakeHit(punch)
            );
        _p2.PlayerData.CharacterEvent.PunchThrowed.AddListener(
            (punch) => _p1.CharacterController.TakeHit(punch)
            );
        Debug.Log("Game prepared, ready to begin");
    }

    private void Update()
    {
        if (BattleData.P1Data.CharacterData.HP <= 0
            || BattleData.P2Data.CharacterData.HP <= 0)
        {
            Time.timeScale = 0;
            if (BattleData.P1Data.CharacterData.HP > BattleData.P2Data.CharacterData.HP)
            {
                _UIController.P1Won();
            }
            else
            {

                _UIController.P2Won();
            }
        }
    }
}



