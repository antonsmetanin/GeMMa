using UnityEngine;
using System.Collections.Generic;


namespace Model
{
	
	public class ActionCardData : ScriptableObject
	{
		public string cardName;
		public bool isPositive;
		public List<Cost> costs;
		public List<ParameterType> requiredParameters;
		public TargetMode targetMode;
		public SkillType skillType;
		public SkillSubtype skillSubtype;
		public int[] damageValues = new int[Character.parametersLength];
	}
	
}