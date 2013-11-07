using UnityEngine;
using System.Collections;
using Model;


public class ParametersView : MonoBehaviour
{
	public UILabel label;
	CharacterView character;
	
	
	public void Init(CharacterView character)
	{
		this.character = character;
	}
	
	
	void Update()
	{
		if (character != null) {
			Vector3 pos = Camera.main.WorldToScreenPoint(character.transform.position + Vector3.up * 1.5f);
			pos.z = 0.0f;
			transform.localPosition = pos;
			
			int[] parameters = character.character.currentParameters;
			int[] maxParameters = character.character.staticData.maxParameters;
		
			label.text = "" + parameters[(int)ParameterType.HealthPoints] + "/" + maxParameters[(int)ParameterType.HealthPoints];
	//		label.text = "" + parameters[(int)ParameterType.HealthPoints] + "/" + maxParameters[(int)ParameterType.HealthPoints];
		}
		
		
	}
}
