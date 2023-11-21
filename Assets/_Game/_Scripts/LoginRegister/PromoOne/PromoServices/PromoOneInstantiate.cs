using System.Collections.Generic;
using _Game._Scripts.PromoActivity.Actions;
using UnityEngine;

public class PromoOneInstantiate : MonoBehaviour
{
   
    
    

    public string JsonString;
    [SerializeField] private PromoStrAction _promoStrAction;

    public PromoStrAction PromoStrAction => _promoStrAction;

    [SerializeField] private DotsController _dotAnimation;

    [SerializeField] private CircleFactory _circleFactory;

 
    [Header("More")] private List<MoreController> _moreButtons = new List<MoreController>();

    [SerializeField] private NextButtons _nextPageButtons = new NextButtons();
    private BookSystem _testNextMoreSystem = new BookSystem();
    private Paragraph mainSubs = new Paragraph();
    public Paragraphs ChildSubs = new Paragraphs();



    private static PromoOneInstantiate _instance;

  

    public void StopAndClearAll()
    {
        _dotAnimation.StopAllCoroutines();
        _circleFactory?.StopAllCoroutines();

        for (int i = 0; i < _testNextMoreSystem.Book.Count; i++)
        {
            for (int j = 0; j < _testNextMoreSystem.Book[i].Pages.Count; j++)
            {
                for (int k = 0; k < _testNextMoreSystem.Book[i].Pages[j].Paragraphs.Count; k++)
                {
                    Destroy(_testNextMoreSystem.Book[i].Pages[j].Paragraphs[k]
                        .gameObject); //todo Сделать пулл!
                }
            }
        }


        for (int i = 0; i < _circleFactory.SomeCircles.Count; i++)
        {
            _circleFactory.SomeCircles[i].gameObject.SetActive(false);
        }


        _circleFactory.Circles.Clear();
        _testNextMoreSystem.Book.Clear();
        _moreButtons.Clear();
        _nextPageButtons.ButtonsNext.Clear();
        ChildSubs.Pages.Clear();
        mainSubs.Paragraphs.Clear();

    }

  

    public Paragraph GetParagraph(int number)
    {
        return _testNextMoreSystem.Book[0].Pages[number];
    }

}