using System;
using System.Collections.Generic;
using _Game._Scripts.FolderScript.GridButton;
using UnityEngine;

public class SetColoredIcon : MonoBehaviour
{
    [SerializeField] private List<ChangerSprite> _changerSprites;
    [SerializeField] private List<Sprite> whiteSprites;
    [SerializeField] private List<Sprite> coloredSprites;

    private void Awake()
    {
        for (int i = 0; i < _changerSprites.Count; i++)
        {
            _changerSprites[i].SetColored(coloredSprites[i]);
            _changerSprites[i].SetWhite(whiteSprites[i]);
            _changerSprites[i].ChangeWhite();
        }
    }
}