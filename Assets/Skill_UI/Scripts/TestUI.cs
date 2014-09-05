using UnityEngine;
using System.Collections;
using Skill.UI;
using System.Collections.Generic;

public class TestUI : MonoBehaviour
{
    public GUISkin Skin;
    public Texture2D WrapTexture;

    private GridWindow _GridWindow;
    private DockWindow _DockWindow;
    
    private List<Window> _Windows; // also you can use Frame instead of Window

    // Use this for initialization
    void Start()
    {
        _Windows = new List<Window>();

        _GridWindow = new GridWindow() { X = 10, Y = 10 };
        _DockWindow = new DockWindow() { X = 420, Y = 10, WrapImageTexture = this.WrapTexture };

        _Windows.Add(_GridWindow);
        _Windows.Add(_DockWindow);
    }

    void OnGUI()
    {
        if (Skin != null)
        {
            GUI.skin = Skin;
            // use first custom style as SelectedItemStyle
            _GridWindow.SelectedItemStyle = Skin.customStyles[0];
        }

        // draw windows
        foreach (var window in _Windows)
            window.OnGUI();
    }
}
