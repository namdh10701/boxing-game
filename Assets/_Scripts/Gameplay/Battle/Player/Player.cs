using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    public enum Side
    {
        P1, P2
    }
    public Side PlayerSide;
    public PlayerData PlayerData { get; set; }
    public CharacterController CharacterController { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public InputManager InputManager { get; private set; }
    public bool IsCPU;
    public bool IsReady { get; private set; }

    private void Awake()
    {
        IsReady = false;
        CharacterController = GetComponent<CharacterController>();
        InputManager = GetComponent<InputManager>();
        HealthManager = GetComponent<HealthManager>();
    }

    public void Init()
    {
        if (PlayerData == null)
        {
            Debug.LogWarning($"{PlayerSide} has not registered to the battle");
            return;
        }
        if (InputManager == null && !IsCPU)
        {
            Debug.LogWarning($"{PlayerSide} has not have InputManager");
            return;
        }

        if (IsCPU)
        {
            CharacterController.Init(PlayerData.CharacterEvent, Side.P2);
        }
        else
        {
            InputManager.Init(PlayerData.InputEvent);
            CharacterController.Init(PlayerData.CharacterEvent, PlayerData.InputEvent, Side.P1);
        }
        HealthManager.Init(PlayerData.CharacterEvent, PlayerData.CharacterData);
        IsReady = true;
        Debug.Log($"{PlayerSide} Is Ready to battle");
    }
}