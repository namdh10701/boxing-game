using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Player cpu;
    public BattleManager BattleManager;

    private void Start()
    {
        BattleManager.Register(player);
        BattleManager.Register(cpu);
        BattleManager.PrepareGame();
        //BattleManager.StartGame();
    }
}