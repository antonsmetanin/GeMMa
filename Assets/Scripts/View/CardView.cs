using UnityEngine;
using System.Collections;
using System;
using Model;


public class CardView : MonoBehaviour
{
	public Action<CardView> openEvent;
	public Action<CardView> centeredEvent;
	
	long actionStartTime;
	long actionEndTime;
	
	long rotationTime = 10000 * 300;
	
	float currentAngle;
	
	float startAngle;
	float endAngle;

	float startScale;
	float endScale;
	
	bool opening = false;
	bool open = false;
	bool moving = false;
	public bool clickable = true;
	
	Vector3 startPosition;
	Vector3 endPosition;
	
	
	public void Reset()
	{
		currentAngle = 0;
		moving = false;
		open = false;
		opening = false;
		clickable = true;
		
		actionStartTime = 0;
		actionEndTime = 0;
	}
	
	
	public void Rotate()
	{
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

		transform.parent = transform.parent.parent;
		
		startPosition = transform.localPosition;
//		endPosition = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0.0f);
		endPosition = new Vector3(this is ActionCardView ? -400f : 400f, (float)(Screen.height / 2), 0);
		startScale = transform.localScale.x;
		endScale = 800f;
	}
	
	
	protected virtual void Update()
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
				
				transform.localPosition = startPosition * (1.0f - t) + endPosition * t;
				transform.localScale = Vector3.one * (startScale * (1f - t) + endScale * t);
			} else {
				moving = false;
				
				if (centeredEvent != null) {
					centeredEvent(this);
				}
			}
		}
		
		transform.localEulerAngles = new Vector3(0, currentAngle, 0);
	}
}
