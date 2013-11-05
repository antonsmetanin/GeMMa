using UnityEngine;
using System.Collections;


public class MouseController : MonoBehaviour
{
	public Camera raycastCamera;
	
	void Update()
	{
		bool rightMouseClicked = false;
		bool leftMouseClicked = false;
		
		if (Input.GetMouseButton(0)) {
			leftMouseClicked = true;
		} else if (Input.GetMouseButton(1)) {
			rightMouseClicked = true;
		}
		
		if (leftMouseClicked || rightMouseClicked) {
			Ray ray = raycastCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
			
			RaycastHit hitInfo;
			
			if (Physics.Raycast(ray, out hitInfo, 100.0f, 1 << 8)) {
				GameObject hitObject = hitInfo.collider.gameObject;
				
//				float speed = 0.001f;
//				Vector3 direction = Vector3.up;
//				
//				if (rightMouseClicked) {
//					direction = Vector3.down;
//				}
//				
//				hitObject.transform.position = hitObject.transform.position + direction * speed;
				
				ActionCardView card = hitObject.GetComponent<ActionCardView>();
				
				card.Rotate();
			}
		}
	}
}
