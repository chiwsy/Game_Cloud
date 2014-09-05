using UnityEngine;
using System.Collections;
using Skill.UI;

/// <summary>
/// a window that shows how to use Grid, ListBox, TextField, StackPanel, Button
/// </summary>
public class GridWindow : Window
{
    /// <summary>
    /// Gets or set style of SelectedItem in ListBox
    /// </summary>
    public GUIStyle SelectedItemStyle { get { return _ListBox.SelectedStyle; } set { _ListBox.SelectedStyle = value; } }

    // variables
    private TextField _TextField;
    private ListBox _ListBox;
    private StackPanel _ButtonsPanel;
    private Button _AddButton;
    private Button _RemoveButton;
    private WindowSizeControl _WindowSize;

    public GridWindow()
    {
        base.Title.text = "Grid, ListBox, TextField, StackPanel, Button";

        // specify grid row definitions
        base.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(26, GridUnitType.Pixel) });
        base.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        base.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(28, GridUnitType.Pixel) });
        // specify grid column definitions
        base.Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        base.Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80, GridUnitType.Pixel) });

        // create controls

        _TextField = new TextField() { Row = 0, Column = 0, ColumnSpan = 2, Name = "ItemTextField", Margin = new Thickness(4, 2, 4, 2) };
        _ListBox = new ListBox() { Row = 1, Column = 0, Name = "ListBox", Margin = new Thickness(2), Padding = new Thickness(2) };
        _ListBox.Background.Visibility = Visibility.Visible;

        _WindowSize = new WindowSizeControl(this) { Row = 2, Column = 0, ColumnSpan = 2, Margin = new Thickness(4, 2, 4, 2) };
        _WindowSize.SelectedChanged += new System.EventHandler(_WindowSize_SelectedChanged);

        _AddButton = new Button() { Name = "Add Button", Height = 22 };
        _AddButton.Content.text = "Add";
        _AddButton.Margin = new Thickness(2, 2, 2, 0);
        _AddButton.Click += new System.EventHandler(_AddButton_Click);

        _RemoveButton = new Button() { Name = "Remove Button", Height = 22 };
        _RemoveButton.Content.text = "Remove";
        _RemoveButton.Margin = new Thickness(2, 2, 2, 0);
        _RemoveButton.Click += new System.EventHandler(_RemoveButton_Click);

        _ButtonsPanel = new StackPanel() { Row = 1, Column = 1, Orientation = Orientation.Vertical };
        _ButtonsPanel.Controls.Add(_AddButton);
        _ButtonsPanel.Controls.Add(_RemoveButton);

        base.Grid.Controls.Add(_TextField);
        base.Grid.Controls.Add(_ListBox);
        base.Grid.Controls.Add(_WindowSize);
        base.Grid.Controls.Add(_ButtonsPanel);

        base.Grid.Margin = new Thickness(4, 16, 4, 4);

        IsDraggable = true;

        _TextField.Text = "ListBox Item 6";

        AddItem("ListBox Item 1");
        AddItem("ListBox Item 2");
        AddItem("ListBox Item 3");
        AddItem("ListBox Item 4");
        AddItem("ListBox Item 5");


    }

    // top 16 pixel of window is draggable
    private void UpdateDraggableArea()
    {
        Rect dragArea = Position;
        dragArea.x = dragArea.y = 0;
        dragArea.height = 16;
        DraggableArea = dragArea;
    }

    // update DraggableArea when window size changed
    void _WindowSize_SelectedChanged(object sender, System.EventArgs e)
    {
        UpdateDraggableArea();
    }

    // add new item (Label) to ListBox
    private void AddItem(string text)
    {
        Label newItem = new Label() { Height = 20 };
        newItem.Content.text = text;
        _ListBox.Controls.Add(newItem);
        _ListBox.SelectedItem = newItem;
    }

    // remove selected item from ListBox ( if exists )
    void _RemoveButton_Click(object sender, System.EventArgs e)
    {
        if (_ListBox.SelectedItem != null)
        {
            _ListBox.Controls.Remove(_ListBox.SelectedItem);
        }
        else
            Debug.LogWarning("First select item");
    }

    // add new item to ListBox
    void _AddButton_Click(object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(_TextField.Text))
        {
            AddItem(_TextField.Text);
        }
        else
            Debug.LogWarning("Nothing to add");
    }
}
