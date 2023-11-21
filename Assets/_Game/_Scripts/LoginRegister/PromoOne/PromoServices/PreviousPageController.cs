using UnityEngine;

public class PreviousPageController : MonoBehaviour
{
    public NextPageController Button;
    //public NextPageController PrevButton;

    public void ButtonClick()
    {
        if (!(Button is null))
        {
            // Button.ShowText(Button.PrevSubs);

            //Button.gameObject.SetActive(true);


            // Button.HideText(Button.Subs);


            Button = Button.PrevButton;
        }
    }
}