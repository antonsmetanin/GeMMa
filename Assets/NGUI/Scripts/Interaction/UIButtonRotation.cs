//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Simple example script of how a button can be rotated visibly when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : UISubscriber
{
	public Transform tweenTarget;
	public Vector3 hover = Vector3.zero;
	public Vector3 pressed = Vector3.zero;
	public float duration = 0.2f;

	Quaternion mRot;
	bool mStarted = false;
	bool mHighlighted = false;

	void Start ()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null) tweenTarget = transform;
			mRot = tweenTarget.localRotation;
		}
	}

	protected override void Enable () { if (mStarted && mHighlighted) OnHover(UICamera.IsHighlighted(receiver)); }

	protected override void Disable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenRotation tc = tweenTarget.GetComponent<TweenRotation>();

			if (tc != null)
			{
				tc.rotation = mRot;
				tc.enabled = false;
			}
		}
	}

	protected override void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenRotation.Begin(tweenTarget.gameObject, duration, isPressed ? mRot * Quaternion.Euler(pressed) :
				(UICamera.IsHighlighted(receiver) ? mRot * Quaternion.Euler(hover) : mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	protected override void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenRotation.Begin(tweenTarget.gameObject, duration, isOver ? mRot * Quaternion.Euler(hover) : mRot).method = UITweener.Method.EaseInOut;
			mHighlighted = isOver;
		}
	}
}
