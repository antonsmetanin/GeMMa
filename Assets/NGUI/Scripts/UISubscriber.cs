using UnityEngine;
using System.Collections;


[ExecuteInEditMode()]
[RequireComponent(typeof(UIReceiver))]
public class UISubscriber : MonoBehaviour
{
    public UIReceiver receiver;

    protected virtual void Enable() { }
    protected virtual void Disable() { }


    void Start()
    {
        if (receiver == null) receiver = GetComponent<UIReceiver>();
    }


    void OnEnable()
    {
        receiver = GetComponent<UIReceiver>();

        receiver.ClickEvent += OnClick;
        receiver.DoubleClickEvent += OnDoubleClick;
        receiver.PressEvent += OnPress;
        receiver.HoverEvent += OnHover;
        receiver.SelectEvent += OnSelect;
        receiver.InputEvent += OnInput;
        receiver.ScrollEvent += OnScroll;
        receiver.DragEvent += OnDrag;
        receiver.DropEvent += OnDrop;
        receiver.TooltipEvent += OnTooltip;
        receiver.KeyEvent += OnKey;

        Enable();
    }


    void OnDisable()
    {
        receiver = GetComponent<UIReceiver>();

        receiver.ClickEvent -= OnClick;
        receiver.DoubleClickEvent -= OnDoubleClick;
        receiver.PressEvent -= OnPress;
        receiver.HoverEvent -= OnHover;
        receiver.SelectEvent -= OnSelect;
        receiver.InputEvent -= OnInput;
        receiver.ScrollEvent -= OnScroll;
        receiver.DragEvent -= OnDrag;
        receiver.DropEvent -= OnDrop;
        receiver.TooltipEvent -= OnTooltip;
        receiver.KeyEvent -= OnKey;

        Disable();
    }

    protected virtual void OnPress(bool isPressed) { }
    protected virtual void OnClick() { }
    protected virtual void OnDoubleClick() { }
    protected virtual void OnHover(bool isOver) { }
    protected virtual void OnSelect(bool selected) { }
    protected virtual void OnInput(string input) { }
    protected virtual void OnScroll(float scroll) { }
    protected virtual void OnDrag(Vector2 delta) { }
    protected virtual void OnDrop(UIReceiver receiver) { }
    protected virtual void OnTooltip(bool val) { }
    protected virtual void OnKey(KeyCode key) { }
}
