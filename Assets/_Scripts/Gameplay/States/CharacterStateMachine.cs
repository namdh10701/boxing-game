using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CharacterState;
using static Dodging;
using static Punching;

public class CharacterStateMachine : MonoBehaviour
{
    public Animator Animator;
    private CharacterState[] _characterStates = new CharacterState[10];
    public CharacterState CurrentState { get; private set; }
    public AnimCollection AnimCollection;
    public StateMachineEvent StateMachineEvent { get; private set; }
    private void Awake()
    {
        StateMachineEvent = ScriptableObject.CreateInstance<StateMachineEvent>();
        _characterStates[(int)State.IDLE] = new Idle(this, AnimCollection.Idle);
        _characterStates[(int)State.BLOCKING] = new Blocking(this, AnimCollection.Block);
        _characterStates[(int)State.DODGING] = new Dodging(this, AnimCollection.DodgeLeft, DodgingDirection.LEFT);
        _characterStates[(int)State.DODGING + 1] = new Dodging(this, AnimCollection.DodgeRight, DodgingDirection.RIGHT);
        _characterStates[(int)State.PUNCHING] = new Punching(this, AnimCollection.LowPunchLeft, PunchingHand.LEFT, PunchingDirection.LOW, StateMachineEvent);
        _characterStates[(int)State.PUNCHING + 1] = new Punching(this, AnimCollection.LowPunchRight, PunchingHand.RIGHT, PunchingDirection.LOW, StateMachineEvent);
        _characterStates[(int)State.PUNCHING + 2] = new Punching(this, AnimCollection.HighPunchLeft, PunchingHand.LEFT, PunchingDirection.HIGH, StateMachineEvent);
        _characterStates[(int)State.PUNCHING + 3] = new Punching(this, AnimCollection.HighPunchRight, PunchingHand.RIGHT, PunchingDirection.HIGH, StateMachineEvent);
        _characterStates[(int)State.TAKING_HIT] = new TakingHit(this, AnimCollection.TakingLowHit, PunchingDirection.LOW);
        _characterStates[(int)State.TAKING_HIT + 1] = new TakingHit(this, AnimCollection.TakingHighHit, PunchingDirection.HIGH);
        CurrentState = _characterStates[(int)State.IDLE];
        CurrentState.Enter();

    }

    public void ChangeState(int stateIndex)
    {
        if (stateIndex == (int)State.BLOCKING && CurrentState == _characterStates[(int)State.BLOCKING])
        {
            CurrentState.Enter();
        }
        else if (CurrentState == _characterStates[(int)State.IDLE])
        {
            CurrentState.Exit();
            CurrentState = _characterStates[stateIndex];
            CurrentState.Enter();
        }

    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
            if (CurrentState.RequireExit)
            {
                CurrentState.Exit();
                CurrentState = _characterStates[(int)State.IDLE];
                CurrentState.Enter();
            }
        }
    }

    internal void ExitBlock()
    {
        if (CurrentState is Blocking blocking)
        {
            blocking.RequireTimerRun = true;
        }
    }

    internal void ChangeStateToTakingHit(PunchingDirection direction)
    {
        if (direction == PunchingDirection.LOW)
        {
            CurrentState.Exit();
            CurrentState = _characterStates[(int)State.TAKING_HIT];
            CurrentState.Enter();
        }
        else
        {
            CurrentState.Exit();
            CurrentState = _characterStates[(int)State.TAKING_HIT + 1];
            CurrentState.Enter();
        }
    }
}