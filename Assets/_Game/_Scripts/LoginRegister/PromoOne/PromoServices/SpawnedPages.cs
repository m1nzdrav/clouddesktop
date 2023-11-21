using System.Collections.Generic;
using UnityEngine;

public class SpawnedPages
{
    private readonly List<int[]> spawnedPages;
    private int check;

    public SpawnedPages()
    {
        spawnedPages = new List<int[]>();
    }

    public bool AddPages(int[] newPages)
    {
        if (CheckPages(newPages))
        {
            return false;
        }

        spawnedPages.Add(newPages);
        return true;
    }

    // проверка существующих страниц
    private bool CheckPages(int[] newPages)
    {
        foreach (int[] spawn in spawnedPages)
        {
            if (spawn.Length != newPages.Length) continue;


            check = 1;
            for (int j = 0; j < spawn.Length; j++)
            {
                if (spawn[j] == newPages[j]) continue;

                check *= 0;
                break;
            }


            if (check != 0)
                return true;
        }

        return false;
    }
}