using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class DeleteModelPrefab : MonoBehaviour
{
    [SerializeField] private ParentChild _parentChild;
    
    [SerializeField] private ICreator iCreator;

    private void Awake()
    {
        if (_parentChild == null)
        {
            try
            {
                _parentChild = GetComponent<ParentChild>();
            }
            catch (Exception e)
            {
                Debug.LogError("Нет компонента +" + typeof(ParentChild));
            }
        }
        if (iCreator == null)
        {
            try
            {
                iCreator = GetComponent<ICreator>();
            }
            catch (Exception e)
            {
                Debug.LogError("Нет компонента +" + typeof(ICreator));
            }
        }

    }

    [Button]
    public  void DeletePrefabe(string child)
    {
       
        _parentChild.RemoveChild(child, true,true);
        
    }
}