//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Simple example script of how a button can be colored when the mouse hovers over it or it gets pressed.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UISubscriber
{
	/// <summary>
	/// Target with a widget, renderer, or light that will have its color tweened.
	/// </summary>

	public GameObject tweenTarget;

	/// <summary>
	/// Color to apply on hover event (mouse only).
	/// </summary>

	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	/// <summary>
	/// Color to apply on the pressed event.
	/// </summary>

	public Color pressed = Color.grey;

	/// <summary>
	/// Duration of the tween process.
	/// </summary>

	public float duration = 0.2f;

	protected Color mColor;
	protected bool mStarted = false;
	protected bool mHighlighted = false;

	/// <summary>
	/// UIButtonColor's default (starting) color. It's useful to be able to change it, just in case.
	/// </summary>

	public Color defaultColor
	{
		get
		{
			if (!mStarted) Init();
			return mColor;
		}
		set { mColor = value; }
	}

	void Start ()
	{
		if (!mStarted)
		{
			Init();
			mStarted = true;
		}
	}

	protected override void Enable () { if (mStarted && mHighlighted) OnHover(UICamera.IsHighlighted(receiver)); }

	protected override void Disable ()
	{
		if (mStarted && tweenTarget != null)
		{
			TweenColor tc = tweenTarget.GetComponent<TweenColor>();

			if (tc != null)
			{
				tc.color = mColor;
				tc.enabled = false;
			}
		}
	}

	protected void Init ()
	{
		if (tweenTarget == null) tweenTarget = gameObject;
		UIWidget widget = tweenTarget.GetComponent<UIWidget>();

		if (widget != null)
		{
			mColor = widget.color;
		}
		else
		{
			Renderer ren = tweenTarget.renderer;

			if (ren != null)
			{
				mColor = ren.material.color;
			}
			else
			{
				Light lt = tweenTarget.light;

				if (lt != null)
				{
					mColor = lt.color;
				}
				else
				{
					Debug.LogWarning(NGUITools.GetHierarchy(gameObject) + " has nothing for UIButtonColor to color", this);
					enabled = false;
				}
			}
		}
		Enable();
	}

	protected override void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenColor.Begin(tweenTarget, duration, isPressed ? pressed : (UICamera.IsHighlighted(receiver) ? hover : mColor));
		}
	}

	protected override void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mStarted) Start();
			TweenColor.Begin(tweenTarget, duration, isOver ? hover : mColor);
			mHighlighted = isOver;
		}
	}
}
