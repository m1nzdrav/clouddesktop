using System;
using System.Collections.Generic;
using _Game._Scripts.PromoActivity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PromoActivity : AbstractPromo
{
    [SerializeField] private QueueAction menuAction;
    [SerializeField] private List<BreadButton> _breadButtons;

    [SerializeField, ReadOnly] private Activity _currentActivity;

    [SerializeField, ReadOnly] private BreadcrumbsActivity _currentBreadcrumbs;

    [SerializeField, ReadOnly] private int currentBread = 0;

    public int CurrentBread
    {
        get => currentBread;
        set
        {
            _breadButtons.Find(b => b.CountBread == value - 1)?.Activate();
            _breadButtons.Find(b => b.CountBread == currentBread - 1)?.ReActivate();

            currentBread = value;
        }
    }

    [SerializeField, ReadOnly] private int currentBreadCount = -1;

    public QueueAction MenuAction
    {
        get
        {
            if (menuAction != null)
                return menuAction;
            return null;
        }
        set => menuAction = value;
    }

    private void Awake()
    {
        if (RegisterSingleton._instance != null)
            (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.SetPromoActivity(this);
    }

    public void Start()
    {
        currentBreadCount = -1;
        FastChangeActivity(
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad).GetBreadValue(SceneManager.GetActiveScene().name));
        // ChangeActivity();
    }

    [Button]
    public void StartCurrentActivity(int bread)
    {
        currentBread = bread;
        currentBreadCount = -1;
        ChangeActivity();
    }

    [Button]
    public override void ChangeActivity()
    {
        currentBreadCount++;
        
        CheckNumberBread(CurrentBread, currentBreadCount).FirstStart();
    }

    private Activity CheckNumberBread(int numberBread, int currentNumber)
    {
        if (_breadcrumbsStock[numberBread]._activityStock.Count - 1 >= currentNumber)
            return _breadcrumbsStock[numberBread]._activityStock[currentNumber];

        currentBreadCount = 0;
        return CheckNumberBread(++CurrentBread,
            currentBreadCount);
    }


    [SerializeField, ReadOnly] private int numberReversBread;

    [Button]
    public void Reverse(int numberToReverse)
    {
        numberReversBread = numberToReverse;
        Revers();
    }

    public override void Revers()
    {
        if (currentBread < numberReversBread) return;


        if (currentBread == numberReversBread && currentBreadCount == 0)
            _breadcrumbsStock[currentBread]._activityStock[currentBreadCount--].ReverseActivity(false);
        else
            _breadcrumbsStock[currentBread]._activityStock[currentBreadCount--].ReverseActivity(true);

        if (currentBreadCount >= 0) return;


        if (currentBread != numberReversBread)
        {
            CurrentBread--;
            currentBreadCount = _breadcrumbsStock[currentBread]._activityStock.Count - 1;
        }
        else
        {
            currentBreadCount = -1;
            ChangeActivity();
        }
    }

    [Button]
    public void FastChangeActivity(int activity)
    {
        for (int i = 0; i < activity; i++)
        {
            foreach (var VARIABLE in _breadcrumbsStock[i]._activityStock)
            {
                VARIABLE.FastStart();
            }
        }
        StartCurrentActivity(activity);
    }

    private int GetCurrentActivity()
    {
        int count = 0;
        for (var index = 0; index < currentBread; index++)
        {
            count += _breadcrumbsStock[index]._activityStock.Count;
        }

        return count + currentBreadCount;
    }
}