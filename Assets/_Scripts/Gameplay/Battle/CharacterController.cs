using System;
using UnityEngine;
using static CharacterState;
using static Dodging;
using static Punching;

public class CharacterController : MonoBehaviour
{
    public CharacterStateMachine StateMachine;
    public CharacterData characterData;
    public InputEvent InputEvent;
    public BattleEvent battleEvent;

    private void Awake()
    {
        InputEvent.Block.AddListener(Block);
        InputEvent.DodgeLeft.AddListener(() => Dodge(DodgingDirection.LEFT));
        InputEvent.DodgeRight.AddListener(() => Dodge(DodgingDirection.RIGHT));
        InputEvent.LowPunch.AddListener(() => Punch(PunchingDirection.LOW));
        InputEvent.HighPunch.AddListener(() => Punch(PunchingDirection.HIGH));
    }

    private void Punch(PunchingDirection direction)
    {
        int random = UnityEngine.Random.Range(0, 2);
        PunchingHand punchHand = random == 0 ? PunchingHand.LEFT : PunchingHand.RIGHT;
        StateMachine.ChangeState((int)State.PUNCHING + (punchHand == PunchingHand.LEFT ? 0 : 1)
            + (direction == PunchingDirection.LOW ? 0 : 2));
    }

    private void Dodge(DodgingDirection direction)
    {
        StateMachine.ChangeState((int)State.DODGING + (direction == DodgingDirection.LEFT ? 0 : 1));
    }

    private void Block()
    {
        StateMachine.ChangeState((int)State.BLOCKING);
    }

    public void Hit()
    {

    }

    public void TakeHit(Punch punch)
    {
        switch (StateMachine.CurrentState)
        {
            case Idle:
                break;
            case Dodging:
                break;
            case Punching:
                break;
            case Blocking:
                break;
        }
    }
}