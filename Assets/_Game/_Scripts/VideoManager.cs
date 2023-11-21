using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    #region Singleton implementation
    private static VideoManager _instance;

    public static VideoManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<VideoManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (var VARIABLE in videoControllers)
        {
            VARIABLE?.player.Prepare();
        }
    }
    #endregion

    [SerializeField] private GameEvent playAll;
    [SerializeField] private GameEvent pauseAll;
    [SerializeField] private GameEvent stopAll;
    private bool isHide;
    public bool IsHide => isHide;
    /// <summary>
    /// List of all registered video players
    /// </summary>
    [field: SerializeField]
    private List<VideoController> videoControllers;
   // private bool checkACtivate = true;
    /// <summary>
    /// Event ot raise when all videos are finished
    /// </summary>
    [field: SerializeField,
            Required]
    private GameEvent allVideosFinished;

   
    private void Start()
    {
        if (NoVideosRegisteredCheck())
            return;

        //Register AllVideosFinished event with the first player
        videoControllers[0].player.loopPointReached += AllVideosFinished;
    }

    /// <summary>
    /// Play all registered videos
    /// </summary>
    [ButtonGroup("Playback")]
    public void PlayAll()
    {
        if (NoVideosRegisteredCheck())
            return;
//        playAll.Raise();
       // if(checkACtivate)
        foreach (VideoController videoController in videoControllers)
        {
            
//            if(videoController.player.isPrepared)
                videoController.Play();
               
            
        }
    }

    /// <summary>
    /// Pause all registered videos
    /// </summary>
    [ButtonGroup("Playback")]
    public void PauseAll()
    {
        if (NoVideosRegisteredCheck())
            return;
//        pauseAll.Raise();
       // if(checkACtivate)
        foreach (VideoController videoController in videoControllers)
        {
            videoController.Pause();
            
        }
    }

    /// <summary>
    /// Stop all registered videos
    /// </summary>
    [ButtonGroup("Playback")]
    public void StopAll()
    {
        
        if (NoVideosRegisteredCheck())
            return;
        stopAll.Raise();
       // if(checkACtivate)
        foreach (VideoController videoController in videoControllers)
        {              
            videoController.Stop();
        }
    }

    /// <summary>
    /// Hide windows of all registered video players
    /// </summary>
    [ButtonGroup("Activation")]
    public void HideAll()
    {
        if (NoVideosRegisteredCheck())
            return;
        isHide = true;
        Debug.LogError("isHide = "+ true);
        //checkACtivate = false;
        foreach (VideoController videoController in videoControllers)
        {
//            videoController.gameObject.GetComponent<AudioSource>().Stop();
            videoController.Hide();
        }
    }

    

    /// <summary>
    /// Hide windows of all registered video players
    /// </summary>
    [ButtonGroup("Activation")]
    public void ShowAll()
    {
        if (NoVideosRegisteredCheck())
            return;
        isHide = false;
        Debug.LogError("isHide = "+ false);
        ActivateAll();
       // checkACtivate = true;
        foreach (VideoController videoController in videoControllers)
        {
            videoController.Show();
        }
    }


    /// <summary>
    /// Deactivate windows of all registered video players
    /// </summary>
    [ButtonGroup("Activation")]
    public void DeactivateAll()
    {
        if (NoVideosRegisteredCheck())
            return;

        foreach (VideoController videoController in videoControllers)
        {
            videoController.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activate windows of all registered video players
    /// </summary>
    [ButtonGroup("Activation")]
    public void ActivateAll()
    {
        if (NoVideosRegisteredCheck())
            return;

        foreach (VideoController videoController in videoControllers)
        {
            videoController.gameObject.SetActive(true);
        }
    }

    public void ActivateAllCover()
    {
        if (NoVideosRegisteredCheck())
            return;
        foreach (VideoController videoController in videoControllers)
        {
            videoController.ActivateCover();
        }
    }

    public void ActivateAllBlackCover()
    {
        if (NoVideosRegisteredCheck())
            return;
        foreach (VideoController videoController in videoControllers)
        {
            videoController.ActivateBlackScreen();
        }
    }
    /// <summary>
    /// Method to attach to a video being finished
    /// </summary>
    /// <param name="player">VideoPlayer to attach to</param>
    private void AllVideosFinished(VideoPlayer player)
    {
        allVideosFinished.Raise();
    }

    /// <summary>
    /// Check if there are any videos registered with the manager,
    /// log error if there are none
    /// </summary>
    /// <returns>true if there are no videos registered,
    /// false if there is at least one video registered</returns>
    private bool NoVideosRegisteredCheck()
    {
        if (videoControllers.Count == 0)
        {
            Debug.LogError("There are no registered video players");
            return true;            
        }
        else
            return false;
    }

//    public void TogglePlayback()
//    {
//        if(videoControllers[0].player.isPlaying)
//        {
//            PauseAll();
//        }
//        else
//        {
//            PlayAll();
//        }
//    }

    public void SetVideosPosition(float value)
    {
        float minValue = 0f;
        float maxValue = 1f;
        if (value < minValue)
        {
            Debug.LogWarning("Value below mininmum");
            return;
        }
        if (value > maxValue)
        {
            Debug.LogWarning("Value above maximum");
            return;
        }
        long targetFrame = (long)Mathf.Lerp(0f, (float)videoControllers[0].player.frameCount, value); //TODO: make a more ellegant approach

        foreach (VideoController videoController in videoControllers)
        {
            videoController.player.frame = targetFrame;
        }
    }
}
