using UnityEngine;

public class GridCellGetGridController : MonoBehaviour
{
   [SerializeField] private Inventory _inventory;
   public GridControllerInFolderDesktop gridControllerInFolderDesktop;

   public Inventory Inventory
   {
      get => _inventory;
      set => _inventory = value;
   }
}
