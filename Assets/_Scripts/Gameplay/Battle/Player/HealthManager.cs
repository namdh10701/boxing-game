using UnityEngine;
public class HealthManager : MonoBehaviour
{
    private PlayerEvent _playerEvent;

    public void SetPlayerEvent(PlayerEvent playerEvent)
    {
        _playerEvent = playerEvent;
        _playerEvent.TakeHit.AddListener(punch => UpdateHp(punch));
    }
    public CharacterData PlayerData;

    private void UpdateHp(Punch punch)
    {
        if (punch == null) return;
        _playerEvent.HPChanged.Invoke(punch.Dmg);
    }

    private void OnDisable()
    {
        if (_playerEvent != null)
        {
            _playerEvent.TakeHit.RemoveListener(punch => UpdateHp(punch));
        }
    }
}