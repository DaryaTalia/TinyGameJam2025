using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeWheelHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject key1, key2, key4, key5;
    public GameObject specialKey;
    int key1Pos, key2Pos, specialKeyPos, key4Pos, key5Pos;

    string correctKey = "";
    (float Speed, float Position) wheelSpeed = (0.6f, 0.0f);

    bool hoverActive;

    private void Update()
    {
        if (hoverActive)
        {
            if (wheelSpeed.Position >= wheelSpeed.Speed) 
            {
                RotateKey(ref key1Pos, ref key1);
                RotateKey(ref key2Pos, ref key2);
                RotateKey(ref specialKeyPos, ref specialKey);
                RotateKey(ref key4Pos, ref key4);
                RotateKey(ref key5Pos, ref key5);

                wheelSpeed.Position = 0;
            }
            else
            {
                wheelSpeed.Position += wheelSpeed.Speed * Time.deltaTime;
            }
        }
    }

    public void InitializeCodeWheel(string myKey)
    {
        ResetCorrectKey();

        key1Pos = Random.Range(0, HackManager.codeKeys.Length);

        key2Pos = (key1Pos + 1) % HackManager.codeKeys.Length;
        specialKeyPos = (key2Pos + 1) % HackManager.codeKeys.Length;
        key4Pos = (specialKeyPos + 1) % HackManager.codeKeys.Length;
        key5Pos = (key4Pos + 1) % HackManager.codeKeys.Length;

        SetCorrectKey(myKey);
    }

    void RotateKey(ref int position, ref GameObject go)
    {
        if (position == HackManager.codeKeys.Length - 1)
        {
            position = 0;
        }
        else
        {
            position++;
        }

        go.GetComponentInChildren<TextMeshProUGUI>().text = HackManager.codeKeys[position].ToString();
    }

    public void ResetCorrectKey()
    {
        correctKey = "";
    }

    public void SetCorrectKey(string key)
    {
        correctKey = key;
    }

    public bool VerifyCorrectKey()
    {
        if(specialKey.GetComponentInChildren<TextMeshProUGUI>().text == correctKey)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    { 
        hoverActive = true;
    }
    public void OnPointerExit(PointerEventData eventData) 
    { 
        hoverActive = false;
    }

}
