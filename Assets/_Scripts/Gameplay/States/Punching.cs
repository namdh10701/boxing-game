﻿using JetBrains.Annotations;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class Punching : CharacterState
{
    public enum PunchingHand
    {
        LEFT, RIGHT
    }
    public enum PunchingDirection
    {
        LOW, HIGH
    }

    private bool _eventTriggered = false;
    public StateMachineEvent StateMachineEvent;
    public PunchingHand PunchHand { get; private set; }
    public PunchingDirection PunchDirection { get; private set; }
    public Punching(CharacterStateMachine characterStateMachine, AnimationClip anim, PunchingHand punchHand, PunchingDirection punchDirection,
        StateMachineEvent stateMachineEvent) : base(characterStateMachine)
    {
        StateMachineEvent = stateMachineEvent;
        PunchHand = punchHand;
        PunchDirection = punchDirection;
        CurrentState = State.PUNCHING;
        AnimationClip = anim;
    }

    public override void Update()
    {
        base.Update();
        if (ElapsedTime >= AnimationClip.length / 2 && !_eventTriggered)
        {
            Debug.Log("Punch");
            StateMachineEvent.Punch.Invoke(PunchHand, PunchDirection);
            _eventTriggered = true;
        }
    }

    public override void Enter()
    {
        base.Enter();
        _eventTriggered = false;
    }
}