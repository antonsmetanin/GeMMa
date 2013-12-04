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
	
	
	public enum BattleSide
	{
		Left,
		Right
	}
	
	
	[System.Serializable]
	public struct Position
	{
		public int x;
		public int y;
		
		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
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
				if (full) {
					return 1.0f;
				} else {
					return (float)(TimeController.CurrentTime - startTime) / (float)(endTime - startTime);
				}
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
		
		public List<ActionCard> actionDeck = new List<ActionCard>();
		public List<ResultCard> resultDeck = new List<ResultCard>();
		
		public Position position;
		public ATBGauge atbGauge = new ATBGauge();
		
		public bool dead;
		
		public Action<Character> atbGaugeFullEvent;
		public Action<Character, ActionCard> actionCardPulledEvent;
		public Action<Character, ResultCard> resultCardPulledEvent;
		public Action<Character, Character> targetSelectedEvent;
		
		public ActionCard pendingActionCard;
		public ResultCard pendingResultCard;
		public Character selectedTarget;
		public BattleSide side;
		
		
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
		
		
		public void SetupNewBattle(BattleSide side, Position position)
		{
			this.side = side;
			this.position = position;
			
			actionDeck.Clear();
			
			foreach (ActionCardData cardData in staticData.possibleActionCards) {
				ActionCard card = new ActionCard();
				card.Init(cardData, this);
				actionDeck.Add(card);
			}
			
			foreach (ResultCardData cardData in staticData.possibleResultCards) {
				ResultCard card = new ResultCard();
				card.Init(cardData, this);
				resultDeck.Add(card);
			}
			
			atbGauge.Clear();
		}
		
		
		public void PullRandomActionCard()
		{
			PullActionCard(actionDeck[RandomController.GetRandom(0, actionDeck.Count)]);
		}
		
		
		public void PullActionCard(ActionCard card)
		{
			if (!actionDeck.Contains(card)) {
				UnityEngine.Debug.LogError("Hey! This is not my card!");
			}
			
			pendingActionCard = card;
			
			if (actionCardPulledEvent != null) {
				actionCardPulledEvent(this, pendingActionCard);
			}
		}
		
		
		public void PullRandomResultCard()
		{
			PullResultCard(resultDeck[RandomController.GetRandom(0, resultDeck.Count)]);
		}
		
		
		public void PullResultCard(ResultCard card)
		{
			pendingResultCard = resultDeck[RandomController.GetRandom(0, resultDeck.Count)];
			
			if (resultCardPulledEvent != null) {
				resultCardPulledEvent(this, pendingResultCard);
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
			
//			UnityEngine.Debug.Log("Confirming move");
			
			atbGauge.Clear();
			
			if (pendingActionCard.data.targetMode == TargetMode.Single) {
				if (selectedTarget == null) {
					return;
				}
				
				if (pendingResultCard.data.type == ResultType.Miss) {
					return;
				} else if (pendingResultCard.data.type == ResultType.OK) {
					ApplyCardOnTarget(selectedTarget);
				}
			} else if (pendingActionCard.data.targetMode == TargetMode.Single) {
				
			}
		}
		
		
		public void ApplyCardOnTarget(Character target)
		{
			UnityEngine.Debug.Log("Applying card to target");
			
			for (int i = 0; i < Character.parametersLength; ++i) {
				if (pendingActionCard.data.isPositive) {
					target.currentParameters[i] += pendingActionCard.data.damageValues[i];
				} else {
					target.currentParameters[i] -= pendingActionCard.data.damageValues[i];
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
