using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FontSingleton : MonoBehaviour, ISingleton
{
    [SerializeField] private List<Font> stockFonts;

    public string NameComponent
    {
        get => name;
    }

    public void CheckRegister()
    {
        stockFonts = new List<Font>();
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

    public void AddFonts(AssetBundle assetBundle)
    {
        stockFonts = assetBundle.LoadAllAssets<Font>().ToList();
    }

    public Font GetFont(string font)
    {
        return stockFonts.Find(x => x.name == font);
    }
}