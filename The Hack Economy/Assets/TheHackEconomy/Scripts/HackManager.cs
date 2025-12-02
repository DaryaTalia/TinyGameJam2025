using System.Collections;
using TMPro;
using UnityEngine;
using static EventManager;

public class HackManager : MonoBehaviour
{
    public static HackManager IHackManager;

    OnPatternSuccess onPatternSuccess;
    OnPatternFail onPatternFail;

    public const string codeKeys = "0123456789ABCDEF";

    int codeLength = 5;
    string nextCode;

    string nextProgramName;

    (float Current, 
        float Max, 
        float Speed, 
        bool Active) hackTimer = (

        5.0f, 
        5.0f, 
        1.0f, 
        false);

    [SerializeField]
    CodeWheelHandler CodeWheel1, CodeWheel2, CodeWheel3, CodeWheel4, CodeWheel5;

    private void Awake()
    {
        if (IHackManager == null)
        {
            IHackManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartHacking(string programName)
    {
        nextProgramName = programName;

        GenerateNewCode();
        Debug.Log(nextCode);

        CodeWheel1.InitializeCodeWheel(nextCode[0].ToString());
        CodeWheel2.InitializeCodeWheel(nextCode[1].ToString());
        CodeWheel3.InitializeCodeWheel(nextCode[2].ToString());
        CodeWheel4.InitializeCodeWheel(nextCode[3].ToString());
        CodeWheel5.InitializeCodeWheel(nextCode[4].ToString());


        hackTimer.Active = true;

        StartCoroutine(Hack());
    }

    IEnumerator Hack()
    {
        while(hackTimer.Current > 0)
        {
            hackTimer.Current -= hackTimer.Speed * Time.deltaTime;
            yield return new WaitForSeconds(1);
        }

        hackTimer.Active = false;
        hackTimer.Current = hackTimer.Max;

        if(CheckInputCodeMatch())
        {
            onPatternSuccess?.Invoke();
        }
        else
        {
            onPatternFail?.Invoke();
        }

        yield return null;
    }

    void ResetCode()
    {
        nextCode = "";
    }

    public string GenerateNewCode()
    {
        ResetCode();

        for (int i = 0; i < codeLength; i++)
        {
            nextCode += codeKeys[Random.Range(0, codeKeys.Length)];
        }

        return nextCode;
    }

    public bool CheckInputCodeMatch()
    {
        if (CodeWheel1.GetComponent<CodeWheelHandler>().specialKey.
            GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[0] != nextCode[0])
        {
            return false;
        }

        if (CodeWheel2.GetComponent<CodeWheelHandler>().specialKey.
            GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[1] != nextCode[1])
        {
            return false;
        }

        if (CodeWheel3.GetComponent<CodeWheelHandler>().specialKey.
            GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[2] != nextCode[2])
        {
            return false;
        }

        if (CodeWheel4.GetComponent<CodeWheelHandler>().specialKey.
            GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[3] != nextCode[3])
        {
            return false;
        }

        if (CodeWheel5.GetComponent<CodeWheelHandler>().specialKey.
            GetComponentInChildren<TextMeshProUGUI>().text.ToCharArray()[4] != nextCode[4])
        {
            return false;
        }

        return true;
    }

    public void DeductTime()
    {

    }
    

}
