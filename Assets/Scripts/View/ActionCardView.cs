using UnityEngine;
using System.Collections;
using System;
using Model;


public class ActionCardView : MonoBehaviour
{
	public Action<ActionCardView> openEvent;
	
	public ActionCardData actionCard;
	
	public Renderer cardRenderer;
	public Texture2D icon;
	
	
//	void DrawTextures(Rect[] rects)
//	{
//		
//	}
	
	long rotationStartTime;
	long rotationEndTime;
	long rotationTime = 10000 * 300;
	float currentAngle;
	
	float startAngle;
	float endAngle;
	
	bool opening = false;
	bool open = false;
	bool moving = false;
	public bool clickable = true;
	
	Vector3 startPosition;
	
	
	public void Init(ActionCardData actionCard)
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
		if (clickable && !opening && !open) {
			opening = true;
			
			rotationStartTime = TimeController.CurrentTime;
			rotationEndTime = rotationStartTime + rotationTime;
		}
	}
	
	
	public void MoveToTheCenter()
	{
		bool moving = true;
		
		rotationStartTime = TimeController.CurrentTime;
		rotationEndTime = rotationStartTime + rotationTime * 10;
		
		startPosition = transform.localPosition;
	}
	
	
	void Update()
	{
		long currentTime = TimeController.CurrentTime;
		
		if (opening) {
			if (currentTime < rotationEndTime) {
				float t = (float)(currentTime - rotationStartTime) / (float)rotationTime;
				
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
			if (currentTime < rotationEndTime) {
				float t = (float)(currentTime - rotationStartTime) / (float)rotationTime;
				
				transform.localPosition = startPosition * (1.0f - t) + new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0.0f) * t;
			} else {
				moving = false;
			}
		}
		
		transform.rotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
	}
}
