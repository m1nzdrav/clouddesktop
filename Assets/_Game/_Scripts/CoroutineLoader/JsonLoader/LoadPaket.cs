using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadPaket
{
    public string url;
    public UnityEvent finitLoad;
    public WWWForm dataForm;
    public string answer;
    public bool isDone;

    public LoadPaket(string url, UnityEvent finitLoad, WWWForm dataForm = null)
    {
        this.url = url;
        this.finitLoad = finitLoad;
        this.dataForm = dataForm;
    }

    public LoadPaket(string url, UnityEvent finitLoad, Dictionary<string, string> dataForm)
    {
        this.url = url;
        this.finitLoad = finitLoad;
        this.dataForm = ConvertForm(dataForm);
    }
    private WWWForm ConvertForm(Dictionary<string, string> dictionary)
    {
        WWWForm form = new WWWForm();
        foreach (var VARIABLE in dictionary)
        {
            form.AddField(VARIABLE.Key, VARIABLE.Value);
        }

        return form;
    }
}