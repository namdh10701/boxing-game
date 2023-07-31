using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Punching;
public class UIController : MonoBehaviour
{
    public enum PlayerSide
    {
        P1, P2
    }

    [SerializeField] private CharacterEvent _P1Event;
    [SerializeField] private CharacterEvent _P2Event;

    [SerializeField] private CharacterData _P1Data;
    [SerializeField] private CharacterData _P2Data;

    [SerializeField] private Image _P1HPBar;
    [SerializeField] private Image _P2HPBar;

    private Coroutine _updatingHp;
    private float targetHp;

    private void HandlePunchEffect(Punch punch, PlayerSide player)
    {
        if (player == PlayerSide.P1)
        {
            if (punch.Direction == PunchingDirection.LOW)
            {

            }
            else
            {

            }
        }
        else
        {
            if (punch.Direction == PunchingDirection.LOW)
            {

            }
            else
            {

            }
        }
    }

    private void UpdateHPBar(PlayerSide player)
    {
        if (_updatingHp != null)
        {
            StopCoroutine(_updatingHp);
        }
        _updatingHp = StartCoroutine(UpdateHPBarCoroutine(player));
    }

    private IEnumerator UpdateHPBarCoroutine(PlayerSide player)
    {
        Image targetHPBar;
        CharacterData playerData;

        if (player == PlayerSide.P1)
        {
            targetHPBar = _P1HPBar;
            playerData = _P1Data;
        }
        else
        {
            targetHPBar = _P2HPBar;
            playerData = _P2Data;
        }

        float initialFillAmount = targetHPBar.fillAmount;
        float targetFillAmount = playerData.HP / playerData.MaxHP;
        float elapsedTime = 0f;
        float speed = 0.01f * Time.deltaTime;
        float calculatedTime = (targetFillAmount - initialFillAmount) / speed;
        while (elapsedTime <= calculatedTime)
        {
            elapsedTime += Time.deltaTime;
            targetHPBar.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, speed);
            yield return null;
        }
        targetHPBar.fillAmount = targetFillAmount;
    }

    private void OnEnable()
    {
        _P1Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P1));
        _P2Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P2));
    }

    private void OnDisable()
    {
        _P1Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P1));
        _P2Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P2));
    }
}