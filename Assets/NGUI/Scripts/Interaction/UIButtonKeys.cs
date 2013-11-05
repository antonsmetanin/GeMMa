//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Attaching this script to a widget makes it react to key events such as tab, up, down, etc.
/// </summary>

[RequireComponent(typeof(Collider))]
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class UIButtonKeys : UISubscriber
{
	public bool startsSelected = false;
	public UIButtonKeys selectOnClick;
	public UIButtonKeys selectOnUp;
	public UIButtonKeys selectOnDown;
	public UIButtonKeys selectOnLeft;
	public UIButtonKeys selectOnRight;

	protected override void Enable ()
	{
		if (startsSelected && UICamera.selectedObject == null)
		{
			if (!NGUITools.GetActive(UICamera.selectedObject.gameObject))
			{
				UICamera.selectedObject = receiver;
			}
			else
			{
                receiver.OnHover(true);
//				UICamera.Notify(gameObject, "OnHover", true);
			}
		}
	}
	 
	protected override void OnKey (KeyCode key)
	{
		if (enabled && NGUITools.GetActive(gameObject))
		{
			switch (key)
			{
			case KeyCode.LeftArrow:
				if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.receiver;
				break;
			case KeyCode.RightArrow:
				if (selectOnRight != null) UICamera.selectedObject = selectOnRight.receiver;
				break;
			case KeyCode.UpArrow:
				if (selectOnUp != null) UICamera.selectedObject = selectOnUp.receiver;
				break;
			case KeyCode.DownArrow:
				if (selectOnDown != null) UICamera.selectedObject = selectOnDown.receiver;
				break;
			case KeyCode.Tab:
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.receiver;
					else if (selectOnUp != null) UICamera.selectedObject = selectOnUp.receiver;
					else if (selectOnDown != null) UICamera.selectedObject = selectOnDown.receiver;
					else if (selectOnRight != null) UICamera.selectedObject = selectOnRight.receiver;
				}
				else
				{
					if (selectOnRight != null) UICamera.selectedObject = selectOnRight.receiver;
					else if (selectOnDown != null) UICamera.selectedObject = selectOnDown.receiver;
					else if (selectOnUp != null) UICamera.selectedObject = selectOnUp.receiver;
					else if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.receiver;
				}
				break;
			}
		}
	}

	protected override void OnClick ()
	{
		if (enabled && selectOnClick != null)
		{
			UICamera.selectedObject = selectOnClick.receiver;
		}
	}
}
