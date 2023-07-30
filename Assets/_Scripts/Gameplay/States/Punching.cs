using UnityEngine;

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
    public PunchingHand PunchHand { get; private set; }
    public PunchingDirection PunchDirection { get; private set; }
    public Punching(CharacterStateMachine characterStateMachine, AnimationClip anim, PunchingHand punchHand, PunchingDirection punchDirection) : base(characterStateMachine)
    {
        PunchHand = punchHand;
        PunchDirection = punchDirection;
        CurrentState = State.PUNCHING;
        AnimationClip = anim;
    }
}