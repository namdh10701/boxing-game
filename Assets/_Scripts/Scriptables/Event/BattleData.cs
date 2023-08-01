using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BattleEvent")]
public class BattleData : ScriptableObject
{
    public PlayerData P1Data;
    public PlayerData P2Data;

    public static BattleData CreateNewBattleData()
    {
        BattleData newBattleData = CreateInstance<BattleData>();
        newBattleData.P1Data = PlayerData.CreateNewPlayerData();
        newBattleData.P2Data = PlayerData.CreateNewPlayerData();
        return newBattleData;
    }
}