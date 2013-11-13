using UnityEngine;
using System.Collections;
using System;
using Model;


public class ActionCardView : CardView
{
	public ActionCard actionCard;
	
	public Renderer cardRenderer;
	public Texture2D icon;
	
	public Renderer iconRenderer;
	
//	void DrawTextures(Rect[] rects)
//	{
//		
//	}
	
	
	public void Init(ActionCard actionCard)
	{
		this.actionCard = actionCard;
		
		SetupIcon();
	}
	
	
	void SetupIcon()
	{
		CardViewData viewData = Resources.Load("Card Icons Data/" + actionCard.owner.staticData.characterName + "/" + actionCard.data.cardName) as CardViewData;
		iconRenderer.material.mainTexture = viewData.iconTexture;
		
		DamageTypeTextureData damageTypeData = Resources.Load("Damage Type Data") as DamageTypeTextureData;
		
		ActionCardData data = actionCard.data;
		
		if (data.isPositive) {
			if (data.damageValues[(int)ParameterType.ManaPoints] > 0) {
				cardRenderer.material.mainTexture = damageTypeData.healingMP;
			} else if (data.damageValues[(int)ParameterType.PowerPoints] > 0) {
				cardRenderer.material.mainTexture = damageTypeData.healingSP;
			} else {
				cardRenderer.material.mainTexture = damageTypeData.healing;
			}
		} else {
			if (data.damageValues[(int)ParameterType.PowerPoints] > 0) {
				cardRenderer.material.mainTexture = damageTypeData.damagingSP;
			} else {
				cardRenderer.material.mainTexture = damageTypeData.damaging;
			}
		}
		
//		damageTypeData
	}
	
	
	void Start()
	{
//		Texture2D texture = Instantiate(cardRenderer.material.mainTexture) as Texture2D;
//		cardRenderer.material.mainTexture = texture;
//		
//		Color[] iconPixels = icon.GetPixels(0);
//		
//		Debug.Log("texture size: " + texture.width + ", " + texture.height);
//		Debug.Log("icon size: " + icon.width + ", " + icon.height);
//		
//		texture.SetPixels(100, 100, icon.width, icon.height, iconPixels);
//		texture.Apply(true, true);
		
		
	}
	
	
	protected override void Update ()
	{
		base.Update();
	}
}
