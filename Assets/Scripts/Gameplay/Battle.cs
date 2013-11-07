using System.Collections.Generic;
using System;


namespace Model
{
	public class Battle : GameState
	{
//		enum BattleState
//		{
//			
//		}
//		
//		BattleState state;
		
		public Action battleStartedEvent;
		
		public List<Character> allies;
		public List<Character> enemies;

		
		public void SetupBattle(List<Character> allies, List<Character> enemies)
		{
			this.allies = allies;
			this.enemies = enemies;
			
			for (int i = 0; i < allies.Count; ++i) {
				allies[i].position.x = -5;
				allies[i].SetupNewBattle();
			}
			
			for (int i = 0; i < enemies.Count; ++i) {
				enemies[i].position.x = 5;
				enemies[i].SetupNewBattle();
			}
			
			if (battleStartedEvent != null) {
				battleStartedEvent();
			}
		}
		
		
		public override void Update()
		{
			if (allies != null) {
				foreach (Character character in allies) {
					character.Update();
				}
			}
			
			if (enemies != null) {
				foreach (Character character in enemies) {
					character.Update();
				}
			}
		}
	}
}