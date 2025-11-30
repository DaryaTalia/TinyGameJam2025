using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnLevelUp();
    public delegate void OnAttentionTooHigh();
    public delegate void OnCycleComplete();
    public delegate void OnPatternSuccess();
    public delegate void OnPatternFail();
    public delegate void OnNewProgramInstance();
    public delegate void OnEarnMoney();
    public delegate bool OnLoseMoney(float absValue);
}
