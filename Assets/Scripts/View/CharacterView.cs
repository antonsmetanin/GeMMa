using UnityEngine;
using System.Collections;
using Model;


public class CharacterView : MonoBehaviour
{
	public Character character;
	

	public void Init(Character character)
	{
		this.character = character;
		
		character.actionCardPulledEvent += OnActionCardPulled;
		character.resultCardPulledEvent += OnResultCardPulled;
		character.atbGaugeFullEvent += OnATBGaugeFull;
		
		transform.position = new Vector3(character.position.x, 0.0f, character.position.y);
		
		if (character.position.x < 3) {
			
		} else {
//			transform.position = new Vector3(character.position.x, 0.0f, character.position.y);
			transform.localScale = Vector3.one * 0.5f;
		}
		
		if (Events.characterViewCreatedEvent != null) {
			Events.characterViewCreatedEvent(this);
		}
	}
	
	
	void OnActionCardPulled(Character targetCharacter, ActionCard card)
	{
		
	}
	
	
	void OnResultCardPulled(Character targetCharacter, ResultCard card)
	{
		
	}
	
	
	void OnATBGaugeFull(Character targetCharacter)
	{
		
	}
}
