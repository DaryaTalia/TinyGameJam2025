using UnityEngine;

[CreateAssetMenu(fileName = "HackProgramSO", menuName = "Scriptable Objects/Hack Program Data")]
public class HackProgramSO : ScriptableObject
{
    public string programName;
    public int minSkill;
    public int attention;
    public int cycleReward;
    public bool unlocked;
    public int current;
    public int max;
    public int upgradeCost;
}
