using UnityEngine;
using static Punching;

public class TakingHit : CharacterState
{
    private PunchingDirection _direction;
    public TakingHit(CharacterStateMachine characterStateMachine, AnimationClip anim, PunchingDirection direction) : base(characterStateMachine, anim)
    {
        CurrentState = State.TAKING_HIT;
        _direction = direction;
    }
}