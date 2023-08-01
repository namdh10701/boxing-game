using System.Collections;
using UnityEngine;
using static CharacterState;
using static Dodging;
using static Player;
using static Punching;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    private InputEvent _inputEvent;
    private CharacterEvent _characterEvent;
    public Side PlayerSide { get; private set; }
    public bool IsCpu { get; private set; }

    public void Init(CharacterEvent characterEvent, InputEvent inputEvent, Side playerSide, bool isCpu)
    {
        PlayerSide = playerSide;
        IsCpu = isCpu;
        _characterEvent = characterEvent;
        _inputEvent = inputEvent;
        _inputEvent.Block.AddListener(Block);
        _inputEvent.ReleaseBlock.AddListener(StopBlock);
        _inputEvent.DodgeLeft.AddListener(() => Dodge(DodgingDirection.LEFT));
        _inputEvent.DodgeRight.AddListener(() => Dodge(DodgingDirection.RIGHT));
        _inputEvent.LowPunch.AddListener(() => Punch(PunchingDirection.LOW));
        _inputEvent.HighPunch.AddListener(() => Punch(PunchingDirection.HIGH));

        _characterStateMachine.StateMachineEvent.Punch.AddListener((hand, direction) =>
        {
            ThrowPunch(hand, direction);
        });

        if (IsCpu)
        {
            Debug.Log("asds");
            StartCoroutine(RandomCommand());
        }
    }

    public IEnumerator RandomCommand()
    {
        while (true)
        {
            int randomNum = Random.Range(0, 5);
            yield return new WaitForSeconds(.2f);

            switch (randomNum)
            {
                case 0:
                    _inputEvent.DodgeLeft.Invoke();
                    break;
                case 1:
                    _inputEvent.DodgeRight.Invoke();
                    break;
                case 2:
                    _inputEvent.LowPunch.Invoke();
                    break;
                case 3:
                    _inputEvent.HighPunch.Invoke();
                    break;
            }
        }
    }



    #region All player actions
    private void Dodge(DodgingDirection direction)
    {
        _characterStateMachine.ChangeState((int)State.DODGING + (direction == DodgingDirection.LEFT ? 0 : 1));
    }

    private void Block()
    {
        _characterStateMachine.ChangeState((int)State.BLOCKING);
    }
    private void StopBlock()
    {
        _characterStateMachine.ExitBlock();
    }

    private void Punch(PunchingDirection direction)
    {
        int random = UnityEngine.Random.Range(0, 2);
        PunchingHand punchHand = random == 0 ? PunchingHand.LEFT : PunchingHand.RIGHT;

        _characterStateMachine.ChangeState((int)State.PUNCHING + (punchHand == PunchingHand.LEFT ? 0 : 1)
            + (direction == PunchingDirection.LOW ? 0 : 2));

    }
    private void ThrowPunch(PunchingHand hand, PunchingDirection direction)
    {
        Debug.Log($"{PlayerSide} throwed {hand} {direction} punch");
        Punch punch = new Punch(7, hand, direction);
        _characterEvent.PunchThrowed.Invoke(punch);
    }

    public void TakeHit(Punch punch)
    {
        Debug.Log($"{PlayerSide} took {punch.Dmg} || {punch.PunchHand} || {punch.Direction}");
        float DmgTakePercent = 100;
        switch (_characterStateMachine.CurrentState)
        {
            case Idle:
                DmgTakePercent = 100;
                punch.Dmg *= DmgTakePercent / 100;
                _characterStateMachine.ChangeStateToTakingHit(punch.Direction);
                _characterEvent.TakeHit.Invoke(punch);
                break;
            case Dodging:
                DmgTakePercent = 0;
                break;
            case Punching:
                DmgTakePercent = 100;
                punch.Dmg *= DmgTakePercent / 100;
                _characterStateMachine.ChangeStateToTakingHit(punch.Direction);
                _characterEvent.TakeHit.Invoke(punch);
                break;
            case Blocking:
                DmgTakePercent = 50;
                punch.Dmg *= DmgTakePercent / 100;
                _characterStateMachine.ChangeStateToTakingHit(punch.Direction);
                _characterEvent.TakeHit.Invoke(punch);
                break;
        }
    }
    #endregion

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
        _characterStateMachine.StateMachineEvent.Punch.RemoveListener((hand, direction) =>
        {
            ThrowPunch(hand, direction);
        });
    }
}