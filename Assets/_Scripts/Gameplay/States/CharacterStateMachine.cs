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
    private CharacterState[] _characterStates = new CharacterState[8];
    public CharacterState CurrentState { get; private set; }
    public AnimCollection AnimCollection;
    private void Awake()
    {
        _characterStates[(int)State.IDLE] = new Idle(this, AnimCollection.Idle);
        _characterStates[(int)State.BLOCKING] = new Blocking(this, AnimCollection.Block);
        _characterStates[(int)State.DODGING] = new Dodging(this, AnimCollection.DodgeLeft, DodgingDirection.LEFT);
        _characterStates[(int)State.DODGING + 1] = new Dodging(this, AnimCollection.DodgeRight, DodgingDirection.RIGHT);
        _characterStates[(int)State.PUNCHING] = new Punching(this, AnimCollection.LowPunchLeft, PunchingHand.LEFT, PunchingDirection.LOW);
        _characterStates[(int)State.PUNCHING + 1] = new Punching(this, AnimCollection.LowPunchRight, PunchingHand.RIGHT, PunchingDirection.LOW);
        _characterStates[(int)State.PUNCHING + 2] = new Punching(this, AnimCollection.HighPunchLeft, PunchingHand.LEFT, PunchingDirection.HIGH);
        _characterStates[(int)State.PUNCHING + 3] = new Punching(this, AnimCollection.HighPunchRight, PunchingHand.RIGHT, PunchingDirection.HIGH);

        CurrentState = _characterStates[(int)State.IDLE];
        CurrentState.Enter();
    }

    public void ChangeState(int stateIndex)
    {
        if (CurrentState == _characterStates[(int)State.IDLE])
        {
            CurrentState.Exit();
            CurrentState = _characterStates[stateIndex];
            CurrentState.Enter();
        }
    }

    private void Update()
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