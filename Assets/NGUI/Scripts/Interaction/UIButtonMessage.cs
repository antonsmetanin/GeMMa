//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sends a message to the remote object when something happens.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : UISubscriber
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		OnDoubleClick,
	}

	public GameObject target;
	public string functionName;
	public Trigger trigger = Trigger.OnClick;
	public bool includeChildren = false;

	bool mStarted = false;
	bool mHighlighted = false;

	void Start () { mStarted = true; }

	protected override void Enable () { if (mStarted && mHighlighted) OnHover(UICamera.IsHighlighted(receiver)); }

	protected override void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (((isOver && trigger == Trigger.OnMouseOver) ||
				(!isOver && trigger == Trigger.OnMouseOut))) Send();
			mHighlighted = isOver;
		}
	}

	protected override void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (((isPressed && trigger == Trigger.OnPress) ||
				(!isPressed && trigger == Trigger.OnRelease))) Send();
		}
	}

	protected override void OnClick () { if (enabled && trigger == Trigger.OnClick) Send(); }

	protected override void OnDoubleClick () { if (enabled && trigger == Trigger.OnDoubleClick) Send(); }

	void Send ()
	{
		if (string.IsNullOrEmpty(functionName)) return;
		if (target == null) target = gameObject;

		if (includeChildren)
		{
			Transform[] transforms = target.GetComponentsInChildren<Transform>();

			for (int i = 0, imax = transforms.Length; i < imax; ++i)
			{
				Transform t = transforms[i];
				t.gameObject.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			target.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}
}