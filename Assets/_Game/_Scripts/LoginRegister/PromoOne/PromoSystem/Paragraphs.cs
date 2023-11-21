using System;
using System.Collections.Generic;

[Serializable]
public class Paragraphs
{
    public List<Paragraph> Pages;

    public Paragraphs()
    {
        Pages = new List<Paragraph>();
    }
}