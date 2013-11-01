using System;
using System.Collections.Generic;


namespace Model {
	
	public enum ElementType
	{
		
	}
	
	
	public enum ParameterType
	{
		HealthPoints,
		ManaPoints,
		PowerPoints
	}
	
	
	public enum TargetMode
	{
		Single,
		Global,
		Superglobal,
		Radical,
		SelfPlusOpponents,
		FrontRow,
		StraightLine
	}
	
	
	public enum SkillType
	{
		Simple,
		Special,
		Powerful,
		Inseed
	}
	
	
	public enum SkillSubtype
	{
		Physical,
		Magical,
		PhysMagical,
		MagiPhysical,
		PhysWeapon
	}
	
	
	public enum ResultType
	{
		OK,
		Miss,
		Double
	}
	
	
	[System.Serializable]
	public class Position
	{
		int x;
		int y;
	}
	
	
	[System.Serializable]
	public class Cost
	{
		public ParameterType parameter;
		public int costValue;
	}
	
	
	[System.Serializable]
	public class ActionCard
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
	
	
	[System.Serializable]
	public class ResultCard
	{
		public ResultType type;
	}
	
	
	public class RandomController
	{
		static Random random = new Random();
		
		public static int GetRandom(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}
	}
	
	
	public class TimeController
	{
		public static long CurrentTime {
			get {
				return System.DateTime.Now.Ticks;
			}
		}
	}
	
	
	[System.Serializable]
	public class ATBGauge
	{
		public float atbValue;
		public long startTime;
		public long endTime;
		
		public float speed = 0.1f;
		
		public void Clear()
		{
			startTime = TimeController.CurrentTime;
			endTime = startTime + (int)(1.0f / speed);
		}
	}
	
	
	[System.Serializable]
	public class Character
	{
		public static int parametersLength = Enum.GetNames(typeof(ParameterType)).Length;
		
		public string characterName;
		
		public int[] parameters = new int[parametersLength];
		
		public List<ActionCard> possibleActionCards;
		public List<ResultCard> possibleResultCards;
		
		public List<ActionCard> actionDeck;
		public List<ResultCard> actionResultDeck;
		
		public Position position;
		public ATBGauge atbGauge;
		
		public bool dead;
		
		
		public Action<Character> atbGaugeFullEvent;
		public Action<Character, ActionCard> actionCardPulledEvent;
		public Action<Character, ResultCard> actionResultCardPulledEvent;
		public Action<Character, Character> targetSelectedEvent;
		
		
		public ActionCard pendingActionCard;
		public ResultCard pendingResultCard;
		public Character selectedTarget;
		
		
		public void SetupNewBattle()
		{
			actionDeck.Clear();
			
			foreach (ActionCard card in possibleActionCards) {
				actionDeck.Add(card);
			}
			
			foreach (ResultCard card in possibleResultCards) {
				actionResultDeck.Add(card);
			}
		}
		
		
		public void PullActionCard()
		{
			pendingActionCard = actionDeck[RandomController.GetRandom(0, actionDeck.Count)];
			
			if (actionCardPulledEvent != null) {
				actionCardPulledEvent(this, pendingActionCard);
			}
		}
		
		
		public void PullResultCard()
		{
			pendingResultCard = actionResultDeck[RandomController.GetRandom(0, actionResultDeck.Count)];
			
			if (actionResultCardPulledEvent != null) {
				actionResultCardPulledEvent(this, pendingResultCard);
			}
		}
		
		
		public void SelectTarget(Character character)
		{
			selectedTarget = character;
			
			if (targetSelectedEvent != null) {
				targetSelectedEvent(this, character);
			}
		}
		
		
		public void ConfirmMove()
		{
			if (pendingActionCard == null || pendingResultCard == null) {
				return;
			}
			
			if (pendingActionCard.targetMode == TargetMode.Single) {
				if (selectedTarget == null) {
					return;
				}
				
				if (pendingResultCard.type == ResultType.Miss) {
					return;
				} else if (pendingResultCard.type == ResultType.OK) {
					ApplyCardOnTarget(selectedTarget);
				}
			} else if (pendingActionCard.targetMode == TargetMode.Single) {
				
			}
		}
		
		
		public void ApplyCardOnTarget(Character target)
		{
			for (int i = 0; i < Character.parametersLength; ++i) {
				if (pendingActionCard.isPositive) {
					target.parameters[i] += pendingActionCard.damageValues[i];
				} else {
					target.parameters[i] -= pendingActionCard.damageValues[i];
				}
				
				if (target.parameters[i] <= 0) {
					target.parameters[i] = 0;
				}
			}
			
			if (target.parameters[(int)ParameterType.HealthPoints] == 0) {
				target.dead = true;
			}
		}
		
		
		public void Update()
		{
			long currentTime = TimeController.CurrentTime;
			
			if (currentTime >= atbGauge.endTime) {
				if (atbGaugeFullEvent != null) {
					atbGaugeFullEvent(this);
				}
			}
		}
	}

}
