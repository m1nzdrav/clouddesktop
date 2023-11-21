using System;
using System.Collections.Generic;

[Serializable]
public class Page
{
    public int Number;
    public AnimationPointJson animationPointJson;
    public List<Content> Contents;
}