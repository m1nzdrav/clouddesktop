using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CustomThread : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    object sync = new object();

    List<Action> actions = new List<Action>();


    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }

    // Update is called once per frame

    void Update()
    {
        lock (sync)
        {
            //обеспечиваем потокобезопасность чтения листа
            while (actions.Count != 0)
            {
                //и исполняем все действия

//                Debug.LogError( Thread.CurrentThread.ManagedThreadId);
                actions[0].Invoke();
                actions.RemoveAt(0);
            }
        }
    }

    public void Execute(Action action)
    {
        lock (sync)
        {
            //обеспечиваем потокобезопасность записи в лист

//            Debug.LogError( Thread.CurrentThread.ManagedThreadId);
            actions.Add(action);
        }

        try
        {
            Thread.Sleep(1000); //усыпляем вызвавший поток
        }
        catch (ThreadInterruptedException)
        {
        }
        finally
        {
        }
    }
}