using UnityEngine;
using System.Collections;
using Skill.UI;
using Skill.Text;

public class PersianTextField : MonoBehaviour
{
    public GUISkin Skin;
    public float Px = 0.8f;
    public float Py = 0.6f;

    private TextField _TextField;
    private Label _StaticText;
    private Frame _Frame;

    private PersianCharacterMap _Map;
    private PersianTextConverter _PC;

    // Use this for initialization
    void Start()
    {
        _Frame = new Frame();
        _Frame.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(64, GridUnitType.Pixel) });
        _Frame.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(64, GridUnitType.Pixel) });
        _Frame.Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

        _Map = new PersianCharacterMap();
        _PC = new PersianTextConverter(_Map);

        _TextField = new TextField() { Row = 1, Column = 0, Margin = new Thickness(4) };
        _TextField.Text = "jhd\\ ;kdn : ";
        _TextField.Name = "TextField";
        _TextField.Converter = new PersianTextFieldConverter(_Map) { ConvertLigature = false };

        _StaticText = new Label() { Row = 0, Column = 0, Margin = new Thickness(4) };
        _StaticText.Content.text = _PC.Convert("hdk d; ljk ehfj hsj.");
        _StaticText.Name = "StaticText";

        _Frame.Grid.Controls.Add(_TextField);
        _Frame.Grid.Controls.Add(_StaticText);
    }


    void OnGUI()
    {
        // set position of frame at center of screen

        if (Px < 0.05f) Px = 0.05f;
        else if (Px > 1.0f) Px = 1.0f;
        if (Py < 0.05f) Py = 0.05f;
        else if (Py > 1.0f) Py = 1.0f;

        Vector2 size = new Vector2(Screen.width * Px, Screen.height * Py);
        _Frame.Position = new Rect((Screen.width - size.x) * 0.5f, (Screen.height - size.y) * 0.5f, size.x, size.y);

        if (Skin != null)
            GUI.skin = Skin;

        _Frame.OnGUI();
    }
}
