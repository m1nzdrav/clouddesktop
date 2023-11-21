using System;
using System.Collections.Generic;

[Serializable]
public class BookSystem
{
    public List<Paragraphs> Book;

    public BookSystem()
    {
        Book = new List<Paragraphs>();
        
    }
}