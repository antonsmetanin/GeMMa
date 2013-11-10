using UnityEngine;
using System.Collections;
using System;
using Model;


public class ActionCardView : MonoBehaviour
{
	public Action<ActionCardView> openEvent;
	public Action<ActionCardView> centeredEvent;
	
	public ActionCard actionCard;
	
	public Renderer cardRenderer;
	public Texture2D icon;
	
	
//	void DrawTextures(Rect[] rects)
//	{
//		
//	}
	
	long actionStartTime;
	long actionEndTime;
	long rotationTime = 10000 * 300;
	float currentAngle;
	
	float startAngle;
	float endAngle;
	
	bool opening = false;
	bool open = false;
	bool moving = false;
	public bool clickable = true;
	
	Vector3 startPosition;
	
	
	public void Init(ActionCard actionCard)
	{
		this.actionCard = actionCard;
	}
	
	
	void Start()
	{
		Texture2D texture = Instantiate(cardRenderer.material.mainTexture) as Texture2D;
		cardRenderer.material.mainTexture = texture;
		
		Color[] iconPixels = icon.GetPixels(0);
		
		Debug.Log("texture size: " + texture.width + ", " + texture.height);
		Debug.Log("icon size: " + icon.width + ", " + icon.height);
		
		texture.SetPixels(100, 100, icon.width, icon.height, iconPixels);
		texture.Apply(true, true);
	}
	
	
	public void Rotate()
	{
		actionCard.owner.PullActionCard(actionCard);
		
		if (clickable && !opening && !open) {
			opening = true;
			
			actionStartTime = TimeController.CurrentTime;
			actionEndTime = actionStartTime + rotationTime;
		}
	}
	
	
	public void MoveToTheCenter()
	{
		moving = true;
		
		actionStartTime = TimeController.CurrentTime;
		actionEndTime = actionStartTime + rotationTime * 3;
		
		startPosition = transform.localPosition;
	}
	
	
	void Update()
	{
		long currentTime = TimeController.CurrentTime;
		
		if (opening) {
			if (currentTime < actionEndTime) {
				float t = (float)(currentTime - actionStartTime) / (float)rotationTime;
				
				currentAngle = t * 180.0f;
			} else {
				opening = false;
				open = true;
				
				if (openEvent != null) {
					openEvent(this);
				}
			}
		}
		
		if (moving) {
			if (currentTime < actionEndTime) {
				float t = (float)(currentTime - actionStartTime) / (float)(actionEndTime - actionStartTime);
				
				transform.localPosition = startPosition * (1.0f - t) + new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0.0f) * t;
			} else {
				moving = false;
				
				if (centeredEvent != null) {
					centeredEvent(this);
				}
			}
		}
		
		transform.rotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
	}
}
