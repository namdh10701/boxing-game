using UnityEngine;

public class Dodging : CharacterState
{
    public enum DodgingDirection
    {
        LEFT, RIGHT
    }
    public DodgingDirection DodgeDirection { get; private set; }

    public Dodging(CharacterStateMachine characterStateMachine, AnimationClip anim, DodgingDirection dodgingDirection) : base(characterStateMachine, anim)
    {
        DodgeDirection = dodgingDirection;
        CurrentState = State.DODGING;
    }
}