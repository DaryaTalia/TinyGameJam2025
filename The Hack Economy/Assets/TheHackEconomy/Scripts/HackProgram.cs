using TMPro;
using UnityEngine;
using static EventManager;

public class HackProgram : MonoBehaviour
{
    #region Fields
    [SerializeField]
    string programName;
    [SerializeField]
    int minSkill;
    [SerializeField]
    int attention;
    [SerializeField]
    int cycleReward;
    [SerializeField]
    bool unlocked;
    [SerializeField]
    int current;
    [SerializeField]
    int max;
    [SerializeField]
    int upgradeCost;

    public string ProgramName
    {
        get { return programName; }
    }

    public int MinSkill
    {
        get { return minSkill; }
    }

    public int Attention
    {
        get { return attention; }
    }

    public int CycleReward
    {
        get { return cycleReward; }
    }

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    public int Current
    {
        get { return current; }
    }

    public int Max
    {
        get { return max; }
    }

    public int UpgradeCost
    {
        get { return upgradeCost; }
    }

    OnPatternSuccess onPatternSuccess;

    #endregion

    #region Functions

    private void Start()
    {
        InitializeUI();
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
        if (GameManager.Money >= upgradeCost)
        {
            GameManager.Money -= upgradeCost;
            max++;

            UpdateUI();
        }            
    }

    void NewInstance()
    {
        current++;
        UnsubscribeFromHacking();

        UpdateUI();
    }

    public void ReduceInstances()
    {
        current--;
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
        programNameText.text = programName;
        skillLevelText.text = "Skill Level: " + minSkill;
        attentionLevelText.text = "Attention Level: " + attention;
        cycleRewardText.text = "$" + cycleReward.ToString("N0") + "/cycle";

        UpdateUI();
    }

    public void UpdateUI()
    {
        newInstanceTrackerText.text = "New Instance (" + current + "/" + max + ")";
        instanceLimitTrackerText.text = "Upgrade Instance Limit (-$" + upgradeCost + ")";
    }


    #endregion

}
