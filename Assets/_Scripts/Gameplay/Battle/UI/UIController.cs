using System;
using System.Collections;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;
using static Punching;

public class UIController : MonoBehaviour
{
    public enum PlayerSide
    {
        P1, P2
    }

    private CharacterEvent _P1Event;
    private CharacterEvent _P2Event;

    private CharacterData _P1Data;
    private CharacterData _P2Data;

    [SerializeField] private Image _P1HPBar;
    [SerializeField] private Image _P2HPBar;

    [SerializeField] private GameObject _P1Won;
    [SerializeField] private GameObject _P1Lose;

    private Coroutine _updatingHp;

    public void Init(BattleData battleData)
    {
        _P1Event = battleData.P1Data.CharacterEvent;
        _P2Event = battleData.P2Data.CharacterEvent;

        _P1Data = battleData.P1Data.CharacterData;
        _P2Data = battleData.P2Data.CharacterData;


        _P1Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P1));
        _P2Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P2));


    }

    private void UpdateHPBar(PlayerSide player)
    {/*
        if (_updatingHp != null)
        {
            StopCoroutine(_updatingHp);
        }
        _updatingHp = StartCoroutine(UpdateHPBarCoroutine(player));*/
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
        targetHPBar.fillAmount = playerData.HP / playerData.MaxHP;

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
        float progress = 0;
        float duration = 1;
        while (progress <= 1)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / duration;
            targetHPBar.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, progress);
            yield return null;
        }
        targetHPBar.fillAmount = targetFillAmount;
    }

    private void OnDisable()
    {
        _P1Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P1));
        _P2Event.HPChanged.AddListener(() => UpdateHPBar(PlayerSide.P2));
    }

    internal void P1Won()
    {
        _P1Won.SetActive(true);
    }

    internal void P2Won()
    {
        _P1Lose.SetActive(true);
    }
}