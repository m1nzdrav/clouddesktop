using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class AbstractPromo : MonoBehaviour
{
    [SerializeField] protected List<BreadcrumbsActivity> _breadcrumbsStock;

 

    public abstract void ChangeActivity();

    public abstract void Revers();
}