using System;
using UnityEngine;

[Serializable]
public class NextActivitySystem
{
    private bool _needInStartPromoActivity;
    private bool _nextPromoActivity;
    private bool _needInStartMenuActivity;
    private bool _nextMenuActivity;

    public bool NextPromoActivity
    {
        get => _nextPromoActivity;
        set => _nextPromoActivity = value;
    }

    public bool NextMenuActivity
    {
        get => _nextMenuActivity;
        set => _nextMenuActivity = value;
    }

    public bool NeedInStartPromoActivity
    {
        get => _needInStartPromoActivity;
        set => _needInStartPromoActivity = value;
    }

    public bool NeedInStartMenuActivity
    {
        get => _needInStartMenuActivity;
        set => _needInStartMenuActivity = value;
    }

    public void TryNext(bool startPromo, bool startMenu)
    {
        if (_nextMenuActivity && startMenu == _needInStartMenuActivity) StartNextMenu();

        if (_nextPromoActivity && startPromo == _needInStartPromoActivity) StartNextPromo();
    }

    private void StartNextPromo()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity.ChangeActivity();
    }

    private void StartNextMenu()
    {
        ((PromoActivity) (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity).MenuAction.NextButton();
    }
}