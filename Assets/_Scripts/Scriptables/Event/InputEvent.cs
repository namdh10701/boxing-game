using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/InputEvent")]
public class InputEvent : ScriptableObject
{
    public UnityEvent HighPunch;
    public UnityEvent LowPunch;
    public UnityEvent Block;
    public UnityEvent ReleaseBlock;
    public UnityEvent DodgeLeft;
    public UnityEvent DodgeRight;
}