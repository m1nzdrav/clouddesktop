using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if !UNITY_WEBGL && !UNITY_STANDALONE ||UNITY_EDITOR
using System;
using System.IO;
using _Game._Scripts;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Paragraph = Xceed.Document.NET.Paragraph;
#endif

public class WordInterop : MonoBehaviour
{
    [SerializeField] private string fileName;
    public string mainJson;
    public string baseDocx;
    public string newDocx;
#if !UNITY_WEBGL && !UNITY_STANDALONE ||UNITY_EDITOR
    [Button]
    void test()
    {
        using (DocX document = DocX.Load(Application.dataPath + "/" + fileName))
        {
            Debug.LogError("test");
            // Add a new Paragraph to the document.
            Xceed.Document.NET.Paragraph p = document.InsertParagraph();
            List<Table> ll = document.Tables;
            Debug.LogError(ll[0].Rows[1].Cells[0].Paragraphs[0].Text);
            // Append some text.
            // p.Append("Hello World");

            // Save the document.
            // document.Save();
        }
    }

    [Button]
    void testTable(string JsonString)
    {
        Documents
            _subOnLoad = Config.LoadJson<Documents>(JsonString);

        using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        {
            Debug.LogError("test");
            // Add a new Paragraph to the document.

            Xceed.Document.NET.Paragraph test = document.InsertParagraph();
            Table p = document.AddTable(1, 2);

            foreach (var VARIABLE in _subOnLoad.Pages)
            {
                foreach (Content content in VARIABLE.Contents)
                {
                    CheckContent(p, content);
                }
            }

            // Append some text.
            test.InsertTableAfterSelf(p);
            // Save the document.
            document.Save();
        }
    }

    [Button]
    void InteropWithAllText(string JsonString)
    {
        AllText singleText = Config.LoadJson<AllText>(JsonString);

        using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        {
            // Add a new Paragraph to the document.

            Xceed.Document.NET.Paragraph test = document.InsertParagraph();
            Table p = document.AddTable(1, 2);

            foreach (IdNameValue variable in singleText.Key)
            {
                Row sec = p.InsertRow();
                sec.Cells[0].Paragraphs[0].Append(variable.MYTValue[0].MYTName);
            }

            // Append some text.
            test.InsertTableAfterSelf(p);
            // Save the document.
            document.Save();
        }
    }

    [Button]
    void ChangeAllTextLanguage()
    {
        string main = File.ReadAllText(Application.dataPath + "/" + mainJson + ".json");

        using (DocX baseDocument = DocX.Load(Application.dataPath + "/" + baseDocx + ".docx"))
        {
            Debug.LogError(Application.dataPath + "/" + newDocx);
            using (DocX newDocument = DocX.Load(Application.dataPath + "/" + newDocx + ".docx"))
            {
                List<Table> baseTables = baseDocument.Tables;
                // Debug.LogError(baseTables[0].Rows[1].Cells[0].Paragraphs[0].Text);

                List<Table> newTables = newDocument.Tables;
                // Debug.LogError(newTables[0].Rows[1].Cells[0].Paragraphs[0].Text);


                for (int i = newTables[0].RowCount - 1; i > 0; i--)
                {
                    if (!main.Contains(baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text) ||
                        baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text.IsNullOrWhitespace())
                    {
                        Debug.LogError(baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text + "/" +
                                       newTables[0].Rows[i].Cells[0].Paragraphs[0].Text);
                    }
                    else
                    {
                        main.Replace(baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text,
                            newTables[0].Rows[i].Cells[0].Paragraphs[0].Text);
                        int startIndex = main.IndexOf(baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text,
                            StringComparison.Ordinal);
                        main = main.Remove(startIndex, baseTables[0].Rows[i].Cells[0].Paragraphs[0].Text.Length);
                        main = main.Insert(startIndex, newTables[0].Rows[i].Cells[0].Paragraphs[0].Text);
                    }
                }
            }
        }

        File.WriteAllText(Application.dataPath + "/" + newDocx + ".json", main, Encoding.UTF8);

        // using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        // {
        //     // Add a new Paragraph to the document.
        //
        //     Xceed.Document.NET.Paragraph test = document.InsertParagraph();
        //     Table p = document.AddTable(1, 2);
        //
        //     foreach (IdNameValue variable in singleText.Key)
        //     {
        //         Row sec = p.InsertRow();
        //         sec.Cells[0].Paragraphs[0].Append(variable.MYTValue[0].MYTName);
        //     }
        //
        //     // Append some text.
        //     test.InsertTableAfterSelf(p);
        //     // Save the document.
        //     document.Save();
        // }
    }

    [Button]
    void ChangeReplaceAllTextLanguage()
    {
        string main = File.ReadAllText(Application.dataPath + "/" + mainJson + ".json");

        using (DocX baseDocument = DocX.Load(Application.dataPath + "/" + baseDocx + ".docx"))
        {
            using (DocX newDocument = DocX.Load(Application.dataPath + "/" + newDocx + ".docx"))
            {
                List<Table> baseTables = baseDocument.Tables;
                List<Table> newTables = newDocument.Tables;
                List<string> baseList = SortFirstColomn(baseTables[0].Rows);
                List<string> newList = SortFirstColomn(newTables[0].Rows);
                Debug.LogError(baseList.Count);
                Debug.LogError(newList.Count);
                RemoveSpace(ref baseList);
                RemoveSpace(ref newList);
                RemoveDots(ref baseList);
                RemoveDots(ref newList);
                RemoveSpace(ref baseList);
                RemoveSpace(ref newList);
                Bubble_Sort(ref baseList, ref newList);

                for (int i = newList.Count - 1; i >= 0; i--)
                {
                    if (!main.Contains(baseList[i]) || baseList[i].IsNullOrWhitespace())
                    {
                        Debug.LogError(baseList[i] + "/" +
                                       newList[i]);
                    }
                    else
                    {
                        main = main.Replace(baseList[i],
                            newList[i]);
                    }
                }
            }
        }

        File.WriteAllText(Application.dataPath + "/" + newDocx + ".json", main);
    }

    private static void Bubble_Sort(ref List<string> anArray, ref List<string> secondArray)
    {
        for (int i = 0; i < anArray.Count; i++)
        {
            for (int j = 0; j < anArray.Count - 1 - i; j++)
            {
                if (anArray[j].Length > anArray[j + 1].Length)
                {
                    (anArray[j], anArray[j + 1]) = (anArray[j + 1], anArray[j]);
                    (secondArray[j], secondArray[j + 1]) = (secondArray[j + 1], secondArray[j]);
                }
            }
        }
    }

    [Button]
    void ChangeReplaceAllTextLanguageInOneFile()
    {
        string main = File.ReadAllText(Application.dataPath + "/" + mainJson + ".json");

        using (DocX baseDocument = DocX.Load(Application.dataPath + "/" + baseDocx + ".docx"))
        {
            List<Table> baseTables = baseDocument.Tables;

            List<string> baseList = SortFirstColomn(baseTables[0].Rows);
            List<string> newList = SortSecondColomn(baseTables[0].Rows);

            Debug.LogError(baseList.Count);
            Debug.LogError(newList.Count);
            RemoveSpace(ref baseList);
            RemoveSpace(ref newList);
            RemoveDots(ref baseList);
            RemoveDots(ref newList);
            Bubble_Sort(ref baseList, ref newList);

            for (int i = newList.Count - 1; i > 0; i--)
            {
                if (!main.Contains(baseList[i]) || baseList[i].IsNullOrWhitespace())
                    Debug.LogError(baseList[i] + "/" +
                                   newList[i]);
                else
                    main = main.Replace(baseList[i],
                        newList[i]);
            }
        }

        File.WriteAllText(Application.dataPath + "/" + baseDocx + ".json", main);
    }

    [Button]
    void ChangeReplaceAllTextLanguageSaveSameJson()
    {
        string main = File.ReadAllText(Application.dataPath + "/" + mainJson + ".json");

        using (DocX baseDocument = DocX.Load(Application.dataPath + "/" + baseDocx + ".docx"))
        {
            using (DocX newDocument = DocX.Load(Application.dataPath + "/" + newDocx + ".docx"))
            {
                List<Table> baseTables = baseDocument.Tables;
                List<Table> newTables = newDocument.Tables;
                List<string> baseList = SortFirstColomn(baseTables[0].Rows);
                List<string> newList = SortFirstColomn(newTables[0].Rows);
                RemoveSpace(ref baseList);
                RemoveSpace(ref newList);
                RemoveDots(ref baseList);
                RemoveDots(ref newList);
                Bubble_Sort(ref baseList, ref newList);

                for (int i = newList.Count - 1; i >= 0; i--)
                    if (!main.Contains(baseList[i]) || baseList[i].IsNullOrWhitespace())
                        Debug.LogError(baseList[i] + "/" +
                                       newList[i]);
                    else
                        main = main.Replace(baseList[i],
                            newList[i]);
            }
        }

        File.WriteAllText(Application.dataPath + "/" + mainJson + ".json", main);
    }

    public List<string> SortFirstColomn(List<Row> table)
    {
        List<string> newList = new List<string>();
        Debug.LogError(table.Count);
        for (int i = 0; i < table.Count; i++)
        {
            if (!table[i].Cells[0].Paragraphs[0].Text.IsNullOrWhitespace())
                newList.Add(table[i].Cells[0].Paragraphs[0].Text);
        }

        return newList;
    }

    public List<string> SortSecondColomn(List<Row> table)
    {
        List<string> newList = new List<string>();

        for (int i = 0; i < table.Count; i++)
        {
            if (!table[i].Cells[1].Paragraphs[0].Text.IsNullOrWhitespace())
                newList.Add(table[i].Cells[1].Paragraphs[0].Text);
        }

        return newList;
    }

    public void RemoveSpace(ref List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int index = 0;
            foreach (char _char in list[i])
            {
                if (_char == ' ')
                    index++;
                else
                    break;
            }

            list[i] = list[i].Remove(0, index);
        }
    }

    public void RemoveDots(ref List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int index = 0;
            foreach (char _char in list[i])
            {
                if (_char == '.')
                    index++;
                else
                    break;
            }

            list[i] = list[i].Remove(0, index);
        }
    }

    [Button]
    void InteropWithSubtitles(string JsonString)
    {
        ListaRecords singleText = Config.LoadJson<ListaRecords>(JsonString);

        using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        {
            Debug.LogError("test");
            // Add a new Paragraph to the document.

            Xceed.Document.NET.Paragraph test = document.InsertParagraph();
            Table p = document.AddTable(1, 2);

            foreach (LanguageSub variable in singleText.Records)
            {
                Row sec = p.InsertRow();
                sec.Cells[0].Paragraphs[0].Append(variable.Subtitles);
            }

            // Append some text.
            test.InsertTableAfterSelf(p);
            // Save the document.
            document.Save();
        }
    }

    [Button]
    void InteropWithCountryCode(string JsonString)
    {
        Country singleText = Config.LoadJson<Country>(JsonString);

        using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        {
            Debug.LogError("test");
            // Add a new Paragraph to the document.

            Xceed.Document.NET.Paragraph test = document.InsertParagraph();
            Table p = document.AddTable(1, 2);

            foreach (CountryCode variable in singleText.countryCodes)
            {
                Row sec = p.InsertRow();
                sec.Cells[0].Paragraphs[0].Append(variable.name);
            }

            // Append some text.
            test.InsertTableAfterSelf(p);
            // Save the document.
            document.Save();
        }
    }

    [Button]
    void InteropWithSingleText(string JsonString)
    {
        DialogModel singleText =
            JsonUtility.FromJson<DialogModel>(File.ReadAllText(Application.dataPath + "/" + JsonString + ".json"));

        using (DocX document = DocX.Create(Application.dataPath + "/test" + JsonString + ".docx"))
        {
            Debug.LogError("test");
            // Add a new Paragraph to the document.

            Xceed.Document.NET.Paragraph test = document.InsertParagraph();
            Table p = document.AddTable(1, 2);

            foreach (SingleText variable in singleText.singleTexts)
            {
                InsertSingleText(variable, p);
            }

            // Append some text.
            test.InsertTableAfterSelf(p);
            // Save the document.
            document.Save();
        }
    }

    private static void InsertSingleText(SingleText variable, Table p)
    {
        if (!variable.question.IsNullOrWhitespace())
        {
            Row sec = p.InsertRow();
            sec.Cells[0].Paragraphs[0].Append(variable.question);
        }

        if (variable.variantAnswer != null)
            foreach (var content in variable.variantAnswer)
            {
                Row sec = p.InsertRow();
                sec.Cells[0].Paragraphs[0].Append(content);
            }

        if (variable.randomQuestion != null)
            foreach (var content in variable.randomQuestion)
            foreach (var VARIABLE in content.Strings)
            {
                Row sec = p.InsertRow();
                sec.Cells[0].Paragraphs[0].Append(VARIABLE);
            }

        if (variable.errorText != null)
        {
            Row sec = p.InsertRow();
            sec.Cells[0].Paragraphs[0].Append(variable.errorText.textError);
        }

        if (variable.childDialogModels != null)
            foreach (var content in variable.childDialogModels)
            {
                InsertSingleText(content, p);
            }
    }

    private static void CheckContent(Table p, Content content)
    {
        if (content.text.IsNullOrWhitespace()) return;

        Row sec = p.InsertRow();
        sec.Cells[0].Paragraphs[0].Append(content.text);

        if (content.commentContents != null)
            foreach (var VARIABLE in content.commentContents)
            {
                CheckContent(p, VARIABLE);
            }

        if (content.childContents != null)
            foreach (var VARIABLE in content.childContents)
            {
                CheckContent(p, VARIABLE);
            }
    }

#endif
}