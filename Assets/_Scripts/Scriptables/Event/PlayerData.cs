using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public InputEvent InputEvent;
    public CharacterEvent CharacterEvent;
    public CharacterData CharacterData;

    public static PlayerData CreateNewPlayerData()
    {
        PlayerData newPlayerData = CreateInstance<PlayerData>();
        newPlayerData.InputEvent = CreateInstance<InputEvent>();
        newPlayerData.CharacterEvent = CreateInstance<CharacterEvent>();
        newPlayerData.CharacterData = CreateInstance<CharacterData>();
        newPlayerData.CharacterData.MaxHP = 100;
        newPlayerData.CharacterData.HP = 100;
        return newPlayerData;
    }
}