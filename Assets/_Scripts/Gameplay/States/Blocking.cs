using UnityEngine;

public class Blocking : CharacterState
{
    public bool RequireTimerRun;
    public Blocking(CharacterStateMachine characterController, AnimationClip anim) : base(characterController, anim)
    {
        CurrentState = State.BLOCKING;
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