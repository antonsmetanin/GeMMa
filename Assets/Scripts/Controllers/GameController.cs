using System;
using System.Collections.Generic;


namespace Model
{
	
	public class GameController
	{
		public Action<GameState> gameStateChangedEvent;
		
		Battle battle = new Battle();
		
		public void SetupBattle(List<Character> allies, List<Character> enemies)
		{
			battle.SetupBattle(allies, enemies);
			
			if (gameStateChangedEvent != null) {
				gameStateChangedEvent(battle);
			}
		}
		
		public void Update()
		{
			battle.Update();
		}
	}
	
}
