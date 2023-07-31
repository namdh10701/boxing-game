using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerEvent")]
public class PlayerEvent : ScriptableObject
{
    public UnityEvent<Punch> PunchThrowed;
    public UnityEvent<Punch> TakeHit;
    public UnityEvent<float> HPChanged;
}