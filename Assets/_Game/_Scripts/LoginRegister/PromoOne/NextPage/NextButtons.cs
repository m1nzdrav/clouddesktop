using System;
using System.Collections.Generic;

[Serializable]
public class NextButtons
{
    public List<NextPageController> ButtonsNext;

    public NextButtons()
    {
        ButtonsNext = new List<NextPageController>();
    }
}