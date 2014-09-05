using UnityEngine;
using System.Collections;
using Skill.UI;

/// <summary>
/// a window that shows how to use DockPanel, Toolbar, WrapImage, Slider, ToggleButton 
/// </summary>
public class DockWindow : Window
{
    /// <summary>
    /// Gets or sets texture image for WrapImage control
    /// </summary>
    public Texture WrapImageTexture { get { return _WrapImage.Texture; } set { _WrapImage.Texture = value; } }

    // variables
    private DockPanel _DockPanel;
    private Toolbar _Toolbar;
    private ToolbarButton _Button1;
    private ToolbarButton _Button2;
    private ToolbarButton _Button3;

    private Slider _VerticalSlider, _HorizontalSlider;
    private TextArea _TextArea;
    private WrapImage _WrapImage;
    private Grid _RightGridPanel;
    private ToggleButton _WrapU;
    private ToggleButton _WrapV;
    private WindowSizeControl _WindowSize;

    public DockWindow()
    {
        base.Title.text = "DockPanel, Toolbar, WrapImage, Slider, ToggleButton";

        // create dock panel
        _DockPanel = new DockPanel() { LastChildFill = true, Name = "DockPanel", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

        // create controls

        _Toolbar = new Toolbar() { Dock = Dock.Top, Height = 20, Name = "Toolbar", Margin = new Thickness(4) };
        _Button1 = new ToolbarButton() { Name = "Button1" };
        _Button1.Selected += new System.EventHandler(_Button_Selected);
        _Button1.Content.text = "Button1";
        _Button2 = new ToolbarButton() { Name = "Button2" };
        _Button2.Content.text = "Button2";
        _Button2.Selected += new System.EventHandler(_Button_Selected);
        _Button3 = new ToolbarButton() { Name = "Button3" };
        _Button3.Content.text = "Button3";
        _Button3.Selected += new System.EventHandler(_Button_Selected);
        _Toolbar.Items.Add(_Button1);
        _Toolbar.Items.Add(_Button2);
        _Toolbar.Items.Add(_Button3);
        _Toolbar.SelectedIndex = 1;

        _VerticalSlider = new Slider() { Orientation = Orientation.Vertical, Width = 16, Margin = new Thickness(2), MaxValue = 100, MinValue = 0, Dock = Dock.Left, Name = "VerticalSlider" };
        _HorizontalSlider = new Slider() { Orientation = Orientation.Horizontal, Height = 16, Margin = new Thickness(2), MaxValue = 100, MinValue = 0, Dock = Dock.Bottom, Name = "HorizontalSlider" };

        _VerticalSlider.ValueChanged += new System.EventHandler(_Slider_ValueChanged);
        _HorizontalSlider.ValueChanged += new System.EventHandler(_Slider_ValueChanged);

        _TextArea = new TextArea() { Text = "This is a TextArea ...", Name = "TextArea", Dock = Dock.Top };
        _TextArea.TextChanged += new System.EventHandler(_TextArea_TextChanged);

        _WrapImage = new WrapImage() { Row = 0, Column = 0 };

        _WrapU = new ToggleButton() { Width = 18, Height = 18, IsChecked = true, Row = 1, Column = 0, Name = "WrapU", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(4, 0, 0, 0) };
        _WrapU.Content.text = "WrapU";
        _WrapU.Changed += new System.EventHandler(_WrapU_Changed);

        _WrapV = new ToggleButton() { Width = 18, Height = 18, IsChecked = true, Row = 2, Column = 0, Name = "WrapV", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(4, 0, 0, 0) };
        _WrapV.Content.text = "WrapV";
        _WrapV.Changed += new System.EventHandler(_WrapV_Changed);

        _RightGridPanel = new Skill.UI.Grid() { Dock = Dock.Right, Width = 132, Margin = new Thickness(2) };
        _RightGridPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        _RightGridPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24, GridUnitType.Pixel) });
        _RightGridPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24, GridUnitType.Pixel) });
        _RightGridPanel.Controls.Add(_WrapImage);
        _RightGridPanel.Controls.Add(_WrapU);
        _RightGridPanel.Controls.Add(_WrapV);

        _WindowSize = new WindowSizeControl(this) { Margin = new Thickness(4, 2, 4, 2), Dock = Dock.Bottom, Height = 28 };
        _WindowSize.SelectedChanged += new System.EventHandler(_WindowSize_SelectedChanged);

        // arrange of controls in dock panel is important
        _DockPanel.Controls.Add(_Toolbar);
        _DockPanel.Controls.Add(_WindowSize);
        _DockPanel.Controls.Add(_HorizontalSlider);
        _DockPanel.Controls.Add(_VerticalSlider);
        _DockPanel.Controls.Add(_RightGridPanel);
        _DockPanel.Controls.Add(_TextArea);

        base.Grid.Controls.Add(_DockPanel);


        base.Grid.Margin = new Thickness(4, 16, 4, 4);
        IsDraggable = true;
        UpdateDraggableArea();
    }

    // change WrapV property of WrapImage control
    void _WrapV_Changed(object sender, System.EventArgs e)
    {
        _WrapImage.WrapV = _WrapV.IsChecked;
    }

    // change WrapU property of WrapImage control
    void _WrapU_Changed(object sender, System.EventArgs e)
    {
        _WrapImage.WrapU = _WrapU.IsChecked;
    }

    // log that text of TextArea is changed
    void _TextArea_TextChanged(object sender, System.EventArgs e)
    {
        Debug.Log(string.Format("text of {0} changed.", ((TextArea)sender).Name));
    }

    // log that value of a Slider is changed
    void _Slider_ValueChanged(object sender, System.EventArgs e)
    {
        Debug.Log(string.Format("{0} value changed : {1}", ((Slider)sender).Name, ((Slider)sender).Value));
    }

    // log that witch ToolbarButton is selected
    void _Button_Selected(object sender, System.EventArgs e)
    {
        Debug.Log(string.Format("{0} is selected.", ((ToolbarButton)sender).Name));
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
}
