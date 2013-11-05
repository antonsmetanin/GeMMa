using UnityEngine;
using System.Collections;
using System;


public class UIReceiver : MonoBehaviour
{
    public Action<bool> PressEvent;
    public Action ClickEvent;
    public Action DoubleClickEvent;
    public Action<bool> HoverEvent;
    public Action<bool> SelectEvent;
    public Action<string> InputEvent;
    public Action<float> ScrollEvent;
    public Action<Vector2> DragEvent;
    public Action<UIReceiver> DropEvent;
    public Action<bool> TooltipEvent;
    public Action<KeyCode> KeyEvent;


    public void OnPress(bool pressed)
    {
        if (PressEvent != null) {
            PressEvent(pressed);
        }
    }


    public void OnClick()
    {
        if (ClickEvent != null) {
            ClickEvent();
        }
    }


    public void OnDoubleClick()
    {
        if (DoubleClickEvent != null) {
            DoubleClickEvent();
        }
    }


    public void OnHover(bool selected)
    {
        if (HoverEvent != null) {
            HoverEvent(selected);
        }
    }


    public void OnSelect(bool selected)
    {
        if (SelectEvent != null) {
            SelectEvent(selected);
        }
    }


    public void OnInput(string input)
    {
        if (InputEvent != null) {
            InputEvent(input);
        }
    }


    public void OnScroll(float scroll)
    {
        if (ScrollEvent != null) {
            ScrollEvent(scroll);
        }
    }


    public void OnDrag(Vector2 delta)
    {
        if (DragEvent != null) {
            DragEvent(delta);
        }
    }


    public void OnDrop(UIReceiver receiver)
    {
        if (DropEvent != null) {
            DropEvent(receiver);
        }
    }


    public void OnTooltip(bool val)
    {
        if (TooltipEvent != null) {
            TooltipEvent(val);
        }
    }


    public void OnKey(KeyCode key)
    {
        if (KeyEvent != null) {
            KeyEvent(key);
        }
    }
}
