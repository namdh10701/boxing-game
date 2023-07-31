using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public InputEvent InputEvent;
    public CharacterEvent CharacterEvent;
    public CharacterData CharacterData;
}