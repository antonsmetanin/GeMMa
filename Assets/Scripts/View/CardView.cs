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
	
	bool opening = false;
	bool open = false;
	bool moving = false;
	public bool clickable = true;
	
	Vector3 startPosition;
	
	
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
		
		startPosition = transform.localPosition;
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
