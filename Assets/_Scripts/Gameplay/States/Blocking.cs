using UnityEngine;

public class Blocking : CharacterState
{
    public Blocking(CharacterStateMachine characterController, AnimationClip blockAnim) : base(characterController)
    {
        CurrentState = State.BLOCKING;
        AnimationClip = blockAnim;
    }
}