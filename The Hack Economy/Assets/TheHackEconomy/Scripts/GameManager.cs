using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static EventManager;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager IGameManager;

    [SerializeField]
    bool gameStatus;

    static double money;

    int skillLevel;
    int skillPoints;
    [SerializeField]
    List<Vector2> skillMap;

    (float Current, float Max, float Decay) attention = (0.0f, 5.0f, 0.3f);
    (float Current, float Max, float Speed) cycle = (0.0f, 5.0f, 0.4f);

    GameObject hackSuccessUI;
    GameObject hackFailUI;

    #endregion

    #region Functions
    public static double Money
    {
        get { return money; }
        set { money = value; }
    }

    private void Awake()
    {
        if (IGameManager == null)
        {
            IGameManager = this;
        } else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (gameStatus)
        {
            IncrementCycle();
            UpdateUI();
        }
    }

    public void PlayGame()
    {
        gameStatus = true;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        gameStatus = false;
        Time.timeScale = 0.0f;
    }

    void IncrementCycle()
    {
        if (cycle.Current >= cycle.Max)
        {
            cycle.Current = 0;

            IncrementAttention();

            CheckLevelUp();

            foreach (HackProgram hp in ProgramManager.IProgramManager.AllPrograms)
            {
                if (hp.Unlocked)
                {
                    money += hp.Current * hp.CycleReward;
                    skillLevel += hp.Current * hp.MinSkill;
                }
            }
        }
        else
        {
            cycle.Current += cycle.Speed * Time.deltaTime;
        }
    }

    void IncrementAttention()
    {
        if (attention.Current >= attention.Max)
        {
            ReduceAttentionConsequence();
        }
        else
        {
            attention.Current -= attention.Decay;
        }
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

        while (time < duration)
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

    void ReduceAttentionConsequence()
    {
        List<HackProgram> ActiveList = new List<HackProgram>();

        foreach (HackProgram hp in ProgramManager.IProgramManager.AllPrograms)
        {
            if (hp.Unlocked)
            {
                ActiveList.Add(hp);
            }
        }

        List<HackProgram> WeighedList = ActiveList.OrderByDescending(hp => hp.Attention).ToList();

        foreach (HackProgram hp in WeighedList)
        {
            if (attention.Current > attention.Max / 2)
            {
                while (hp.Current > 0 && attention.Current > attention.Max / 2)
                {
                    attention.Current -= hp.Attention;
                    hp.ReduceInstances();
                }
            }
            else
            {
                return;
            }
        }
    }

    void CheckLevelUp() 
    {
        foreach (Vector2 level in skillMap)
        {
            if (skillLevel == level.x && skillPoints < level.y)
            {
                skillLevel = (int) level.x;
                break;
            }
            else if (skillLevel == level.x && skillPoints >= level.y)
            {
                skillLevel++;
                skillPoints = 0;
                ProgramManager.IProgramManager.AllPrograms[skillLevel-1].Unlocked = true;
                ProgramManager.IProgramManager.EnableProgram(ProgramManager.IProgramManager.AllPrograms[skillLevel-1]);
            }
        }
    }


    #endregion

    #region UI

    [SerializeField]
    Button menuBuutton;
    [SerializeField]
    Image attentionSlider;
    [SerializeField]
    Image skillSlider;
    [SerializeField]
    Image cycleSlider;
    [SerializeField]
    TextMeshProUGUI moneyText;
    [SerializeField]
    TextMeshProUGUI levelText;
    [SerializeField]
    CanvasGroup attentionTooHighPopup;

    void UpdateUI()
    {
        moneyText.text = "$" + money.ToString("N0");
        levelText.text = "Level " + skillLevel.ToString();

        attentionSlider.fillAmount = attention.Current / attention.Max;
        cycleSlider.fillAmount = cycle.Current / cycle.Max;
        skillSlider.fillAmount = skillPoints / skillMap.Find(v => v.x == skillLevel).y;
    }

    #endregion

}
