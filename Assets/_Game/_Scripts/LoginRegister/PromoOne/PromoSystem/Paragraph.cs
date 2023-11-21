using System;
using System.Collections.Generic;

[Serializable]
public class Paragraph
{
    public List<PromoOneStrController> Paragraphs;

    public Paragraph()
    {
        // Paragraphs = new List<PromoOneStrController>();
    }

    public Paragraph(int count)
    {
        Paragraphs = new List<PromoOneStrController>(count);
    }
}