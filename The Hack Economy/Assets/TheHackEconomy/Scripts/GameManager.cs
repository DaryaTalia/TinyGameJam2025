using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    #region Fields

    bool gameStatus;

    double money;
    float skillLevel;
    (float Current, float Max, float Decay) attention = (0.0f, 5.0f, 0.3f);
    (float Current, float Max, float Speed) cycle = (0.0f, 5.0f, 0.4f);

    EventManager.OnEarnMoney onEarnMoney;
    EventManager.OnLoseMoney onLoseMoney;
    EventManager.OnCycleComplete onCycleComplete;
    EventManager.OnAttentionTooHigh onAttentionTooHigh;
    EventManager.OnLevelUp onLevelUp;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onLoseMoney += DeductMoney;
        onEarnMoney += IncrementMoney;
        onEarnMoney += IncrementHackingSkill;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus)
        {
            IncrementCycle();
        }
    }

    void IncrementCycle()
    {
        if (cycle.Current >= cycle.Max)
        {
            onEarnMoney?.Invoke();
            cycle.Current = 0;
        }
        else
        {
            cycle.Current += cycle.Speed * Time.deltaTime;
        }
    }

    bool DeductMoney(float absValue)
    {
        if (money >= absValue)
        {
            money -= absValue;
            return true;
        }

        return false;
    }

    void IncrementMoney()
    {

    }

    void IncrementHackingSkill()
    {

    }

    void IncrementAttention()
    {

    }

    void ReduceAttentionReward()
    {

    }

    void ReduceAttentionConsequence()
    {

    }
}
