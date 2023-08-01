using UnityEngine;
using UnityEngine.Events;
using static Punching;

[CreateAssetMenu(menuName = "ScriptableObjects/StateMachineEvent")]
public class StateMachineEvent : ScriptableObject
{
    public UnityEvent<PunchingHand, PunchingDirection> Punch = new UnityEvent<PunchingHand, PunchingDirection>();
}