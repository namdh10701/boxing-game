using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerEvent")]
public class CharacterEvent : ScriptableObject
{
    public UnityEvent<Punch> PunchThrowed = new UnityEvent<Punch>();
    public UnityEvent<Punch> TakeHit = new UnityEvent<Punch>();
    public UnityEvent HPChanged = new UnityEvent();
}