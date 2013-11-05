using UnityEngine;
using System.Collections.Generic;

namespace Model
{
	public class CharacterData : ScriptableObject
	{
		public string characterName;
		
		public int[] defaultParameters = new int[Model.Character.parametersLength];
		public int[] maxParameters = new int[Model.Character.parametersLength];
		
		public float atbSpeed;
		
		public List<ActionCardData> possibleActionCards;
		public List<ResultCardData> possibleResultCards;
	}
}