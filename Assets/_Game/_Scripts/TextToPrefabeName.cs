using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextToPrefabeName : MonoBehaviour
{
  private Text _text;
  [SerializeField] private IconModel iconModel; 

  private void Start()
  {
    _text = GetComponentInChildren<Text>();
  }

  public void Converter()
  {
    Debug.LogError(_text.text);
    iconModel.ChangeName(_text.text);
  }
  
}
