using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/AnimCollection")]
public class AnimCollection : ScriptableObject
{
    public AnimationClip LowPunchLeft;
    public AnimationClip LowPunchRight;
    public AnimationClip Idle;
    public AnimationClip HighPunchLeft;
    public AnimationClip HighPunchRight;
    public AnimationClip DodgeLeft;
    public AnimationClip DodgeRight;
    public AnimationClip Block;
    public AnimationClip TakingLowHit;
    public AnimationClip TakingHighHit;
}