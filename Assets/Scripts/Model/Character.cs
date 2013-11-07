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
	public struct Position
	{
		public int x;
		public int y;
	}
	
	
	[System.Serializable]
	public class Cost
	{
		public ParameterType parameter;
		public int costValue;
	}
	
	
	public class RandomController
	{
		static Random random = new Random();
		
		public static int GetRandom(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}
	}
	
	
//	public class TimedValue<T>
//	{
//		bool finished;
//		Action<TimedValue<T>> finishedEvent;
//		
//		long startTime;
//		long endTime;
//		
//		T startValue;
//		T endValue;
//		
//		public void SetTo(T val)
//		{
//			startValue = endValue = val;
//		}
//		
//		
//		public T Value
//		{
//			get { 
//				float t = (TimeController.CurrentTime - startTime) / (endTime - startTime);
//				return startValue * (1.0f - t) + endValue * t;
//			}
//		}
//		
//		
//		public void Update()
//		{
//			if (!finished && TimeController.CurrentTime >= endTime) {
//				finished = true;
//				
//				if (finishedEvent != null) {
//					finishedEvent(this);
//				}
//			}
//		}
//	}
	
	
	
	
	
	[System.Serializable]
	public class ATBGauge
	{
		public float ATBValue {
			get {
				return (float)(TimeController.CurrentTime - startTime) / (float)(endTime - startTime);
			}
		}
		
		public Action fullEvent;
		
		public long startTime;
		public long endTime;
		public float speed;
		bool full;
		
		
		public void Clear()
		{
			startTime = TimeController.CurrentTime;
			endTime = startTime + (int)(10000.0f * 1000.0f / speed);
			
			full = false;
		}
		
		public void Update()
		{
			if (!full && TimeController.CurrentTime >= endTime) {
				if (fullEvent != null) {
					fullEvent();
				}
				
				full = true;
			}
		}
	}
	
	
	[System.Serializable]
	public class Character
	{
		public CharacterData staticData;
		
		public static int parametersLength = Enum.GetNames(typeof(ParameterType)).Length;
		
		public int[] currentParameters = new int[parametersLength];
		
		public List<ActionCardData> actionDeck = new List<ActionCardData>();
		public List<ResultCardData> actionResultDeck = new List<ResultCardData>();
		
		public Position position = new Position();
		public ATBGauge atbGauge = new ATBGauge();
		
		public bool dead;
		
		public Action<Character> atbGaugeFullEvent;
		public Action<Character, ActionCardData> actionCardPulledEvent;
		public Action<Character, ResultCardData> actionResultCardPulledEvent;
		public Action<Character, Character> targetSelectedEvent;
		
		public ActionCardData pendingActionCard;
		public ResultCardData pendingResultCard;
		public Character selectedTarget;
		
		
		public void Init(CharacterData staticData)
		{
			this.staticData = staticData;
			
			atbGauge.speed = staticData.atbSpeed;
			atbGauge.fullEvent += OnATBGaugeFull;
			
			for (int i = 0; i < parametersLength; ++i) {
				currentParameters[i] = staticData.defaultParameters[i];
			}
		}
		
		
		void OnATBGaugeFull()
		{
			if (atbGaugeFullEvent != null) {
				atbGaugeFullEvent(this);
			}
		}
		
		
		public void SetupNewBattle()
		{
			actionDeck.Clear();
			
			foreach (ActionCardData card in staticData.possibleActionCards) {
				actionDeck.Add(card);
			}
			
			foreach (ResultCardData card in staticData.possibleResultCards) {
				actionResultDeck.Add(card);
			}
			
			atbGauge.Clear();
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
					target.currentParameters[i] += pendingActionCard.damageValues[i];
				} else {
					target.currentParameters[i] -= pendingActionCard.damageValues[i];
				}
				
				if (target.currentParameters[i] <= 0) {
					target.currentParameters[i] = 0;
				}
			}
			
			if (target.currentParameters[(int)ParameterType.HealthPoints] == 0) {
				target.dead = true;
			}
		}
		
		
		public void Update()
		{
			atbGauge.Update();
			
			
		}
	}

}
