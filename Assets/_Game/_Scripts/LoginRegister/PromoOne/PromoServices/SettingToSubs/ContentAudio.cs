
public class ContentAudio 
{
    public void SetMusicSetting(PromoOneStrController item, Content content)
    {
        item.AudioSource.clip =
            (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.StockAudioClips.Find(x => x.name == content.audio);
        item.NeedClickAudio = content.needClickAudio;
    }
}