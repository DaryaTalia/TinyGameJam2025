using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using static EventManager;

public class GameManager : MonoBehaviour
{
    #region Fields

    bool gameStatus;

    double money;
    float skillLevel;
    (float Current, float Max, float Decay) attention = (0.0f, 5.0f, 0.3f);
    (float Current, float Max, float Speed) cycle = (0.0f, 5.0f, 0.4f);

    OnEarnMoney onEarnMoney;
    OnLoseMoney onLoseMoney;
    OnCycleComplete onCycleComplete;
    OnAttentionTooHigh onAttentionTooHigh;
    OnLevelUp onLevelUp;

    GameObject hackSuccessUI;
    GameObject hackFailUI;

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

    public void StartHackSuccess()
    {
        StartCoroutine(DisplayHackSuccessUI());
    }

    IEnumerator DisplayHackSuccessUI()
    {
        hackSuccessUI.SetActive(true);

        float time = 0;
        float duration = 2;

        while(time < duration)
        {
            hackSuccessUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, duration);

            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        while (time > 0)
        {
            hackSuccessUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, duration);

            time -= Time.deltaTime;
            yield return null;
        }

        hackSuccessUI.SetActive(false);
    }



    public void StartHackFail()
    {
        StartCoroutine(DisplayHackFailUI());
    }

    IEnumerator DisplayHackFailUI()
    {
        hackFailUI.SetActive(true);

        float time = 0;
        float duration = 2;

        while (time < duration)
        {
            hackFailUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, duration);

            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        while (time > 0)
        {
            hackFailUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, duration);

            time -= Time.deltaTime;
            yield return null;
        }

        hackFailUI.SetActive(false);
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
