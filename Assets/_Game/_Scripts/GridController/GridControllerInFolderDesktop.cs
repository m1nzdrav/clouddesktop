using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GridControllerInFolderDesktop : UpdateJson
{
    #region Local Class Objects

    GridLayoutGroup _grid;

    MytPrefabColorTypes mytPrefabColorTypes;
    #endregion

    public RectTransform GridContainer;

    //[SerializeField] GameObject SlotContainer;

    [SerializeField] GameObject prefabSlot;

    [SerializeField] private RectTransform _viewport;

    #region GridSettings
    [Header("Grid Settings")]
    [SerializeField]
    private Vector2 _cellSize = new Vector2(60f, 60f);

    [SerializeField] private Vector2 _cellLocalSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 _spacing = new Vector2(0f, 0f);
    #endregion

    #region Padding
    [Header("Padding")] [SerializeField] private int _left;
    [SerializeField] private int _right;
    [SerializeField] private int _top;
    [SerializeField] private int _bottom;
    #endregion

    [SerializeField] private bool _restruct;

    [SerializeField] private bool _isGrid;

    [SerializeField] private ContentSizeFitter _contentSize;

    #region Свойства
    public bool IsGrid
    {
        get => _isGrid;
        set => _isGrid = value;
    }

    public Vector2 CellSize
    {
        get => _cellSize;
        set => _cellSize = value;
    }

    public Vector2 Spacing
    {
        get => _spacing;
        set => _spacing = value;
    }

    public int Left
    {
        get => _left;
        set => _left = value;
    }

    public int Right
    {
        get => _right;
        set => _right = value;
    }

    public int Top
    {
        get => _top;
        set => _top = value;
    }

    public int Bottom
    {
        get => _bottom;
        set => _bottom = value;
    }

    public int RowCount1
    {
        get => RowCount;
        set => RowCount = value;
    }

    public int ColumnCount1
    {
        get => ColumnCount;
        set => ColumnCount = value;
    }

    public Vector2 Size
    {
        get => _size;
        set => _size = value;
    }

    public bool IsAutosize1
    {
        get => IsAutosize;
        set => IsAutosize = value;
    }
    #endregion

    public int RowCount;
    public int ColumnCount;

    public List<GameObject> Cells = new List<GameObject>();

    int icons_Count = 0;
    int rows_additive = 0;
    int rows_multiplier = 1;
    bool rowed = false;

    #region ForSizing
    private Vector2 _size;
    private Vector2 _saveSize;

    public bool IsAutosize = false;
    #endregion

    public Action OnSetColumns;

    #region For Namespaces Visibility
    //TODO: saving betw folders
    bool showNamespaces = true;

    public bool ShowNamespaces 
    {
        get => showNamespaces;
        
        set 
        {
            showNamespaces = value;
            onSwitchVisibility?.Invoke(showNamespaces);
        }
    }

    public delegate void SwitchNamespacesVisibility(bool showNamespaces);
    public event SwitchNamespacesVisibility onSwitchVisibility;
    #endregion

    // todo закоментил Awake Update - сейчас не работают правильно
     public void Start()
     {
         mytPrefabColorTypes = GetComponent<MytPrefabColorTypes>();
         //GridContainer = SlotContainer.GetComponent<RectTransform>();
         _grid = GridContainer.GetComponent<GridLayoutGroup>();
         
         if (_grid!=null)
         {
             _grid.cellSize = _cellSize;
             _grid.spacing = _spacing;
             _grid.padding.left = _left;
             _grid.padding.right = _right;
             _grid.padding.top = _top;
             _grid.padding.bottom = _bottom;
    
             SetGridIcons();
         }
        
    
         // _contentSize.verticalFit = ContentSize.FitMode.Unconstrained;
    
         GridContainer.offsetMin = new Vector2(0, 0);
         GridContainer.offsetMax = new Vector2(0, 0);
    
         var width = GridContainer.rect.width;
         var height = GridContainer.rect.height;
         _size = new Vector2(width, height);
     }

    public void AddIcon(GameObject icon, bool isGrid)
    {
        int childCount = GridContainer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var currentSlot = GridContainer.transform.GetChild(i);

            //Debug.Log("current slot " + currentSlot.gameObject.name);

            if (currentSlot.childCount <= 1)
            {
                //Настройка текста
                if (isGrid)
                {
                    //gridController.SetTextForList(icon.transform);
                    SetTextForList(icon.transform);
                }
                else
                {
                    //gridController.SetTextForGrid(icon.transform);
                    SetTextForGrid(icon.transform);
                }

                //настройка ячейки
                //currentSlot.GetChild(0).gameObject.SetActive(false);

                MaskControl_cell maskControl = currentSlot.GetComponent<MaskControl_cell>();
                maskControl.gridControllerInFolderDesktop = this;
                maskControl.enabled = true;
                //maskControl.MaskEventInitial();

                //Присовение иконки ячейке и настройка
                icon.transform.SetParent(currentSlot, false);
                icon.transform.localPosition = Vector3.zero;
                //TODO: поправить все ошибки и вернуть закоменченное - это фильтр видимости наименований папок
                /*NameField_inceractability nameField_Inceractability = icon.GetComponent<NameField_inceractability>();
                nameField_Inceractability.gridControllerInFolderDesktop = this;
                nameField_Inceractability.enabled = true;*/


                //var glow = currentSlot.GetChild(0);
                //var color = icon.GetComponent<Image>().color;
                //glow.GetComponent<Image>().color = color;

                icon.GetComponent<RectTransform>().sizeDelta = currentSlot.GetComponent<RectTransform>().sizeDelta;

                icons_Count++;
                return;
            }
        }

        AddCells();
        var newCurrentSlot = Instantiate(prefabSlot.transform, GridContainer.transform);
        icon.transform.SetParent(newCurrentSlot, false);
        icon.transform.localPosition = Vector3.zero;
        Cells.Add(newCurrentSlot.gameObject);
        newCurrentSlot.name = Cells.Count.ToString();

        //var newGlow = newCurrentSlot.GetChild(0);
        //var newColor = icon.GetComponent<Image>().color;
        //newGlow.GetComponent<Image>().color = newColor;

        //newCurrentSlot.SetAsFirstSibling();
    }

    //TODO: подумать над более оптимизированным вариантом
    /// <summary>
    /// Полная очистка списка и добавление ячеек
    /// </summary>
    public void AddCells()
    {
        Cells.Clear();

        for (int i = 0; i < GridContainer.childCount; i++)
        {
            var child = GridContainer.transform.GetChild(i);
            Cells.Add(child.gameObject);
        }
    }

    //здесь пока только Autosize
    private void Update()
    {
        if (IsAutosize)
        {
            var width = GridContainer.rect.width;
            var height = GridContainer.rect.height;
            var size = new Vector2(width, height);
        
            if (size != _size)
            {
                ResizeWindow();
            }
        }
    }

    [Button]
    public void OpenFile()
    {
        SetDefaultParameters();
        WhatAboutSells();

        IsAutosize = true;
    }

    public void SetColumnsAndRows()
    {
        WhatAboutSells();
    }

    public void ResizeWindow()
    {
        // SetDefaultParameters();
        WhatAboutSells();

        _grid.CalculateLayoutInputVertical();
    }

    #region Validate

    //private void OnValidate()
    //{
    //    if (_grid == null)
    //    {
    //        SetGrid();
    //    }

    //    if (GridContainer != null)
    //    {
    //        _grid = GridContainer.GetComponent<GridLayoutGroup>();

    //        SetParameters();
    //    }
    //}

    //private void SetGrid()
    //{
    //    if (GridContainer != null)
    //    {
    //        if (GridContainer.GetComponent<GridLayoutGroup>() == null)
    //        {
    //            GridContainer.gameObject.AddComponent<GridLayoutGroup>();
    //            EditorUtility.DisplayDialog("SUCCESS", "Grid added", "OK");
    //        }

    //        _grid = GridContainer.GetComponent<GridLayoutGroup>();
    //    }

    //    if (_grid != null)
    //    {
    //        SetDefaultParameters();
    //    }
    //}

    #endregion

    private void SetDefaultParameters()
    {
        var width = _viewport.rect.width;
        var height = _viewport.rect.height;

        _size = new Vector2(width, height);

        float rows = 1f;
        float columns = 1f;

        if (_grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            rows = (_size.y - (_grid.padding.top + _grid.padding.bottom) + _grid.spacing.y) /
                   (_grid.cellSize.y + _grid.spacing.y);
        }
        else if (_grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            columns = (_size.x - (_grid.padding.left + _grid.padding.right) + _grid.spacing.x) /
                      (_grid.cellSize.x + _grid.spacing.x);
        }
        else
        {
            rows = (_size.y - (_grid.padding.top + _grid.padding.bottom) + _grid.spacing.y) /
                   (_grid.cellSize.y + _grid.spacing.y);

            columns = (_size.x - (_grid.padding.left + _grid.padding.right) + _grid.spacing.x) /
                      (_grid.cellSize.x + _grid.spacing.x);
        }

        RowCount = (int)rows + rows_additive; //* rows_multiplier;
        ColumnCount = (int)columns;

        if(ColumnCount > 1) 
        {
            if (icons_Count > RowCount * ColumnCount * 2 / 3)
            {
                //rows_multiplier++;
                rows_additive = RowCount * ColumnCount / 3;
                RowCount = (int)rows + rows_additive;//* rows_multiplier;
            }
        }
    }

    private void SetParameters()
    {
        _grid.cellSize = _cellSize;
        _grid.padding.left = _left;
        _grid.padding.right = _right;
        _grid.padding.top = _top;
        _grid.padding.bottom = _bottom;
        _grid.spacing = _spacing;
    }

    private void WhatAboutSells()
    {
        OnSetColumns?.Invoke();

        int have = GridContainer.transform.childCount;
        int max = RowCount * ColumnCount;

        if (have > max)
        {
            TurnOffUnnecessary(max, have);

            have = GridContainer.transform.childCount;

            //Debug.Log("have2 " + have);

            if (have > max)
            {
                if (_contentSize != null)
                {
                    _contentSize.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }

                if (_viewport == null) return;

                if (_size.y < _viewport.rect.height)
                {
                    _contentSize.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

                    GridContainer.offsetMin = new Vector2(0, 0);
                    GridContainer.offsetMax = new Vector2(0, 0);

                    var width = GridContainer.rect.width;
                    var height = GridContainer.rect.height;
                    _size = new Vector2(width, height);
                }
            }
        }

        if (have < max)
        {
            CreateScells(max, have);
        }

        //if (have == max)
        //{
        //    TurnOn(have);
        //}
    }

    private void CreateScells(int max, int have)
    {
        Vector3 localScale;

        if (have != 0)
        {
            AddCells();
            localScale = Cells[0].transform.localScale;
        }
        else
        {
            localScale = Vector3.one;
        }

        for (int i = have; i < max; i++)
        {
            Transform instantiateCell = (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)? .GridCellPool.GetObject().transform;
            instantiateCell.gameObject.SetActive(true);
            instantiateCell.SetParent(GridContainer, false);
            instantiateCell.localPosition = Vector3.zero;
            instantiateCell.localScale = localScale;
            instantiateCell.GetComponent<ChildBorder>()?.SetColor(mytPrefabColorTypes.ParentBorder.color);
            instantiateCell.transform.GetChild(0).gameObject.GetComponent<ChildBorder>().SetColor(mytPrefabColorTypes.ParentBorder.color);
            instantiateCell.GetComponent<GridCell>().MyInventory = this;

            Cells.Add(instantiateCell.gameObject);
            instantiateCell.name = (i + 1).ToString();
        }
    }

    private void TurnOffUnnecessary(int max, int have)
    {
        //if (_contentSize != null)
        //{
        //    if (_contentSize.verticalFit != ContentSize.FitMode.Unconstrained)
        //    {
        //        _contentSize.verticalFit = ContentSize.FitMode.Unconstrained;

        //        GridContainer.offsetMin = new Vector2(0, 0);
        //        GridContainer.offsetMax = new Vector2(0, 0);

        //        var width = GridContainer.rect.width;
        //        var height = GridContainer.rect.height;
        //        _size = new Vector2(width, height);
        //    }
        //}

        for (int i = have - 1; i > max - 1; i--)
        {
            if (i < 0)
            {
                return;
            }
            
            if (Cells[i].transform.childCount > 1)
            {
                //if (_contentSize != null)
                //{
                //    if (_viewport != null)
                //    {
                //        var widthViewport = GridContainer.rect.width;
                //        var heightViewport = GridContainer.rect.height;
                //        var viewportSize = new Vector2(widthViewport, heightViewport);


                //        if (_size.y > viewportSize.y)
                //        {
                //            _contentSize.verticalFit = ContentSize.FitMode.PreferredSize;
                //        }
                //        else
                //        {
                //            _contentSize.verticalFit = ContentSize.FitMode.Unconstrained;

                //            GridContainer.offsetMin = new Vector2(0, 0);
                //            GridContainer.offsetMax = new Vector2(0, 0);

                //            var width = GridContainer.rect.width;
                //            var height = GridContainer.rect.height;
                //            _size = new Vector2(width, height);
                //        }
                //    }
                //}

                break;
            }

            Cells[i].SetActive(false);
            Cells[i].transform.SetParent((RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform);
            Cells.RemoveAt(i);
        }
    }

    [Button]
    private void Test()
    {
        if (GridContainer.transform.childCount != 0 && Cells.Count == 0)
        {
            for (int i = 0; i < GridContainer.childCount; i++)
            {
                var child = GridContainer.transform.GetChild(i);
                Cells.Add(child.gameObject);
            }
        }

        _grid = GridContainer.GetComponent<GridLayoutGroup>();
        OpenFile();
    }

    private void TurnOn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Cells[i]?.SetActive(true);
            //Cells[i].transform.localScale = Vector3.one;
        }
    }

    public void UpdateListGrid()
    {
        if (!_restruct) return;

        if (!IsGrid)
        {
            IsGrid = !IsGrid;
            SetGridIcons();
        }
        else
        {
            IsGrid = !IsGrid;
            SetListIcons();
        }
    }

    [Button]
    public void SetListIcons()
    {
        if (IsGrid) return;

        IsGrid = true;
        if (_grid == null)
        {
            Start();
        }

        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = 1;
        _grid.spacing = new Vector2(0f, -20f);
        _grid.padding.left = 0;
        _grid.padding.top = 0;

        var counter = 0;

        AddCells();

        for (int i = 0; i < Cells.Count; i++)
        {
            var cellTransform = Cells[i].transform;
            var childCount = Cells[i].transform.childCount;

            Cells[i].transform.localScale = Vector3.one / 2f;
            //cellTransform.localPosition = new Vector3(-40, 20, 0);

            if (childCount > 1)
            {
                var icon = cellTransform.GetChild(1);
                SetTextForList(icon);

                cellTransform.SetSiblingIndex(counter);
                counter++;
            }
        }

        ResizeWindow();
    }

    [Button]
    public void SetGridIcons()
    {
        if (!IsGrid) return;

        if (_grid == null)
        {
            Start();
        }

        _grid.constraint = GridLayoutGroup.Constraint.Flexible;
        _grid.spacing = _spacing;
        _grid.padding.left = 17;
        _grid.padding.top = 17;

        IsGrid = false;

        AddCells();

        for (int i = 0; i < Cells.Count; i++)
        {
            var cellTransform = Cells[i].transform;
            var childCount = Cells[i].transform.childCount;

            Cells[i].transform.localScale = Vector3.one;
            cellTransform.localPosition = Vector3.zero;

            if (childCount > 1)
            {
                var icon = cellTransform.GetChild(1);
                SetTextForGrid(icon);
            }

            int.TryParse(cellTransform.name, out var index);
            cellTransform.SetSiblingIndex(index - 1);
        }

        ResizeWindow();
    }

    #region TopButtonPanelArea_Buttons

    public void BtnSwitchIconNamespaces() 
    {
        if (showNamespaces) 
            showNamespaces = false;
        else 
            showNamespaces = true;

        onSwitchVisibility.Invoke(showNamespaces);
    }

    public void BtnList()
    {
        SetListIcons();
        SetJson(gameObject);
    }

    public void BtnGrid()
    {
        SetGridIcons();
        SetJson(gameObject);
    }

    #endregion

    public void SetTextForList(Transform icon)
    {
        var childText = icon.GetChild(0);
        childText.localScale = Vector3.one * 2f;
        childText.localPosition = new Vector3(120f, 15f, 0f);
    }

    public void SetTextForGrid(Transform icon)
    {
        var childText = icon.GetChild(0);
        childText.localScale = Vector3.one;
        childText.localPosition = new Vector3(0f, -40f, 0f);
    }
}