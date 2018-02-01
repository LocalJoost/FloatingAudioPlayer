using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.Buttons;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class IconToggler : MonoBehaviour, IInputClickHandler
{

    public Texture2D Icon1;

    public Texture2D Icon2;

    public string Text1;

    public string Text2;

    private TextMesh _textMesh;

    private GameObject _buttonFace;

    // Use this for initialization
    void Awake ()
	{
	    _buttonFace = gameObject.transform.Find("UIButtonSquare/UIButtonSquareIcon").gameObject;
        var text = gameObject.transform.Find("UIButtonSquare/Text").gameObject;
	    _textMesh = text.GetComponent<TextMesh>();
	    SetBaseState();
	}

    public void SetBaseState()
    {
       _textMesh.text = Text1;
       _buttonFace.GetComponent<Renderer>().sharedMaterial.mainTexture = Icon1;
    }

    private float _lastClick;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (Time.time - _lastClick > 0.1)
        {
            _lastClick = Time.time;
            Toggle();
        }
    }

    public void Toggle()
    {
        var material = _buttonFace.GetComponent<Renderer>().sharedMaterial;
        material.mainTexture = material.mainTexture == Icon1 ? Icon2 : Icon1;
       _textMesh.text = _textMesh.text == Text1 ? Text2 : Text1;
    }
}
