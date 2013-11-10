using UnityEngine;
using System.Collections;
using Model;


public class ParametersView : MonoBehaviour
{
	public UILabel healthLabel;
	public UILabel manaLabel;
	public UILabel powerLabel;
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
		
			healthLabel.text = "HP: " + parameters[(int)ParameterType.HealthPoints] + "/" + maxParameters[(int)ParameterType.HealthPoints];
			manaLabel.text = "MP: " + parameters[(int)ParameterType.ManaPoints] + "/" + maxParameters[(int)ParameterType.ManaPoints];
			powerLabel.text = "PP: " + parameters[(int)ParameterType.PowerPoints] + "/" + maxParameters[(int)ParameterType.PowerPoints];
		}
		
		
	}
}
