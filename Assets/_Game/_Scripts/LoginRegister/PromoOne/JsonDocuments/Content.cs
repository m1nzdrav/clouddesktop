using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Content
{
    public Circles Circles;
    public string button;
    public int[] buttonPages;
    public int currentPage;
    public bool autoNextPage = false;
    public string nextContents;
    public string audio;
    public bool needClickAudio;
    public bool needGong = false;
    //не используется
    public string section;
    public string text;
    public bool earthLamp = false;
    public bool glowDot = false;
    public string typeContentIcon;

    public bool clickAfterTextPrinting = false;

    public ContentInteractivity contentInteractivity;
    public ContentSceneLocker contentSceneLocker;
    public bool point;
    public bool sword;
    public bool needInStartPromoActivity = false;
    public bool nextPromoActivity;
    public bool needInStartMenuActivity = false;
    public bool nextMenuActivity;
    public int size;
    public bool sizeIsHeight = true;
    public int height;
    public string font;
    public Color color;

    public AnimationPointJson animationPointJson;
    public AnimationTextJson animationTextJson;
    public List<Content> commentContents;
    // public Content[] commentContents;
    public List<Content> childContents;
}