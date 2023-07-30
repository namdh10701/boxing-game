using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/BattleEvent")]
public class BattleEvent : ScriptableObject
{
    public UnityEvent Hit;
    public UnityEvent<Punch> TakeHit;
}