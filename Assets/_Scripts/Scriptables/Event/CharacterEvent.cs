using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerEvent")]
public class CharacterEvent : ScriptableObject
{
    public UnityEvent<Punch> PunchThrowed;
    public UnityEvent<float> TakeHit;
    public UnityEvent HPChanged;
}