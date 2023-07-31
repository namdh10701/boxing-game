using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/BattleEvent")]
public class BattleEvent : ScriptableObject
{
    public PlayerEvent P1Event;
    public PlayerEvent P2Event;

    public InputEvent P1Input;
    public InputEvent P2Input;

    public CharacterData P1Data;
    public CharacterData P2Data;

    public StateMachineEvent P1StateMachine;
    public StateMachineEvent P2StateMachine;


    public void Register(Player player)
    {
        if (player.PlayerSide == BattleManager.PlayerSide.P1)
        {
            player.PlayerEvent = P1Event;
            player.HealthManager.PlayerData = P1Data;
            player.CharacterController.SetStateMachineEvent(P1StateMachine);
            player.CharacterStateMachine.SetStateMachineEvent(P1StateMachine);

            if (!player.IsCPU)
                player.InputEvent = P1Input;

            P2Event.PunchThrowed.AddListener((punch) => player.CharacterController.TakeHit(punch));
        }
        else
        {
            player.PlayerEvent = P2Event;
            player.HealthManager.PlayerData = P2Data;
            player.CharacterController.SetStateMachineEvent(P2StateMachine);
            player.CharacterStateMachine.SetStateMachineEvent(P2StateMachine);
            if (!player.IsCPU)
                player.InputEvent = P2Input;

            P1Event.PunchThrowed.AddListener((punch) => player.CharacterController.TakeHit(punch));
        }
    }
}