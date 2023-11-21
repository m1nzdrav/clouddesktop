using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MusicStock : MonoBehaviour, ISingleton
{
   

    public string NameComponent
    {
        get => name;
    }

    [SerializeField] private AudioSource needPlay;

    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }
    [SerializeField] private List<AudioClip> stockAudioClips;

    public List<AudioClip> StockAudioClips => stockAudioClips;

    
    public AudioClip GetAudioClip(string _nameClip)
    {
        Debug.LogError("stock");
        return StockAudioClips.Find(x => x.name == _nameClip);
    }

    [Button]
    public void CheckMusic()
    {
        foreach (var VARIABLE in stockAudioClips)
        {
            Debug.LogError("+" + VARIABLE.name + "+");
        }
    }

    public void PlayMusic()
    {
        needPlay.Play();
    }

    public void AddBundleMusic(AssetBundle assetBundle)
    {
        stockAudioClips.AddRange(assetBundle.LoadAllAssets<AudioClip>());
    }
}