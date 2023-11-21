using System;
using System.Collections.Generic;

[Serializable]
public class CustomCircles
{
    public List<SettingsForCircle> stock;

    public CustomCircles()
    {
        stock = new List<SettingsForCircle>();
    }
}