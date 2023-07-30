using UnityEngine;

public class Idle : CharacterState
{
    public Idle(CharacterStateMachine characterStateMachine, AnimationClip anim) : base(characterStateMachine)
    {
        CurrentState = State.IDLE;
        AnimationClip = anim;
    }
}