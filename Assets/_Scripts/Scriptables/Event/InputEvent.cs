using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/InputEvent")]
public class InputEvent : ScriptableObject
{
    public UnityEvent HighPunch = new UnityEvent();
    public UnityEvent LowPunch = new UnityEvent();
    public UnityEvent Block = new UnityEvent();
    public UnityEvent ReleaseBlock = new UnityEvent();
    public UnityEvent DodgeLeft = new UnityEvent();
    public UnityEvent DodgeRight = new UnityEvent();
}