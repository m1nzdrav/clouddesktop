using UnityEngine;
using UnityEngine.UI;

public class VolUpDown : MonoBehaviour
{
    [SerializeField]
    private AudioSource _sound;

    [SerializeField]
    private Sprite _trueSprite;

    [SerializeField]
    private Sprite _falseSprite;

    [SerializeField]
    private GameObject _buttonUp;

    [SerializeField]
    private GameObject _buttonDown;


    public int MaxVol = 1;
    public int MinVol = 0;

    void Start()
    {
        var volume = _sound.volume;

        if (volume == MinVol)
        {
            _buttonDown.GetComponent<Image>().sprite = _falseSprite;
        }
        if (volume == MaxVol)
        {
            _buttonUp.GetComponent<Image>().sprite = _falseSprite;
        }
    }
    
    public void DownLoc()
    {
      var volume = _sound.volume;

      if (volume != MinVol)
      {
          _buttonUp.GetComponent<Image>().sprite = _trueSprite;
          volume -= 0.1f;
          _sound.volume = volume;
      }
      else
      { 
          _buttonDown.GetComponent<Image>().sprite = _falseSprite;
      }
    }

  public void UpLoc()
  {
      var volume = _sound.volume;

      if (volume != MaxVol)
      {
          _buttonDown.GetComponent<Image>().sprite = _trueSprite;
          volume += 0.1f;
          _sound.volume = volume;
      }
      else
      {
          _buttonUp.GetComponent<Image>().sprite = _falseSprite;
      }
    }
}