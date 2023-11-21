using System;
using System.Collections.Generic;
using _Game._Scripts;
using DG.Tweening;
using Project._Game._Scripts.SameSequence;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class Calendar : MonoBehaviour
{
    [SerializeField] private List<Year> years;
    [SerializeField] private List<Year> selected;
    [SerializeField] private InputField inputField;


    private SequenceController _sequenceController;

    private void Start()
    {
        selected = new List<Year>(years.Count);
        _sequenceController = new SequenceController();
        TakeNumberFromInputField();
    }

    public void TakeNumberFromInputField()
    {
        SelectNumber(Int32.Parse(inputField.text));
    }

    [Button]
    public void SelectNumber(int number)
    {
        int age = Config.CURRENT_YEAR - number;

        if (age >= years.Count)
            return;

        if (selected.Count > 0) selected.ForEach(x => _sequenceController._same.Join(x.DeSelect()));

        for (int i = 0; i < age; i++)
        {
            selected.Add(years[i]);
            _sequenceController._same.Join(years[i].SetSelected(number + i));
        }

        for (int i = age; i < years.Count; i++) _sequenceController._same.Join(years[i].SetOnlyText(number + i));

        _sequenceController.PlaySequence(null);
    }
}