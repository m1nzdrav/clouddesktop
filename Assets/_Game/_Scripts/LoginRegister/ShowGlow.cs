using UnityEngine;

public class ShowGlow : MonoBehaviour
{
    public void ShowAnimOpen()
    {
        GetComponent<Animation>().Play("GlowOpen");
    }

    public void ShowAnimClose()
    {
        GetComponent<Animation>().Play("GlowClose");
    }
}
