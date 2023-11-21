using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
// using UnityEditor.Events;

#endif

namespace Michsky.UI.ModernUIPack
{
    public class CustomDropdown : MonoBehaviour, IPointerExitHandler
    {
        [Header("OBJECTS")] public GameObject triggerObject;
        public Text selectedText;
        public Text numberCode;
        public Image selectedImage;
        public bool selectedIsArabic;
        public Transform itemParent;
        public GameObject itemObject;
        public GameObject scrollbar;
        private VerticalLayoutGroup itemList;
        private Transform currentListParent;
        public Transform listParent;

        [Header("SETTINGS")] public bool enableIcon = true;
        public bool enableTrigger = true;
        public bool enableScrollbar = true;
        public bool setHighPriorty = true;
        public bool outOnPointerExit = false;
        public bool isListItem = false;
        public bool invokeAtStart = false;
        public bool isCode;
        public AnimationType animationType;
        public int selectedItemIndex = 0;

        [Header("SAVING")] public bool saveSelected = false;

        [Tooltip("Note that every Dropdown should has its own unique tag.")]
        public string dropdownTag = "Dropdown";

        [SerializeField] [Header("CONTENT")] public List<Item> dropdownItems = new List<Item>();

        [SerializeField] private Animator dropdownAnimator;
        private Text setItemText;
        private Image setItemImage;

        Sprite imageHelper;
        string textHelper;
        string newItemTitle;
        string code;
        Sprite newItemIcon;
        bool isOn;
        [HideInInspector] public int index = 0;
        [HideInInspector] public int siblingIndex = 0;

        private Item _currentItem;

        [Serializable]
        public enum AnimationType
        {
            FADING = 0,
            SLIDING = 1,
            STYLISH_600 = 2,
            STYLISH_300 = 3
        }

        [System.Serializable]
        public class Item
        {
            public int number;
            public string itemName = "Dropdown Item";
            public string code;
            public bool isArabic;
            public Sprite itemIcon;
            public UnityEvent OnItemSelection;
            
        }

        [SerializeField] private UnityEvent eventChoose;

#if UNITY_EDITOR

        [Button]
        void Set()
        {
            dropdownAnimator = gameObject.GetComponent<Animator>();
            itemList = itemParent.GetComponent<VerticalLayoutGroup>();
            SetupDropdown();
            currentListParent = transform.parent;

            if (enableIcon == false)
                selectedImage.gameObject.SetActive(false);
            else
                selectedImage.gameObject.SetActive(true);

            if (enableScrollbar == true)
            {
                itemList.padding.right = 25;
                scrollbar.SetActive(true);
            }

            else
            {
                itemList.padding.right = 8;
                scrollbar.SetActive(false);
            }

            if (setHighPriorty == true)
                transform.SetAsLastSibling();

            if (saveSelected == true)
            {
                if (invokeAtStart == true)
                    dropdownItems[PlayerPrefs.GetInt(dropdownTag + "Dropdown")].OnItemSelection.Invoke();
                else
                    ChangeDropdownInfo(PlayerPrefs.GetInt(dropdownTag + "Dropdown"));
            }
        }


        public bool GetIsOn()
        {
            return isOn;
        }

        public void UpdateValues()
        {
            if (enableScrollbar == true)
            {
                itemList.padding.right = 25;
                scrollbar.SetActive(true);
            }

            else
            {
                itemList.padding.right = 8;
                scrollbar.SetActive(false);
            }

            if (enableIcon == false)
                selectedImage.gameObject.SetActive(false);
            else
                selectedImage.gameObject.SetActive(true);
        }


        public void CreateNewItem()
        {
            Item item = new Item();
            item.itemName = newItemTitle;
            item.itemIcon = newItemIcon;

            if (isCode)
            {
                item.code = code;
            }

            dropdownItems.Add(item);


            SetupDropdown();
        }


        public void SetItemTitle(string title)
        {
            newItemTitle = title;
        }

        public void SetItemIcon(Sprite icon)
        {
            newItemIcon = icon;
        }

        public void SetCode(string countryCode)
        {
            code = countryCode;
        }
#endif


        public void SetupDropdown()
        {
            foreach (Transform child in itemParent)
                GameObject.Destroy(child.gameObject);

            index = 0;
            for (int i = 0; i < dropdownItems.Count; ++i)
            {
                GameObject go = Instantiate(itemObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(itemParent, false);

                setItemText = go.GetComponentInChildren<Text>();
                textHelper = dropdownItems[i].itemName;
                setItemText.text = textHelper;

                if (isCode)
                {
                    go.GetComponent<CodeCountrySettings>().NumberText.text = dropdownItems[i].code;
                }

                try
                {
                    Transform goImage;

                    goImage = go.gameObject.transform.Find("Icon");

                    if (goImage == null) continue;

                    setItemImage = goImage.GetComponent<Image>();
                    imageHelper = dropdownItems[i].itemIcon;
                    setItemImage.sprite = imageHelper;

                    if (setItemImage.sprite == null)
                    {
                        setItemImage.gameObject.SetActive(false);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }


                Button itemButton;
                itemButton = go.GetComponent<Button>();

                itemButton.onClick.AddListener(Animate);
                itemButton.onClick.AddListener(delegate
                {
                    ChangeDropdownInfo(index = go.transform.GetSiblingIndex());

                    // if (saveSelected == true)
                    //     PlayerPrefs.SetInt(dropdownTag + "Dropdown", go.transform.GetSiblingIndex());
                });

                //todo UnityEventTools.AddPersistentListener(itemButton.onClick, Animate);
                //
                // UnityAction<GameObject> action = new UnityAction<GameObject>(ActionForItem);

                //todo UnityEventTools.AddObjectPersistentListener<GameObject>(itemButton.onClick, action, go);

                //UnityEventTools.AddPersistentListener(itemButton.onClick, delegate
                //{
                //    ChangeDropdownInfo(index = go.transform.GetSiblingIndex());

                //    if (saveSelected == true)
                //        PlayerPrefs.SetInt(dropdownTag + "Dropdown", go.transform.GetSiblingIndex());
                //});

                if (dropdownItems[i].OnItemSelection != null)
                {
                    var invokeAction = new UnityAction<int>(ActionItemInvoke);

                    //todo  UnityEventTools.AddIntPersistentListener(itemButton.onClick, invokeAction, i);
                }


                if (invokeAtStart == true)
                    dropdownItems[i].OnItemSelection.Invoke();
            }


            //selectedText.text = dropdownItems[selectedItemIndex].itemName;
            //selectedImage.sprite = dropdownItems[selectedItemIndex].itemIcon;
            //numberCode.text = dropdownItems[selectedItemIndex].code;
            //currentListParent = transform.parent;
        }

        public void CreateNewItem(string title = null, Sprite icon = null, Action callback = null)
        {
            Item item = new Item() { itemName = title, itemIcon = icon };

            // item.OnItemSelection.AddListener(callback.Invoke);

            if (isCode)
            {
                item.code = code;
            }

            dropdownItems.Add(item);


            // SetupDropdown();
        }

        public void ChangeDropdownInfo(int itemIndex)
        {
            selectedImage.sprite = dropdownItems[itemIndex].itemIcon;
            selectedText.text = dropdownItems[itemIndex].itemName;
            selectedIsArabic = dropdownItems[itemIndex].isArabic;

            if (isCode) numberCode.text = dropdownItems[itemIndex].code;

            selectedItemIndex = itemIndex;
            eventChoose?.Invoke();
            // dropdownItems[itemIndex].OnItemSelection.Invoke();
        }

        public void Animate()
        {
            if (isOn == false && animationType == AnimationType.FADING)
            {
                dropdownAnimator.Play("Fading In");
                isOn = true;
                if (isListItem == true)
                {
                    siblingIndex = transform.GetSiblingIndex();
                    gameObject.transform.SetParent(listParent, true);
                }
            }

            else if (isOn == true && animationType == AnimationType.FADING)
            {
                dropdownAnimator.Play("Fading Out");
                isOn = false;

                if (isListItem == true)
                {
                    gameObject.transform.SetParent(currentListParent, true);
                    gameObject.transform.SetSiblingIndex(siblingIndex);
                }
            }

            else if (isOn == false && animationType == AnimationType.SLIDING)
            {
                dropdownAnimator.Play("Sliding In");
                isOn = true;


                if (isListItem == true)
                {
                    siblingIndex = transform.GetSiblingIndex();
                    gameObject.transform.SetParent(listParent, true);
                }
            }

            else if (isOn == true && animationType == AnimationType.SLIDING)
            {
                dropdownAnimator.Play("Sliding Out");
                isOn = false;


                if (isListItem == true)
                {
                    gameObject.transform.SetParent(currentListParent, true);
                    gameObject.transform.SetSiblingIndex(siblingIndex);
                }
            }

            else if (isOn == false && animationType == AnimationType.STYLISH_600)
            {
                dropdownAnimator.Play("Stylish In 600");
                isOn = true;

                if (isListItem == true)
                {
                    siblingIndex = transform.GetSiblingIndex();
                    gameObject.transform.SetParent(listParent, true);
                }
            }

            else if (isOn == true && animationType == AnimationType.STYLISH_600)
            {
                dropdownAnimator.Play("Stylish Out 600");
                isOn = false;

                if (isListItem == true)
                {
                    gameObject.transform.SetParent(currentListParent, true);
                    gameObject.transform.SetSiblingIndex(siblingIndex);
                }
            }
            else if (isOn == false && animationType == AnimationType.STYLISH_300)
            {
                dropdownAnimator.Play("Stylish In 300");
                isOn = true;

                if (isListItem == true)
                {
                    siblingIndex = transform.GetSiblingIndex();
                    gameObject.transform.SetParent(listParent, true);
                }
            }

            else if (isOn == true && animationType == AnimationType.STYLISH_300)
            {
                dropdownAnimator.Play("Stylish Out 300");
                isOn = false;

                if (isListItem == true)
                {
                    gameObject.transform.SetParent(currentListParent, true);
                    gameObject.transform.SetSiblingIndex(siblingIndex);
                }
            }

            if (enableTrigger == true && isOn == false)
                triggerObject.SetActive(false);

            else if (enableTrigger == true && isOn == true)
                triggerObject.SetActive(true);

            if (outOnPointerExit == true)
                triggerObject.SetActive(false);

            if (setHighPriorty == true)
                transform.SetAsLastSibling();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (outOnPointerExit == true)
            {
                if (isOn == true)
                {
                    Animate();
                    isOn = false;
                }

                if (isListItem == true)
                    gameObject.transform.SetParent(currentListParent, true);
            }
        }

        public void ActionForItem(GameObject go)
        {
            // Animate();

            // ChangeDropdownInfo(index = go.transform.GetSiblingIndex());
            
            if (saveSelected == true)
                PlayerPrefs.SetInt(dropdownTag + "Dropdown", go.transform.GetSiblingIndex());
        }

        public void ActionItemInvoke(int i)
        {
            dropdownItems[i].OnItemSelection.Invoke();
            ChangeDropdownInfo(i);
        }

        public void SetAnimationType()
        {
            this.animationType = AnimationType.SLIDING;
        }

        public void SetAnimationType(AnimationType test)
        {
            this.animationType = test;
        }
    }
}