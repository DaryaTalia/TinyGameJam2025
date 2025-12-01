using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class ProgramManager : MonoBehaviour
{
    public static ProgramManager IProgramManager;

    [SerializeField]
    List<HackProgram> allPrograms;

    private void Awake()
    {
        if(IProgramManager == null)
        {
            IProgramManager = this;
        } else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InitializePrograms();
    }

    public List<HackProgram> AllPrograms
    {
        get { return allPrograms; }
    }

    void InitializePrograms()
    {
        foreach (HackProgram program in allPrograms)
        {
            program.GetComponent<CanvasGroup>().alpha = 0;
            program.GetComponent<CanvasGroup>().interactable = false;
            program.GetComponent<CanvasGroup>().blocksRaycasts = false;
            program.GetComponent<CanvasGroup>().ignoreParentGroups = false;
        }

        // Initialize First Program
        EnableProgram(allPrograms[0]);
    }

    public void EnableProgram(HackProgram p)
    {
        allPrograms[0].GetComponent<CanvasGroup>().alpha = 1;
        allPrograms[0].GetComponent<CanvasGroup>().interactable = true;
    }
}
