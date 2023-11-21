using System;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeGridContriller : MonoBehaviour
{
    [SerializeField] private Toggle _autosize;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private GridControllerInFolderDesktop gridControllerInFolderDesktop;
    [SerializeField] private InputField _rowsInput;
    [SerializeField] private InputField _columnsInput;
    [SerializeField] private Dropdown _dropdownAlignment;

    [Header("Padding")] 
    [SerializeField] private InputField _paddingLeft;
    [SerializeField] private InputField _paddingRight;
    [SerializeField] private InputField _paddingTop;
    [SerializeField] private InputField _paddingBot;

    [Header("Spacing")] 
    [SerializeField] private InputField _spacingX;
    [SerializeField] private InputField _spacingY;

    [Header("Scale")] 
    [SerializeField] private InputField _scaleX;
    [SerializeField] private InputField _scaleY;

    private bool _isOn;

    //Сделать чтобы можно было ограничивать только количество столбцов (т.е. при этом работает ресайз). При автосайзе доступны rows всегда. Добавить уствновку интревалов. Добавить центровку. И сделать, чтобы для каждого префаба своё

    private void Awake()
    {
        _isOn = _autosize.isOn;

        gridControllerInFolderDesktop.OnSetColumns += SetColumns;
    }

    private void OnDestroy()
    {
        if (gridControllerInFolderDesktop.OnSetColumns != null)
        {
            gridControllerInFolderDesktop.OnSetColumns -= SetColumns;
        }
    }

    private void Update()
    {
        if (_isOn)
        {
            _rowsInput.text = gridControllerInFolderDesktop.RowCount.ToString();
        }
    }

    public void SetColumns()
    {
        _columnsInput.text = gridControllerInFolderDesktop.ColumnCount.ToString();
    }

    public void WhatAboutRows()
    {
        if (int.TryParse(_rowsInput.text, out var rows))
        {
            gridControllerInFolderDesktop.RowCount = rows;

            gridControllerInFolderDesktop.SetColumnsAndRows();
        }
    }

    public void WhatAboutColumns()
    {
        if (int.TryParse(_columnsInput.text, out var columns))
        {
            if (columns == 0)
            {
                _grid.constraint = GridLayoutGroup.Constraint.Flexible;
                gridControllerInFolderDesktop.ResizeWindow();

                return;
            }

            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = columns;

            gridControllerInFolderDesktop.ColumnCount = columns;

            gridControllerInFolderDesktop.SetColumnsAndRows();
        }
    }

    public void WhatAboutAutosize()
    {
        _isOn = _autosize.isOn;


        if (_isOn)
        {
            gridControllerInFolderDesktop.SetColumnsAndRows();

            _rowsInput.interactable = false;

            gridControllerInFolderDesktop.IsAutosize = true;

        }
        else
        {
            _rowsInput.interactable = true;

            gridControllerInFolderDesktop.IsAutosize = false;


        }
    }

    public void AlignmentDropdown()
    {
        _grid.childAlignment = (TextAnchor)_dropdownAlignment.value;
    }

    public void SetPaddings()
    {
        if (_paddingLeft.text == "")
        {
            _paddingLeft.text = "0";
        }

        if (_paddingRight.text == "")
        {
            _paddingRight.text = "0";
        }

        if (_paddingTop.text == "")
        {
            _paddingTop.text = "0";
        }

        if (_paddingBot.text == "")
        {
            _paddingBot.text = "0";
        }

        var left = int.TryParse(_paddingLeft.text, out var leftInt);
        var right = int.TryParse(_paddingRight.text, out var rightInt);
        var top = int.TryParse(_paddingTop.text, out var topInt);
        var bot = int.TryParse(_paddingBot.text, out var botInt);

        if (!left || !right || !top || !bot)
        {
            Debug.Log("Некорректный paddings");
            return;
        }

        _grid.padding.left = leftInt;
        _grid.padding.right = rightInt;
        _grid.padding.top = topInt;
        _grid.padding.bottom = botInt;

        gridControllerInFolderDesktop.ResizeWindow();
    }

    public void SetSpacing()
    {
        if (_spacingX.text == "")
        {
            _spacingX.text = "0";
        }

        if (_spacingY.text == "")
        {
            _spacingY.text = "0";
        }

        var tryX = int.TryParse(_spacingX.text, out var x);
        var tryY = int.TryParse(_spacingY.text, out var y);

        if (!tryX || !tryY)
        {
            Debug.Log("Некорректное значение spacing");

            return;
        }

        var spacingVect = new Vector2(x, y);

        _grid.spacing = spacingVect;

        gridControllerInFolderDesktop.ResizeWindow();
    }

    public void SetScale()
    {
        if (_scaleX.text == "")
        {
            _scaleX.text = "0";
        }

        if (_scaleY.text == "")
        {
            _scaleY.text = "0";
        }

        var tryX = int.TryParse(_scaleX.text, out var x);
        var tryY = int.TryParse(_scaleY.text, out var y);

        if (!tryX || !tryY)
        {
            Debug.Log("Некорректное значение scale");
            return;
        }

        var scaleVect = new Vector2(x, y);

        _grid.cellSize = scaleVect;

        gridControllerInFolderDesktop.ResizeWindow();
    }
}
