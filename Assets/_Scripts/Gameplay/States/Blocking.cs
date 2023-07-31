using UnityEngine;

public class Blocking : CharacterState
{
    public bool RequireTimerRun;
    public Blocking(CharacterStateMachine characterController, AnimationClip blockAnim) : base(characterController)
    {
        CurrentState = State.BLOCKING;
        AnimationClip = blockAnim;
    }

    public override void Enter()
    {
        base.Enter();
        RequireTimerRun = false;
    }
    public override void Update()
    {
        if (RequireTimerRun)
        {
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= AnimationClip.length)
            {
                RequireExit = true;
            }
        }
    }
}