using System;
using UnityEngine;
using UnityEngine.Events;
using static Player;

[CreateAssetMenu(menuName = "ScriptableObjects/BattleEvent")]
public class BattleData : ScriptableObject
{
    public PlayerData P1Data;
    public PlayerData P2Data;
}