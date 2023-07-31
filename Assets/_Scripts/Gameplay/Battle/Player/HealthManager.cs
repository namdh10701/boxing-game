using UnityEngine;
public class HealthManager : MonoBehaviour
{
    private CharacterEvent _characterEvent;
    private CharacterData _characterData;

    public void Init(CharacterEvent characterEvent, CharacterData characterData)
    {
        _characterEvent = characterEvent;
        _characterData = characterData;
        _characterEvent.TakeHit.AddListener(dmgTook => UpdateHp(dmgTook));
    }
    private void UpdateHp(float dmgTook)
    {
        _characterData.HP -= dmgTook;
        _characterEvent.HPChanged.Invoke();
    }

    private void OnDisable()
    {
        if (_characterEvent != null)
        {
            _characterEvent.TakeHit.RemoveListener(punch => UpdateHp(punch));
        }
    }
}