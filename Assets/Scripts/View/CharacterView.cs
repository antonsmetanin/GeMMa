using UnityEngine;
using System.Collections;
using Model;


namespace View
{
	public class CharacterView : MonoBehaviour
	{
		public Character character;
		
		
		public void Init(Character character)
		{
			this.character = character;
			
			character.actionCardPulledEvent += OnActionCardPulled;
			character.actionResultCardPulledEvent += OnResultCardPulled;
			character.atbGaugeFullEvent += OnATBGaugeFull;
			
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
}