using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleText
{
    public string key;
    public bool onlyAnswer = false;
    public bool needSkip = false;
    public bool needRestart = false;
    public bool unlockWithoutSave = false;
    public bool needUnlock = false;
    public string question;
    public ContainerString[] randomQuestion;
    public float delay = 1f;
    public int numberInput;
    public DropdownDialog dropdownDialog;
    public ErrorText errorText;
    public string saveAnswer;
    public List<SingleText> childDialogModels;
    public bool allVariant = false;
    public List<string> variantAnswer;
    public bool refAfter = false;
    public string keyRef = null;
    public string answer;
    public string label;
    public string linkToLabel;
    public bool nextAction = false;
    public TimingVideo timingVideo;
}