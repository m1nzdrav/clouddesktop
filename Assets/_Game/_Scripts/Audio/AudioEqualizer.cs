using System.Collections.Generic;
using System.Runtime.InteropServices;
using CrazyMinnow.AmplitudeWebGL;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;


public class AudioEqualizer : MonoBehaviour, ISingleton
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    //private static extern bool WebGL_StartSampling(string uniqueName, float duration, int sampleSize);
    private static extern bool WebGL_StartSampling(string uniqueName, float duration, int sampleSize, string dataType);

    [DllImport("__Internal")]
    private static extern bool WebGL_StopSampling(string uniqueName);

    [DllImport("__Internal")]
    //private static extern bool WebGL_GetAmplitude(string uniqueName, float[] sample, int sampleSize);
    private static extern bool WebGL_GetAmplitude(string uniqueName, float[] sample, int sampleSize);

    [DllImport("__Internal")]
    //private static extern bool WebGL_GetFrequency(string uniqueName, float[] sample, int sampleSize);
    private static extern bool WebGL_GetFrequency(string uniqueName, float[] sample);
#endif
    [SerializeField] private float waitTimer = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<Transform> needChange;
    [SerializeField] private float[] testFrequency = new float[256];
    [SerializeField] private List<float> sizeStock;
    [SerializeField] private float frequencyMultiplayer = 200f;
    

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    } 
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }
    
    //&& !UNITY_EDITOR
#if UNITY_WEBGL &&!UNITY_EDITOR
    private void LateUpdate()
    {
        if (audioSource == null || !audioSource.isPlaying)
        {
            if (videoPlayer == null || !videoPlayer.isPlaying)
            {
                return;
            }

            if (!WebGL_GetFrequency(videoPlayer.GetTargetAudioSource(0).GetInstanceID().ToString(), testFrequency))
            {
                WebGL_StartSampling(videoPlayer.GetTargetAudioSource(0).GetInstanceID().ToString(),
                    videoPlayer.GetTargetAudioSource(0).clip.length,
                    128,
                    Amplitude.DataType.Frequency.ToString());
            }

            ;


            // audioSource.GetSpectrumData(testFrequency, 0, FFTWindow.Blackman);
            Debug.LogError(testFrequency[0]);
            for (int i = 0; i < needChange.Count; i++)
            {
                needChange[i].DOScale(sizeStock[i] + Mathf.Abs(testFrequency[0]), waitTimer);
            }
        }
        else
        {
            // Debug.LogError(  GetLipSamples(audioSource.name, testFrequency, testFrequency.Length))
            //     ;
            if (!WebGL_GetFrequency(audioSource.GetInstanceID().ToString(), testFrequency))
            {
                WebGL_StartSampling(audioSource.GetInstanceID().ToString(), audioSource.clip.length, 128,
                    Amplitude.DataType.Frequency.ToString());
            }

            ;


            // audioSource.GetSpectrumData(testFrequency, 0, FFTWindow.Blackman);
            
            
            if (!(Mathf.Abs(testFrequency[0])  < 70)) return;
            
            Debug.LogError("normal "+Mathf.Abs(testFrequency[0]) /1.7f);
            
            for (int i = 0; i < needChange.Count; i++)
            {
                needChange[i].DOScale(sizeStock[i] + Mathf.Abs(testFrequency[0])/1.7f, waitTimer);
            }
        }


        // obj.ForEach(x => x.DOScale(1 + testFrequency.Max() * 10, waitTimer));
    }
#else
    private void LateUpdate()
    {
        if (audioSource == null || !audioSource.isPlaying)

        {
            if (videoPlayer == null || !videoPlayer.isPlaying)
                return;
            
            audioSource.GetSpectrumData(testFrequency, 0, FFTWindow.Blackman);

            for (int i = 0; i < needChange.Count; i++)
                needChange[i].DOScale(sizeStock[i] + testFrequency[0] * frequencyMultiplayer, waitTimer);
            
            return;
        }
        //
        // StartLipSampling(audioSource.name, audioSource.clip.length, 256);
        // GetLipSamples(audioSource.name, testFrequency, testFrequency.Length);

        audioSource.GetSpectrumData(testFrequency, 0, FFTWindow.Blackman);

        for (int i = 0; i < needChange.Count; i++)
        {
            needChange[i].DOScale(sizeStock[i] + testFrequency[0] * frequencyMultiplayer, waitTimer);
        }


        // obj.ForEach(x => x.DOScale(1 + testFrequency.Max() * 10, waitTimer));
    }
#endif

    public void SetAudio(List<Transform> transform, List<float> startSize, AudioSource audioSource,
        float frequencyMultiplayer)
    {
        ReturnScale();
        this.frequencyMultiplayer = frequencyMultiplayer;
        needChange = transform;
        this.audioSource = audioSource;
      
        sizeStock = startSize;
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGL_StartSampling(this.audioSource.GetInstanceID().ToString(), this.audioSource.clip.length, 128, Amplitude.DataType.Frequency.ToString());
#endif

        // StartLipSampling(audioSource.name, audioSource.clip.length, 256);
        // Update();
    }
#if UNITY_WEBGL && !UNITY_EDITOR
    public void SetVideo(List<Transform> transform, List<float> startSize, VideoPlayer videoSource,
        float frequencyMultiplayer)
    {
        this.frequencyMultiplayer = frequencyMultiplayer;
        needChange = transform;
        videoPlayer = videoSource;
        sizeStock = startSize;
        // videoPlayer.GetTargetAudioSource(0).GetSpectrumData(testFrequency, 0, FFTWindow.Blackman);
        WebGL_StartSampling(this.videoPlayer.GetTargetAudioSource(0).GetInstanceID().ToString(),(float) this.videoPlayer.GetTargetAudioSource(0).clip.length, 128, Amplitude.DataType.Frequency.ToString());


        // StartLipSampling(audioSource.name, audioSource.clip.length, 256);
        // Update();
    }
#endif
    public void ResetAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       // WebGL_StopSampling(audioSource.GetInstanceID().ToString());
#endif

        Debug.LogError("end Test");
        audioSource = null;
        for (int i = 0; i < needChange.Count; i++)
        {
            needChange[i].DOScale(sizeStock[i], waitTimer);
        }
    }

    public void ResetVideo()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       WebGL_StopSampling(videoPlayer.GetInstanceID().ToString());
#endif

        audioSource = null;
        ReturnScale();
        
    }

    private void ReturnScale()
    {
        for (int i = 0; i < needChange.Count; i++)
        {
            needChange[i].DOScale(sizeStock[i], waitTimer);
        }
    }
}