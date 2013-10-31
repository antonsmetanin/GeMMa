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
		PowePoints
	}
	
	
	public enum TargetMode
	{
		Single,
		Global,
		Superglobal,
		Radical,
		SelfPlusOpponents,
		FrontRow,
		Line
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
		public bool isPositive;
		public List<Cost> costs;
		public List<ParameterType> requiredParameters;
	}
	
	
	[System.Serializable]
	public class ResultCard
	{
		
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
		public int healthPoints;
		public int manaPoints;
		public int powerPoints;
		
		public List<ActionCard> possibleActionCards;
		public List<ResultCard> possibleResultCards;
		
		public List<ActionCard> actionDeck;
		public List<ResultCard> actionResultDeck;
		
		public Position position;
		public ATBGauge atbGauge;
		
		
		public Action<Character> atbGaugeFullEvent;
		public Action<Character, ActionCard> actionCardPulled;
		public Action<Character, ResultCard> actionResultCardPulled;
		
		
		public ActionCard pendingActionCard;
		public ResultCard pendingResultCard;
		
		
		
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
			
			if (actionCardPulled != null) {
				
			}
		}
		
		
		public void PullResultCard()
		{
			pendingResultCard = actionResultDeck[RandomController.GetRandom(0, actionResultDeck.Count)];
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
