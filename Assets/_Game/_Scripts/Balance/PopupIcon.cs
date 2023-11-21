using UnityEngine;


public class PopupIcon : Parent
{
    [SerializeField] private GameObject information;
    [SerializeField] private BalanceMain balanceMain;

    public void OpenBalanceInformation()
    {
        balanceMain.SetupPopup(information);
    }

    public override void SpawnChild(GameObject child)
    {
        
        throw new System.NotImplementedException();
    }
}