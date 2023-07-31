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
    public BattleEvent BattleEvent;
    public CharacterData P1Data;
    public CharacterData P2Data;

    public Image P1HPBar;
    public Image P2HPBar;


    private Coroutine _updatingHp;

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

    private void UpdateHPBar(float newHp, PlayerSide player)
    {
        if (_updatingHp != null)
        {
            StopCoroutine(_updatingHp);
        }
        _updatingHp = StartCoroutine(UpdateHPBarCoroutine(newHp, player));
    }

    private IEnumerator UpdateHPBarCoroutine(float newHp, PlayerSide player)
    {
        Image targetHPBar;
        CharacterData playerData;

        if (player == PlayerSide.P1)
        {
            targetHPBar = P1HPBar;
            playerData = P1Data;
        }
        else
        {
            targetHPBar = P2HPBar;
            playerData = P2Data;
        }

        float initialFillAmount = targetHPBar.fillAmount;
        float targetFillAmount = playerData.HP / playerData.MaxHP;
        float elapsedTime = 0f;
        float speed = 0.1f;
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
        BattleEvent.P1Event.HPChanged.AddListener((newHp) => UpdateHPBar(newHp, PlayerSide.P1));
        BattleEvent.P2Event.HPChanged.AddListener((newHp) => UpdateHPBar(newHp, PlayerSide.P2));

    }

    private void OnDisable()
    {
        BattleEvent.P1Event.HPChanged.AddListener((newHp) => UpdateHPBar(newHp, PlayerSide.P1));
        BattleEvent.P2Event.HPChanged.AddListener((newHp) => UpdateHPBar(newHp, PlayerSide.P2));
    }
}