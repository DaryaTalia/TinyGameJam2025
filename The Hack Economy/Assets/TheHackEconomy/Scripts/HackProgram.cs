using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static EventManager;

public class HackProgram : MonoBehaviour
{
    #region Fields
    [SerializeField]
    HackProgramSO data;

    public string ProgramName
    {
        get { return data.programName; }
    }

    public int MinSkill
    {
        get { return data.minSkill; }
    }

    public int Attention
    {
        get { return data.attention; }
    }

    public int CycleReward
    {
        get { return data.cycleReward; }
    }

    public bool Unlocked
    {
        get { return data.unlocked; }
        set { data.unlocked = value; }
    }

    public int Current
    {
        get { return data.current; }
    }

    public int Max
    {
        get { return data.max; }
    }

    public int UpgradeCost
    {
        get { return data.upgradeCost; }
    }

    OnPatternSuccess onPatternSuccess;

    #endregion

    #region Functions

    private void Start()
    {
        InitializeUI();
    }

    public void SetNextProgram()
    {
        HackManager.IHackManager.StartHacking(data.programName);
    }

    public void SubscribeToHacking()
    {
        onPatternSuccess += NewInstance;
    }

    void UnsubscribeFromHacking()
    {
        onPatternSuccess -= NewInstance;
    }

    public void UpgradeInstanceLimit()
    {
        if (GameManager.Money >= data.upgradeCost)
        {
            GameManager.Money -= data.upgradeCost;
            data.max++;

            UpdateUI();
        }            
    }

    void NewInstance()
    {
        data.current++;
        UnsubscribeFromHacking();

        UpdateUI();
    }

    public void ReduceInstances()
    {
        data.current--;
    }


    #endregion

    #region UI
    [SerializeField]
    TextMeshProUGUI programNameText;
    [SerializeField]
    TextMeshProUGUI skillLevelText;
    [SerializeField]
    TextMeshProUGUI attentionLevelText;
    [SerializeField]
    TextMeshProUGUI cycleRewardText;

    [SerializeField]
    TextMeshProUGUI newInstanceTrackerText;
    [SerializeField]
    TextMeshProUGUI instanceLimitTrackerText;

    void InitializeUI()
    {
        programNameText.text = data.programName;
        skillLevelText.text = "Skill Level: " + data.minSkill;
        attentionLevelText.text = "Attention Level: " + data.attention;
        cycleRewardText.text = "$" + data.cycleReward.ToString("N0") + "/cycle";

        UpdateUI();
    }

    public void UpdateUI()
    {
        newInstanceTrackerText.text = "New Instance (" + data.current + "/" + data.max + ")";
        instanceLimitTrackerText.text = "Upgrade Instance Limit (-$" + data.upgradeCost + ")";
    }

    #endregion

}
