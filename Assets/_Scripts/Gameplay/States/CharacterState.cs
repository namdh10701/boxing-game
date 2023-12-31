using UnityEngine;

public abstract class CharacterState
{
    public enum State
    {
        IDLE = 0, BLOCKING = 1, DODGING = 2, PUNCHING = 4,
        TAKING_HIT = 8
    }
    protected Animator _animator;
    public StateMachineEvent StateMachineEvent;
    public State CurrentState { get; protected set; }
    public float ElapsedTime { get; protected set; }
    public bool RequireExit { get; protected set; }
    public AnimationClip AnimationClip { get; set; }

    public CharacterState(CharacterStateMachine characterStateMachine, AnimationClip anim)
    {
        _animator = characterStateMachine.Animator;
        AnimationClip = anim;
    }

    public virtual void Enter()
    {
        ElapsedTime = 0;
        _animator.Play(AnimationClip.name);
    }
    public virtual void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= AnimationClip.length)
        {
            RequireExit = true;
        }
    }
    public virtual void Exit()
    {
        RequireExit = false;
    }
}
