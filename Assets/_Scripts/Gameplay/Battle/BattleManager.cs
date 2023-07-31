using UnityEngine;
public class BattleManager : MonoBehaviour
{
    public enum PlayerSide
    {
        P1, P2
    }
    public enum BattleState
    {
        INTRODUCING, RUNNING, PAUSE, STOP
    }
    private Player[] _players;
    public BattleEvent BattleEvent;

    private void Awake()
    {
        _players = new Player[2];
    }
    public Player Register(Player player)
    {
        int playerIndex = _players.GetNearestAvailablePos();
        if (playerIndex != -1)
        {
            player.PlayerSide = playerIndex == 0 ? PlayerSide.P1 : PlayerSide.P2;
            _players[playerIndex] = player;
            BattleEvent.Register(player);
            player.Init();
            return player;
        }
        return null;
    }
    public void StartGame()
    {
    }
}