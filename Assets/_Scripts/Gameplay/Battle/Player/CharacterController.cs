using UnityEngine;
using static CharacterState;
using static Dodging;
using static Player;
using static Punching;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine StateMachine;
    private InputEvent _inputEvent;
    private CharacterEvent _characterEvent;
    public Side PlayerSide { get; private set; }
    public bool IsCpu { get; private set; }
    public StateMachineEvent StateMachineEvent;


    public void Init(CharacterEvent characterEvent, InputEvent inputEvent, Side playerSide)
    {
        PlayerSide = playerSide;
        IsCpu = false;
        _characterEvent = characterEvent;
        _inputEvent = inputEvent;
        _inputEvent.Block.AddListener(Block);
        _inputEvent.ReleaseBlock.AddListener(StopBlock);
        _inputEvent.DodgeLeft.AddListener(() => Dodge(DodgingDirection.LEFT));
        _inputEvent.DodgeRight.AddListener(() => Dodge(DodgingDirection.RIGHT));
        _inputEvent.LowPunch.AddListener(() => Punch(PunchingDirection.LOW));
        _inputEvent.HighPunch.AddListener(() => Punch(PunchingDirection.HIGH));
    }

    public void Init(CharacterEvent characterEvent, Side playerSide)
    {
        _characterEvent = characterEvent;
        PlayerSide = playerSide;
        IsCpu = true;
    }

    #region All player actions
    private void Dodge(DodgingDirection direction)
    {
        StateMachine.ChangeState((int)State.DODGING + (direction == DodgingDirection.LEFT ? 0 : 1));
    }

    private void Block()
    {
        StateMachine.ChangeState((int)State.BLOCKING);
    }
    private void StopBlock()
    {
        StateMachine.ExitBlock();
    }

    private void Punch(PunchingDirection direction)
    {
        int random = UnityEngine.Random.Range(0, 2);
        PunchingHand punchHand = random == 0 ? PunchingHand.LEFT : PunchingHand.RIGHT;

        StateMachine.ChangeState((int)State.PUNCHING + (punchHand == PunchingHand.LEFT ? 0 : 1)
            + (direction == PunchingDirection.LOW ? 0 : 2));

    }
    private void ThrowPunch(PunchingHand hand, PunchingDirection direction)
    {
        Debug.Log($"{PlayerSide} throwed {hand} {direction} punch");
        Punch punch = new Punch(10, hand, direction);
        _characterEvent.PunchThrowed.Invoke(punch);
    }

    public void TakeHit(Punch punch)
    {
        Debug.Log($"{PlayerSide} took {punch.Dmg} || {punch.PunchHand} || {punch.Direction}");
        float DmgTakePercent = 100;
        switch (StateMachine.CurrentState)
        {
            case Idle:
                DmgTakePercent = 100;
                break;
            case Dodging:
                DmgTakePercent = 0;
                break;
            case Punching:
                DmgTakePercent = 100;
                break;
            case Blocking:
                DmgTakePercent = 50;
                break;
        }
        punch.Dmg *= DmgTakePercent / 100;
        _characterEvent.TakeHit.Invoke(punch.Dmg);
    }
    #endregion

    private void OnEnable()
    {
        StateMachineEvent.Punch.AddListener((hand, direction) =>
        {
            ThrowPunch(hand, direction);
        });
    }
    private void OnDisable()
    {
        if (_inputEvent != null && !IsCpu)
        {
            _inputEvent.Block.RemoveListener(Block);
            _inputEvent.ReleaseBlock.RemoveListener(StopBlock);
            _inputEvent.DodgeLeft.RemoveListener(() => Dodge(DodgingDirection.LEFT));
            _inputEvent.DodgeRight.RemoveListener(() => Dodge(DodgingDirection.RIGHT));
            _inputEvent.LowPunch.RemoveListener(() => Punch(PunchingDirection.LOW));
            _inputEvent.HighPunch.RemoveListener(() => Punch(PunchingDirection.HIGH));
        }
        StateMachineEvent.Punch.RemoveListener((hand, direction) =>
        {
            ThrowPunch(hand, direction);
        });
    }
}