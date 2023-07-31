using System;
using UnityEngine;
using static BattleManager;
public class Player : MonoBehaviour
{
    public PlayerSide PlayerSide;
    public PlayerEvent PlayerEvent;

    public CharacterStateMachine CharacterStateMachine;
    public CharacterController CharacterController;
    public HealthManager HealthManager;
    public InputManager InputManager;
    public InputEvent InputEvent;
    public bool IsCPU;
    public bool IsReady { get; private set; }

    private void Awake()
    {
        IsReady = false;
    }

    public void Init()
    {
        if (PlayerEvent == null)
        {
            Debug.LogWarning($"{PlayerSide} has not registered to the battle");
            return;
        }

        CharacterController.PlayerEvent = PlayerEvent;
        HealthManager.SetPlayerEvent(PlayerEvent);

        if (!IsCPU)
        {
            CharacterController.SetInputEvent(InputEvent);
            InputManager.InputEvent = InputEvent;
        }
        else
        {
            if (InputManager == null)
            {
                Debug.LogWarning($"{PlayerSide} has not have InputManager");
                return;
            }
            else if (InputManager.InputEvent == null)
            {
                Debug.LogWarning($"{PlayerSide} has not registered InputManager");
                return;
            }
        }

        IsReady = true;
        Debug.Log($"{PlayerSide} Is Ready to battle");
    }
}