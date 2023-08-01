using JetBrains.Annotations;
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
    private bool _eventCanceled = false;
    public PunchingHand PunchHand { get; private set; }
    public PunchingDirection PunchDirection { get; private set; }
    public Punching(CharacterStateMachine characterStateMachine, AnimationClip anim, PunchingHand punchHand, PunchingDirection punchDirection,
        StateMachineEvent stateMachineEvent) : base(characterStateMachine, anim)
    {
        StateMachineEvent = stateMachineEvent;
        PunchHand = punchHand;
        PunchDirection = punchDirection;
        CurrentState = State.PUNCHING;
    }

    public override void Update()
    {
        base.Update();
        if (ElapsedTime >= AnimationClip.length / 10 * 6 && !_eventTriggered && !_eventCanceled)
        {
            StateMachineEvent.Punch.Invoke(PunchHand, PunchDirection);
            _eventTriggered = true;
        }
    }

    public override void Enter()
    {
        base.Enter();
        _eventTriggered = false;
        _eventCanceled = false;
    }

    public override void Exit()
    {
        base.Exit();
        _eventTriggered = true;
        _eventCanceled = true;
    }
}