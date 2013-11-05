//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sample script showing how easy it is to implement a standard button that swaps sprites.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : UISubscriber
{
	public UISprite target;
	public string normalSprite;
	public string hoverSprite;
	public string pressedSprite;
	public string disabledSprite;
	
	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;

			if (col.enabled != value)
			{
				col.enabled = value;
				UpdateImage();
			}
		}
	}

	void Awake () { if (target == null) target = GetComponentInChildren<UISprite>(); }
	protected override void Enable () { UpdateImage(); }
	
	void UpdateImage()
	{
		if (target != null)
		{
			if (isEnabled)
			{
				target.spriteName = UICamera.IsHighlighted(receiver) ? hoverSprite : normalSprite;
			}
			else
			{
				target.spriteName = disabledSprite;
			}	
			target.MakePixelPerfect();
		}
	}

	protected override void OnHover (bool isOver)
	{
		if (isEnabled && target != null)
		{
			target.spriteName = isOver ? hoverSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}

	protected override void OnPress (bool pressed)
	{
		if (pressed)
		{
			target.spriteName = pressedSprite;
			target.MakePixelPerfect();
		}
		else UpdateImage();
	}
}
