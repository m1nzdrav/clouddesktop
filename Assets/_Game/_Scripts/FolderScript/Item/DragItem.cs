using System;
using Doozy.Engine.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    private Vector2 pointerOffset;

    private RectTransform rectTransform;

    //    private RectTransform rectTransformSlot;
    private CanvasGroup canvasGroup;
    private GameObject oldSlot;
    [SerializeField] private Inventory inventory;
    [SerializeField] private IconModel _iconModel;

    //    private Transform draggedItemBox;

    public delegate void ItemDelegate();

    public static event ItemDelegate updateInventoryList;

    [SerializeField] private bool canDrag = false;

    public bool CanDrag
    {
        get => canDrag;
        set => canDrag = value;
    }

    void Start()
    {
        //        _iconModel = GetComponent<IconModel>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData data)
    {
        if (GetComponent<WaitPress>().IsLockDrag)
        {
            return;
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera,
                out pointerOffset);
            oldSlot = transform.parent.gameObject;
        }

        if (updateInventoryList != null)
            updateInventoryList();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<WaitPress>().IsLockDrag)
        {
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent((RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.CurrentArea.transform);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (GetComponent<WaitPress>().IsLockDrag)
        {
            return;
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
            var slot = data.pointerEnter.transform;

            if (slot.tag == "Slot")
            {
                try
                {
                    slot.GetChild(0).gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                    // throw;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        //        Debug.LogError(GetComponent<FlexibleDraggableObject>().CanDrag);
        if (GetComponent<WaitPress>().IsLockDrag)
        {
            return;
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.blocksRaycasts = true;
            Transform newSlot = null;
            if (data.pointerEnter != null)
                newSlot = data.pointerEnter.transform;

            if (newSlot != null)
            {
                //getting the items from the slots, GameObjects and RectTransform
                GameObject firstItemGameObject = this.gameObject;
                //                GameObject secondItemGameObject = newSlot.parent.gameObject;
                RectTransform firstItemRectTransform = this.gameObject.GetComponent<RectTransform>();
                //                RectTransform secondItemRectTransform = newSlot.parent.GetComponent<RectTransform>();
                //                Item firstItem = rectTransform.GetComponent<ItemOnObject>().item;
                //                Item secondItem = new Item();
                //                if (newSlot.parent.GetComponent<ItemOnObject>() != null)
                //                    secondItem = newSlot.parent.GetComponent<ItemOnObject>().item;

                //get some informations about the two items
                //                bool sameItem = firstItem.itemName == secondItem.itemName;
                //                bool sameItemRerferenced = firstItem.Equals(secondItem);
                bool secondItemStack = false;
                bool firstItemStack = false;

                //                if (sameItem)
                //                {
                //                    firstItemStack = firstItem.itemValue < firstItem.maxStack;
                //                    secondItemStack = secondItem.itemValue < secondItem.maxStack;
                //                }

                //                GameObject Inventory = secondItemRectTransform.parent.gameObject;
                //                if (Inventory.tag == "Slot")
                //                    Inventory = secondItemRectTransform.parent.parent.parent.gameObject;

                //                if (Inventory.tag.Equals("Slot"))
                //                    Inventory = Inventory.transform.parent.parent.gameObject;

                //dragging in an Inventory      
                //                if (Inventory.GetComponent<Hotbar>() == null && Inventory.GetComponent<EquipmentSystem>() == null && Inventory.GetComponent<CraftSystem>() == null)
                //                {
                //                    //you cannot attach items to the resultslot of the craftsystem
                //                    if (newSlot.transform.parent.tag == "ResultSlot" || newSlot.transform.tag == "ResultSlot" || newSlot.transform.parent.parent.tag == "ResultSlot")
                //                                         {
                //                                             
                //                                             firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                                             firstItemRectTransform.localPosition = Vector3.zero;
                //                                         }
                //                    else
                //                    {

                int newSlotChildCount = newSlot.transform.parent.childCount;
                bool isOnSlot = newSlot.transform.parent.GetChild(0).tag == "ItemIcon";

                // dragging on a slot where allready is an item on
                if (newSlotChildCount != 0 && isOnSlot)
                {
                    //                            //check if the items fits into the other item
                    //                            bool fitsIntoStack = false;
                    //                            if (sameItem)
                    //                                fitsIntoStack = (firstItem.itemValue + secondItem.itemValue) <= firstItem.maxStack;
                    //                            //if the item is stackable checking if the firstitemstack and seconditemstack is not full and check if they are the same items
                    //
                    //                            if (inventory.stackable && sameItem && firstItemStack && secondItemStack)
                    //                            {
                    //                                //if the item does not fit into the other item
                    //                                if (fitsIntoStack && !sameItemRerferenced)
                    //                                {
                    //                                    secondItem.itemValue = firstItem.itemValue + secondItem.itemValue;
                    //                                    secondItemGameObject.transform.SetParent(newSlot.parent.parent);
                    //                                    Destroy(firstItemGameObject);
                    //                                    secondItemRectTransform.localPosition = Vector3.zero;
                    //                                    if (secondItemGameObject.GetComponent<ConsumeItem>().duplication != null)
                    //                                    {
                    //                                        GameObject dup = secondItemGameObject.GetComponent<ConsumeItem>().duplication;
                    //                                        dup.GetComponent<ItemOnObject>().item.itemValue = secondItem.itemValue;
                    //                                        dup.GetComponent<SplitItem>().inv.stackableSettings();
                    //                                        dup.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
                    //                                    }
                    //                                }
                    //
                    //                                else
                    //                                {
                    //                                    //creates the rest of the item
                    //                                    int rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                    //
                    //                                    //fill up the other stack and adds the rest to the other stack 
                    //                                    if (!fitsIntoStack && rest > 0)
                    //                                    {
                    //                                        firstItem.itemValue = firstItem.maxStack;
                    //                                        secondItem.itemValue = rest;
                    //
                    //                                        firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                    //                                        secondItemGameObject.transform.SetParent(oldSlot.transform);
                    //
                    //                                        firstItemRectTransform.localPosition = Vector3.zero;
                    //                                        secondItemRectTransform.localPosition = Vector3.zero;
                    //                                    }
                    //                                }
                    //
                    //                            }
                    //                            //if does not fit
                    //                            else
                    //                            {
                    //                                //creates the rest of the item
                    //                                int rest = 0;
                    //                                if (sameItem)
                    //                                    rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                    //
                    //                                //fill up the other stack and adds the rest to the other stack 
                    //                                if (!fitsIntoStack && rest > 0)
                    //                                {
                    //                                    secondItem.itemValue = firstItem.maxStack;
                    //                                    firstItem.itemValue = rest;
                    //
                    //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                    //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                    //
                    //                                    firstItemRectTransform.localPosition = Vector3.zero;
                    //                                    secondItemRectTransform.localPosition = Vector3.zero;
                    //                                }
                    //                                //if they are different items or the stack is full, they get swapped
                    //                                else if (!fitsIntoStack && rest == 0)
                    //                                {
                    //                                    //if you are dragging an item from equipmentsystem to the inventory and try to swap it with the same itemtype
                    //                                    if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && firstItem.itemType == secondItem.itemType)
                    //                                    {
                    //                                        newSlot.transform.parent.parent.parent.parent.GetComponent<Inventory>().UnEquipItem1(firstItem);
                    //                                        oldSlot.transform.parent.parent.GetComponent<Inventory>().EquiptItem(secondItem);
                    //
                    //                                        firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                    //                                        secondItemGameObject.transform.SetParent(oldSlot.transform);
                    //                                        secondItemRectTransform.localPosition = Vector3.zero;
                    //                                        firstItemRectTransform.localPosition = Vector3.zero;
                    //
                    //                                        if (secondItemGameObject.GetComponent<ConsumeItem>().duplication != null)
                    //                                            Destroy(secondItemGameObject.GetComponent<ConsumeItem>().duplication);
                    //
                    //                                    }
                    //                                    //if you are dragging an item from the equipmentsystem to the inventory and they are not from the same itemtype they do not get swapped.                                    
                    //                                    else if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && firstItem.itemType != secondItem.itemType)
                    //                                    {
                    //                                        firstItemGameObject.transform.SetParent(oldSlot.transform);
                    //                                        firstItemRectTransform.localPosition = Vector3.zero;
                    //                                    }
                    //                                    //swapping for the rest of the inventorys
                    //                                    else if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null)
                    //                                    {
                    //                                        firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                    //                                        secondItemGameObject.transform.SetParent(oldSlot.transform);
                    //                                        secondItemRectTransform.localPosition = Vector3.zero;
                    //                                        firstItemRectTransform.localPosition = Vector3.zero;
                    //                                    }
                    //                                }
                    //
                    //                            }
                }

                //empty slot
                else
                {
                    Debug.LogError(canDrag + " " + newSlot.tag);
                    Debug.LogError(newSlot.tag != "Slot" && newSlot.tag != "ItemIcon");
                    if (newSlot.tag != "Slot" && newSlot.tag != "ItemIcon" || !canDrag)
                    {
                        Debug.LogError("В старый слот " + newSlot.gameObject.name);
                        firstItemGameObject.transform.SetParent(oldSlot.transform);
                        firstItemRectTransform.localPosition = Vector3.zero;
                        //firstItemRectTransform.FullScreen(true);
                    }
                    else
                    {
                        //                                Debug.LogError("В новый слот");                 

                        //                        firstItemGameObject.transform.SetParent(newSlot.transform);
                        firstItemRectTransform.Center(true);

                        (RegisterSingleton._instance.GetSingleton(typeof(ParentRegister)) as ParentRegister)?.SetNewParent(
                            newSlot,
                            oldSlot.transform,
                            firstItemGameObject);

                        firstItemGameObject.transform.localScale = Vector3.one;


                        //                        //todo Перенести в ParentRegister
                        //                        newSlot.GetComponent<GridCell>().MyInventory.GetComponent<ParentChild>().AddChild(null,
                        //                            firstItemGameObject.GetComponent<IconModel>().Folder.gameObject);
                        //                        oldSlot.GetComponent<GridCell>().MyInventory.GetComponent<ParentChild>()
                        //                            .RemoveChild(firstItemGameObject.GetComponent<IconModel>().Folder.gameObject, false, true);
                        //
                        //
                        //                        firstItemRectTransform.localPosition = Vector3.zero;

                        //                                if (newSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null && oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
                        //                                    oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(firstItem);
                    }

                    canDrag = false;
                }

                //                    }
                //                }

                //
                //
                //                //dragging into a Hotbar            
                //                if (Inventory.GetComponent<Hotbar>() != null)
                //                {
                //                    int newSlotChildCount = newSlot.transform.parent.childCount;
                //                    bool isOnSlot = newSlot.transform.parent.GetChild(0).tag == "ItemIcon";
                //                    //dragging on a slot where allready is an item on
                //                    if (newSlotChildCount != 0 && isOnSlot)
                //                    {
                //                        //check if the items fits into the other item
                //                        bool fitsIntoStack = false;
                //                        if (sameItem)
                //                            fitsIntoStack = (firstItem.itemValue + secondItem.itemValue) <= firstItem.maxStack;
                //                        //if the item is stackable checking if the firstitemstack and seconditemstack is not full and check if they are the same items
                //
                //                        if (inventory.stackable && sameItem && firstItemStack && secondItemStack)
                //                        {
                //                            //if the item does not fit into the other item
                //                            if (fitsIntoStack && !sameItemRerferenced)
                //                            {
                //                                secondItem.itemValue = firstItem.itemValue + secondItem.itemValue;
                //                                secondItemGameObject.transform.SetParent(newSlot.parent.parent);
                //                                Destroy(firstItemGameObject);
                //                                secondItemRectTransform.localPosition = Vector3.zero;
                //                                if (secondItemGameObject.GetComponent<ConsumeItem>().duplication != null)
                //                                {
                //                                    GameObject dup = secondItemGameObject.GetComponent<ConsumeItem>().duplication;
                //                                    dup.GetComponent<ItemOnObject>().item.itemValue = secondItem.itemValue;
                //                                    Inventory.GetComponent<Inventory>().stackableSettings();
                //                                    dup.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
                //                                }
                //                            }
                //
                //                            else
                //                            {
                //                                //creates the rest of the item
                //                                int rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                //
                //                                //fill up the other stack and adds the rest to the other stack 
                //                                if (!fitsIntoStack && rest > 0)
                //                                {
                //                                    firstItem.itemValue = firstItem.maxStack;
                //                                    secondItem.itemValue = rest;
                //
                //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                //
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //                                    secondItemRectTransform.localPosition = Vector3.zero;
                //
                //                                    createDuplication(this.gameObject);
                //                                    secondItemGameObject.GetComponent<ConsumeItem>().duplication.GetComponent<ItemOnObject>().item = secondItem;
                //                                    secondItemGameObject.GetComponent<SplitItem>().inv.stackableSettings();
                //
                //                                }
                //                            }
                //
                //                        }
                //                        //if does not fit
                //                        else
                //                        {
                //                            //creates the rest of the item
                //                            int rest = 0;
                //                            if (sameItem)
                //                                rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                //
                //                            bool fromEquip = oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null;
                //
                //                            //fill up the other stack and adds the rest to the other stack 
                //                            if (!fitsIntoStack && rest > 0)
                //                            {
                //                                secondItem.itemValue = firstItem.maxStack;
                //                                firstItem.itemValue = rest;
                //
                //                                createDuplication(this.gameObject);
                //
                //                                firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                secondItemGameObject.transform.SetParent(oldSlot.transform);
                //
                //                                firstItemRectTransform.localPosition = Vector3.zero;
                //                                secondItemRectTransform.localPosition = Vector3.zero;
                //
                //                            }
                //                            //if they are different items or the stack is full, they get swapped
                //                            else if (!fitsIntoStack && rest == 0)
                //                            {
                //                                if (!fromEquip)
                //                                {
                //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                //                                    secondItemRectTransform.localPosition = Vector3.zero;
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                                    if (oldSlot.transform.parent.parent.gameObject.Equals(GameObject.FindGameObjectWithTag("MainInventory")))
                //                                    {
                //                                        Destroy(secondItemGameObject.GetComponent<ConsumeItem>().duplication);
                //                                        createDuplication(firstItemGameObject);
                //                                    }
                //                                    else
                //                                    {
                //                                        createDuplication(firstItemGameObject);
                //                                    }
                //                                }
                //                                else
                //                                {
                //                                    firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //                                }
                //                            }
                //
                //                        }
                //                    }
                //                    //empty slot
                //                    else
                //                    {
                //                        if (newSlot.tag != "Slot" && newSlot.tag != "ItemIcon")
                //                        {
                //                            firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //                        }
                //                        else
                //                        {                            
                //                            firstItemGameObject.transform.SetParent(newSlot.transform);
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                            if (newSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null && oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
                //                                oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(firstItem);
                //                            createDuplication(firstItemGameObject);
                //                        }
                //                    }
                //
                //                }
                //
                //
                //                //dragging into a equipmentsystem/charactersystem
                //                if (Inventory.GetComponent<EquipmentSystem>() != null)
                //                {
                //                    ItemType[] itemTypeOfSlots = GameObject.FindGameObjectWithTag("EquipmentSystem").GetComponent<EquipmentSystem>().itemTypeOfSlots;
                //                    int newSlotChildCount = newSlot.transform.parent.childCount;
                //                    bool isOnSlot = newSlot.transform.parent.GetChild(0).tag == "ItemIcon";
                //                    bool sameItemType = firstItem.itemType == secondItem.itemType;
                //                    bool fromHot = oldSlot.transform.parent.parent.GetComponent<Hotbar>() != null;
                //
                //                    //dragging on a slot where allready is an item on
                //                    if (newSlotChildCount != 0 && isOnSlot)
                //                    {
                //                        //items getting swapped if they are the same itemtype
                //                        if (sameItemType && !sameItemRerferenced) //
                //                        {
                //                            Transform temp1 = secondItemGameObject.transform.parent.parent.parent;
                //                            Transform temp2 = oldSlot.transform.parent.parent;                            
                //
                //                            firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                            secondItemGameObject.transform.SetParent(oldSlot.transform);
                //                            secondItemRectTransform.localPosition = Vector3.zero;
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                            if (!temp1.Equals(temp2))
                //                            {
                //                                if (firstItem.itemType == ItemType.UFPS_Weapon)
                //                                {
                //                                    Inventory.GetComponent<Inventory>().UnEquipItem1(secondItem);
                //                                    Inventory.GetComponent<Inventory>().EquiptItem(firstItem);
                //                                }
                //                                else
                //                                {
                //                                    Inventory.GetComponent<Inventory>().EquiptItem(firstItem);
                //                                    if (secondItem.itemType != ItemType.Backpack)
                //                                        Inventory.GetComponent<Inventory>().UnEquipItem1(secondItem);
                //                                }
                //                            }
                //
                //                            if (fromHot)
                //                                createDuplication(secondItemGameObject);
                //
                //                        }
                //                        //if they are not from the same Itemtype the dragged one getting placed back
                //                        else
                //                        {
                //                            firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                            if (fromHot)
                //                                createDuplication(firstItemGameObject);
                //                        }
                //
                //                    }
                //                    //if the slot is empty
                //                    else
                //                    {
                //                        for (int i = 0; i < newSlot.parent.childCount; i++)
                //                        {
                //                            if (newSlot.Equals(newSlot.parent.GetChild(i)))
                //                            {
                //                                //checking if it is the right slot for the item
                //                                if (itemTypeOfSlots[i] == transform.GetComponent<ItemOnObject>().item.itemType)
                //                                {
                //                                    transform.SetParent(newSlot);
                //                                    rectTransform.localPosition = Vector3.zero;
                //
                //                                    if (!oldSlot.transform.parent.parent.Equals(newSlot.transform.parent.parent))
                //                                        Inventory.GetComponent<Inventory>().EquiptItem(firstItem);
                //
                //                                }
                //                                //else it get back to the old slot
                //                                else
                //                                {
                //                                    transform.SetParent(oldSlot.transform);
                //                                    rectTransform.localPosition = Vector3.zero;
                //                                    if (fromHot)
                //                                        createDuplication(firstItemGameObject);
                //                                }
                //                            }
                //                        }
                //                    }
                //
                //                }
                //
                //                if (Inventory.GetComponent<CraftSystem>() != null)
                //                {
                //                    CraftSystem cS = Inventory.GetComponent<CraftSystem>();
                //                    int newSlotChildCount = newSlot.transform.parent.childCount;
                //
                //
                //                    bool isOnSlot = newSlot.transform.parent.GetChild(0).tag == "ItemIcon";
                //                    //dragging on a slot where allready is an item on
                //                    if (newSlotChildCount != 0 && isOnSlot)
                //                    {
                //                        //check if the items fits into the other item
                //                        bool fitsIntoStack = false;
                //                        if (sameItem)
                //                            fitsIntoStack = (firstItem.itemValue + secondItem.itemValue) <= firstItem.maxStack;
                //                        //if the item is stackable checking if the firstitemstack and seconditemstack is not full and check if they are the same items
                //
                //                        if (inventory.stackable && sameItem && firstItemStack && secondItemStack)
                //                        {
                //                            //if the item does not fit into the other item
                //                            if (fitsIntoStack && !sameItemRerferenced)
                //                            {
                //                                secondItem.itemValue = firstItem.itemValue + secondItem.itemValue;
                //                                secondItemGameObject.transform.SetParent(newSlot.parent.parent);
                //                                Destroy(firstItemGameObject);
                //                                secondItemRectTransform.localPosition = Vector3.zero;
                //
                //
                //                                if (secondItemGameObject.GetComponent<ConsumeItem>().duplication != null)
                //                                {
                //                                    GameObject dup = secondItemGameObject.GetComponent<ConsumeItem>().duplication;
                //                                    dup.GetComponent<ItemOnObject>().item.itemValue = secondItem.itemValue;
                //                                    dup.GetComponent<SplitItem>().inv.stackableSettings();
                //                                    dup.transform.parent.parent.parent.GetComponent<Inventory>().updateItemList();
                //                                }
                //                                cS.ListWithItem();
                //                            }
                //
                //                            else
                //                            {
                //                                //creates the rest of the item
                //                                int rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                //
                //                                //fill up the other stack and adds the rest to the other stack 
                //                                if (!fitsIntoStack && rest > 0)
                //                                {
                //                                    firstItem.itemValue = firstItem.maxStack;
                //                                    secondItem.itemValue = rest;
                //
                //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                //
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //                                    secondItemRectTransform.localPosition = Vector3.zero;
                //                                    cS.ListWithItem();
                //
                //
                //                                }
                //                            }
                //
                //                        }
                //                        //if does not fit
                //                        else
                //                        {
                //                            //creates the rest of the item
                //                            int rest = 0;
                //                            if (sameItem)
                //                                rest = (firstItem.itemValue + secondItem.itemValue) % firstItem.maxStack;
                //
                //                            //fill up the other stack and adds the rest to the other stack 
                //                            if (!fitsIntoStack && rest > 0)
                //                            {
                //                                secondItem.itemValue = firstItem.maxStack;
                //                                firstItem.itemValue = rest;
                //
                //                                firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                secondItemGameObject.transform.SetParent(oldSlot.transform);
                //
                //                                firstItemRectTransform.localPosition = Vector3.zero;
                //                                secondItemRectTransform.localPosition = Vector3.zero;
                //                                cS.ListWithItem();
                //
                //                            }
                //                            //if they are different items or the stack is full, they get swapped
                //                            else if (!fitsIntoStack && rest == 0)
                //                            {
                //                                //if you are dragging an item from equipmentsystem to the inventory and try to swap it with the same itemtype
                //                                if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && firstItem.itemType == secondItem.itemType)
                //                                {                                  
                //
                //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                //                                    secondItemRectTransform.localPosition = Vector3.zero;
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                                    oldSlot.transform.parent.parent.GetComponent<Inventory>().EquiptItem(secondItem);
                //                                    newSlot.transform.parent.parent.parent.parent.GetComponent<Inventory>().UnEquipItem1(firstItem);
                //                                }
                //                                //if you are dragging an item from the equipmentsystem to the inventory and they are not from the same itemtype they do not get swapped.                                    
                //                                else if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null && firstItem.itemType != secondItem.itemType)
                //                                {
                //                                    firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //                                }
                //                                //swapping for the rest of the inventorys
                //                                else if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null)
                //                                {
                //                                    firstItemGameObject.transform.SetParent(secondItemGameObject.transform.parent);
                //                                    secondItemGameObject.transform.SetParent(oldSlot.transform);
                //                                    secondItemRectTransform.localPosition = Vector3.zero;
                //                                    firstItemRectTransform.localPosition = Vector3.zero;
                //                                }
                //                            }
                //
                //                        }
                //                    }
                //                    else
                //                    {
                //                        if (newSlot.tag != "Slot" && newSlot.tag != "ItemIcon")
                //                        {
                //                            firstItemGameObject.transform.SetParent(oldSlot.transform);
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //                        }
                //                        else
                //                        {                            
                //                            firstItemGameObject.transform.SetParent(newSlot.transform);
                //                            firstItemRectTransform.localPosition = Vector3.zero;
                //
                //                            if (newSlot.transform.parent.parent.GetComponent<EquipmentSystem>() == null && oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
                //                                oldSlot.transform.parent.parent.GetComponent<Inventory>().UnEquipItem1(firstItem);
                //                        }
                //                    }
                //
                //                }
            }

            //            else
            //            {
            //                GameObject dropItem = (GameObject)Instantiate(GetComponent<ItemOnObject>().item.itemModel);
            //                dropItem.AddComponent<PickUpItem>();
            //                dropItem.GetComponent<PickUpItem>().item = this.gameObject.GetComponent<ItemOnObject>().item;               
            //                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            //                inventory.OnUpdateItemList();
            //                if (oldSlot.transform.parent.parent.GetComponent<EquipmentSystem>() != null)
            //                    inventory.GetComponent<Inventory>().UnEquipItem1(dropItem.GetComponent<PickUpItem>().item);
            //                Destroy(this.gameObject);
            //
            //            }
        }

        //        if (inventory==null)
        //        {
        //            inventory=_iconModel.Folder.GetComponent<ParentChild>().Parent.GetComponent<Inventory>();
        //        }
        //        inventory.OnUpdateItemList();
    }
}