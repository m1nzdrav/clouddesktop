using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefabSlot;

    [SerializeField] private RectTransform PanelRectTransform;

    [SerializeField] private GameObject SlotContainer;

    [SerializeField] private RectTransform SlotContainerRectTransform;
    [SerializeField] private GridLayoutGroup SlotGridLayout;
    [SerializeField] private RectTransform SlotGridRectTransform;

    [SerializeField] public int height;
    [SerializeField] public int width;
    [SerializeField] public bool stackable;

    private Color _commonColor = new Color(0f, 0.627f, 1f, 0.4549f);

    //GUI Settings
    [SerializeField] public int slotSize;
    [SerializeField] public int iconSize;
    [SerializeField] public int paddingBetweenX;
    [SerializeField] public int paddingBetweenY;
    [SerializeField] public int paddingLeft;
    [SerializeField] public int paddingRight;
    [SerializeField] public int paddingBottom;
    [SerializeField] public int paddingTop;
    [SerializeField] public int positionNumberX;
    [SerializeField] public int positionNumberY;


    public delegate void InventoryOpened();

    public static event InventoryOpened InventoryOpen;
    public static event InventoryOpened AllInventoriesClosed;

    void Start()
    {
        updateItemList();
        updateSlotSize();
    }

    void Update()
    {
        updateItemIndex();
    }


    public void OnUpdateItemList()
    {
        updateItemList();
    }


    //todo тут присваивается грид layaout надо ручками проставить связь
    public void setImportantVariables()
    {
        PanelRectTransform = GetComponent<RectTransform>();
        //        SlotContainer = transform.GetChild(1).gameObject;
        SlotGridLayout = SlotContainer.GetComponent<GridLayoutGroup>();
        SlotGridRectTransform = SlotContainer.GetComponent<RectTransform>();
    }

    public void getPrefabs()
    {
        setImportantVariables();
        setDefaultSettings();
        adjustInventorySize();
        updateSlotAmount(width, height);
        updateSlotSize();
        updatePadding(paddingBetweenX, paddingBetweenY);
    }

    public void updateItemList()
    {
        for (int i = 0; i < SlotContainer.transform.childCount; i++)
        {
            Transform trans = SlotContainer.transform.GetChild(i);
            if (trans.childCount != 0)
            {
            }
        }
    }


    public void setDefaultSettings()
    {
        height = 5;
        width = 5;

        slotSize = 50;
        iconSize = 45;

        paddingBetweenX = 5;
        paddingBetweenY = 5;
        paddingTop = 35;
        paddingBottom = 10;
        paddingLeft = 10;
        paddingRight = 10;
    }

    public void adjustInventorySize()
    {
        int x = (((width * slotSize) + ((width - 1) * paddingBetweenX)) + paddingLeft + paddingRight);
        int y = (((height * slotSize) + ((height - 1) * paddingBetweenY)) + paddingTop + paddingBottom);

        SlotGridRectTransform.sizeDelta = new Vector2(x, y);
    }

    public void updateSlotAmount()
    {
        updateSlotAmount(width, height);
    }

    public void updateSlotAmount(int width, int height)
    {
        if (SlotContainerRectTransform == null)
            SlotContainerRectTransform = SlotContainer.GetComponent<RectTransform>();

        SlotContainerRectTransform.localPosition = Vector3.zero;

        //        List<Item> itemsToMove = new List<Item>();
        List<GameObject> slotList = new List<GameObject>();
        foreach (Transform child in SlotContainer.transform)
        {
            if (child.tag == "Slot")
            {
                slotList.Add(child.gameObject);
            }
        }

        while (slotList.Count > width * height)
        {
            GameObject go = slotList[slotList.Count - 1];
            //            ItemOnObject itemInSlot = go.GetComponentInChildren<ItemOnObject>();
            //            if (itemInSlot != null)
            //            {
            //                itemsToMove.Add(itemInSlot.item);
            ////                ItemsInInventory.Remove(itemInSlot.item);
            //            }

            slotList.Remove(go);
            DestroyImmediate(go);
        }

        if (slotList.Count < width * height)
        {
            Debug.LogError(slotList.Count);
            for (int i = slotList.Count; i < (width * height); i++)
            {
                GameObject Slot = (GameObject) Instantiate(prefabSlot);
                Slot.name = (slotList.Count + 1).ToString();
                Slot.transform.SetParent(SlotContainer.transform, false);
                //                Slot.GetComponent<GridCell>().MyInventory = this;
                slotList.Add(Slot);
            }
        }

        //        if (itemsToMove != null && ItemsInInventory.Count < width * height)
        //        {
        //            foreach (Item i in itemsToMove)
        //            {
        ////                addItemToInventory(i.itemID);
        //            }
        //        }

        setImportantVariables();
    }

    public void updateSlotSize()
    {
        updateSlotSize(slotSize);
    }

    public void updateSlotSize(int slotSize)
    {
        SlotGridLayout.cellSize = new Vector2(slotSize, slotSize);
        updateItemSize();
    }

    [Button]
    void updateItemSize()
    {
        for (int i = 0; i < SlotContainer.transform.childCount; i++)
        {
            if (SlotContainer.transform.GetChild(i).childCount > 1)
            {
                SlotContainer.transform.GetChild(i).GetChild(1).GetComponent<RectTransform>().sizeDelta =
                    new Vector2(slotSize, slotSize);
                //                SlotContainer.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta =
                //                    new Vector2(slotSize, slotSize);
            }
        }
    }

    public void updatePadding(int spacingBetweenX, int spacingBetweenY)
    {
        SlotGridLayout.spacing = new Vector2(paddingBetweenX, paddingBetweenY);
        SlotGridLayout.padding.bottom = paddingBottom;
        SlotGridLayout.padding.right = paddingRight;
        SlotGridLayout.padding.left = paddingLeft;
        SlotGridLayout.padding.top = paddingTop;
    }

    public void updatePadding()
    {
        SlotGridLayout.spacing = new Vector2(paddingBetweenX, paddingBetweenY);
        SlotGridLayout.padding.bottom = paddingBottom;
        SlotGridLayout.padding.right = paddingRight;
        SlotGridLayout.padding.left = paddingLeft;
        SlotGridLayout.padding.top = paddingTop;
    }


    //todo добавление предметов  
    public void addAllItemsToInventory()
    {
        //        for (int k = 0; k < ItemsInInventory.Count; k++)
        //        {
        //            for (int i = 0; i < SlotContainer.transform.childCount; i++)
        //            {
        //                if (SlotContainer.transform.GetChild(i).childCount == 0)
        //                {
        ////                    GameObject item = (GameObject)Instantiate(prefabItem);
        ////                    item.GetComponent<ItemOnObject>().item = ItemsInInventory[k];
        ////                    item.transform.SetParent(SlotContainer.transform.GetChild(i));
        ////                    item.GetComponent<RectTransform>().localPosition = Vector3.zero;
        ////                    item.transform.GetChild(0).GetComponent<Image>().sprite = ItemsInInventory[k].itemIcon;
        ////                    updateItemSize();
        //                    break;
        //                }
        //            }
        //        }

        //        stackableSettings();
    }


    public bool checkIfItemAllreadyExist(int itemID, int itemValue)
    {
        updateItemList();

        return false;
    }

    //todo добавление предметов
    //    public void addItemToInventory(int id)
    //         {
    //             for (int i = 0; i < SlotContainer.transform.childCount; i++)
    //             {
    //                 if (SlotContainer.transform.GetChild(i).childCount == 0)
    //                 {
    //                     GameObject item = (GameObject)Instantiate(prefabItem);
    //                     item.GetComponent<ItemOnObject>().item = itemDatabase.getItemByID(id);
    //                     item.transform.SetParent(SlotContainer.transform.GetChild(i));
    //                     item.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //                     item.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<ItemOnObject>().item.itemIcon;
    //                     item.GetComponent<ItemOnObject>().item.indexItemInList = ItemsInInventory.Count - 1;
    //                     break;
    //                 }
    //             }
    //     
    //             stackableSettings();
    //             updateItemList();
    //     
    //         }
    //todo добавляет предмет и возвращает его
    //    public GameObject addItemToInventory(int id, int value)
    //    {
    //        for (int i = 0; i < SlotContainer.transform.childCount; i++)
    //        {
    //            if (SlotContainer.transform.GetChild(i).childCount == 0)
    //            {
    //                GameObject item = (GameObject)Instantiate(prefabItem);
    //                ItemOnObject itemOnObject = item.GetComponent<ItemOnObject>();
    //                itemOnObject.item = itemDatabase.getItemByID(id);
    //                if (itemOnObject.item.itemValue <= itemOnObject.item.maxStack && value <= itemOnObject.item.maxStack)
    //                    itemOnObject.item.itemValue = value;
    //                else
    //                    itemOnObject.item.itemValue = 1;
    //                item.transform.SetParent(SlotContainer.transform.GetChild(i));
    //                item.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //                item.transform.GetChild(0).GetComponent<Image>().sprite = itemOnObject.item.itemIcon;
    //                itemOnObject.item.indexItemInList = ItemsInInventory.Count - 1;
    //                if (inputManagerDatabase == null)
    //                    inputManagerDatabase = (InputManager)Resources.Load("InputManager");
    //                return item;
    //            }
    //        }
    //
    //        stackableSettings();
    //        updateItemList();
    //        return null;
    //
    //    }

    //    public void addItemToInventoryStorage(int itemID, int value)
    //    {
    //
    //        for (int i = 0; i < SlotContainer.transform.childCount; i++)
    //        {
    //            if (SlotContainer.transform.GetChild(i).childCount == 0)
    //            {
    //                GameObject item = (GameObject)Instantiate(prefabItem);
    //                ItemOnObject itemOnObject = item.GetComponent<ItemOnObject>();
    //                itemOnObject.item = itemDatabase.getItemByID(itemID);
    //                if (itemOnObject.item.itemValue < itemOnObject.item.maxStack && value <= itemOnObject.item.maxStack)
    //                    itemOnObject.item.itemValue = value;
    //                else
    //                    itemOnObject.item.itemValue = 1;
    //                item.transform.SetParent(SlotContainer.transform.GetChild(i));
    //                item.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //                itemOnObject.item.indexItemInList = 999;
    //                if (inputManagerDatabase == null)
    //                    inputManagerDatabase = (InputManager)Resources.Load("InputManager");
    //                updateItemSize();
    //                stackableSettings();
    //                break;
    //            }
    //        }
    //        stackableSettings();
    //        updateItemList();
    //    }

    public void updateIconSize()
    {
        updateIconSize(iconSize);
    }

    public void updateIconSize(int iconSize)
    {
        for (int i = 0; i < SlotContainer.transform.childCount; i++)
        {
            if (SlotContainer.transform.GetChild(i).childCount > 0)
            {
                SlotContainer.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta =
                    new Vector2(iconSize, iconSize);
            }
        }

        updateItemSize();
    }



    public void updateItemIndex()
    {

    }

    public void AddIcon(GameObject icon, bool isGrid)
    {
        var childCount = SlotContainer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var currentSlot = SlotContainer.transform.GetChild(i);

            if (currentSlot.childCount <= 1)
            {
                var gridController = GetComponent<GridControllerInFolderDesktop>();
                var iisGrid = gridController.IsGrid;


                if (iisGrid)
                {
                    gridController.SetTextForList(icon.transform);
                }
                else
                {
                    gridController.SetTextForGrid(icon.transform);
                }

                //Debug.LogError("Присвоил иконке "+icon.gameObject.name+ " родителя " + currentSlot.gameObject.name);
                icon.transform.SetParent(currentSlot, false);
                icon.transform.localPosition = Vector3.zero;

                //var glow = currentSlot.GetChild(0);
                //var color = icon.GetComponent<Image>().color;
                //glow.GetComponent<Image>().color = color;

                icon.GetComponent<RectTransform>().sizeDelta = currentSlot.GetComponent<RectTransform>().sizeDelta;
                return;
            }
        }

        var newCurrentSlot = Instantiate(prefabSlot.transform, SlotContainer.transform);
        icon.transform.SetParent(newCurrentSlot, false);
        icon.transform.localPosition = Vector3.zero;
        var cells = GetComponent<GridControllerInFolderDesktop>().Cells;
        cells.Add(newCurrentSlot.gameObject);
        newCurrentSlot.name = cells.Count.ToString();

        //var newGlow = newCurrentSlot.GetChild(0);
        //var newColor = icon.GetComponent<Image>().color;
        //newGlow.GetComponent<Image>().color = newColor;

        //newCurrentSlot.SetAsFirstSibling();
    }
}