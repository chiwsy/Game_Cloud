using UnityEngine;
using System.Collections;
using Skill.UI;

/// <summary>
/// A CustomControl to change size of window
/// </summary>
public class WindowSizeControl : SelectionGrid
{
    /// <summary> Gets or sets small size of window ( default is 300) </summary>
    public Size SmallSize { get; set; }
    /// <summary> Gets or sets medium size of window ( default is 400) </summary>
    public Size MediumSize { get; set; }
    /// <summary> Gets or sets big size of window ( default is 500) </summary>
    public Size BigSize { get; set; }

    private SelectionGridItem _SmallSize;
    private SelectionGridItem _MediumSize;
    private SelectionGridItem _BigSize;

    /// <summary>
    /// Owner window
    /// </summary>
    public Window OwnerWindow { get; private set; }

    /// <summary>
    /// Create a WindowSizeControl
    /// </summary>
    /// <param name="window">Owner window</param>
    public WindowSizeControl(Window window)
    {
        this.OwnerWindow = window;
        this.BigSize = new Size(500,500);
        this.MediumSize = new Size(400, 400);
        this.SmallSize = new Size(300, 300);

        _SmallSize = new SelectionGridItem() { Name = "Small Size" };
        _SmallSize.Content.text = "Small";
        _SmallSize.Selected += new System.EventHandler(_SmallSize_Selected);

        _MediumSize = new SelectionGridItem() { Name = "Medium Size" };
        _MediumSize.Content.text = "Medium";
        _MediumSize.Selected += new System.EventHandler(_MediumSize_Selected);

        _BigSize = new SelectionGridItem() { Name = "Big Size" };
        _BigSize.Content.text = "Big";
        _BigSize.Selected += new System.EventHandler(_BigSize_Selected);

        this.XCount = 3;
        Items.Add(_SmallSize);
        Items.Add(_MediumSize);
        Items.Add(_BigSize);
        SelectedIndex = 1;
    }

    // change window size to big size
    void _BigSize_Selected(object sender, System.EventArgs e)
    {
        Rect pos = OwnerWindow.Position;
        pos.width = BigSize.Width;
        pos.height = BigSize.Height;
        OwnerWindow.Position = pos;
    }

    // change window size to medium size
    void _MediumSize_Selected(object sender, System.EventArgs e)
    {
        Rect pos = OwnerWindow.Position;
        pos.width = MediumSize.Width;
        pos.height = MediumSize.Height;
        OwnerWindow.Position = pos;
    }

    // change window size to small size
    void _SmallSize_Selected(object sender, System.EventArgs e)
    {
        Rect pos = OwnerWindow.Position;
        pos.width = SmallSize.Width;
        pos.height = SmallSize.Height;
        OwnerWindow.Position = pos;
    }
}
