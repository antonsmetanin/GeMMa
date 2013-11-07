using UnityEngine;
using System.Collections.Generic;
using Model;


public class BattleView : MonoBehaviour
{
	public CharacterView characterViewSource;
	public Battlefield battlefieldSource;
	public ActionCardView cardSource;
	public Transform cardsContainer;
	
	List<ActionCardView> cards = new List<ActionCardView>();
	
	Battle battle;
	
	
	public void Init(Battle battle) {
		this.battle = battle;
		
		Battlefield battlefield = GameObject.Instantiate(battlefieldSource) as Battlefield;
		battlefield.transform.parent = transform;
		battlefield.transform.localPosition = Vector3.zero;
		battlefield.transform.localRotation = Quaternion.identity;
		
		foreach (Character ally in battle.allies) {
			CharacterView characterView = GameObject.Instantiate(characterViewSource) as CharacterView;
			characterView.Init(ally);
			ally.atbGaugeFullEvent += ATBGaugeFull;
		}
		
		foreach (Character enemy in battle.enemies) {
			CharacterView characterView = GameObject.Instantiate(characterViewSource) as CharacterView;
			characterView.Init(enemy);
		}
	}

	
	void ATBGaugeFull(Character character)
	{
		if (battle.allies.Contains(character)) {
			float cardWidth = 120.0f;
			float x = (float)(Screen.width / 2) - (cardWidth * (float)character.actionDeck.Count) * 0.5f;
			
			foreach (ActionCardData actionCard in character.actionDeck) {
				ActionCardView actionCardView = GameObject.Instantiate(cardSource) as ActionCardView;
				actionCardView.Init(actionCard);
				actionCardView.transform.parent = cardsContainer;
				actionCardView.transform.localPosition = new Vector3(x, 50.0f, 0.0f);
				
				actionCardView.openEvent += OnActionCardOpen;
				actionCardView.centeredEvent += OnActionCardCentered;
				cards.Add(actionCardView);
				
				x += cardWidth;
			}
		}
	}
	
	
	void OnActionCardOpen(ActionCardView actionCard)
	{
		foreach (ActionCardView card in cards) {
			card.clickable = false;
		}
		
		actionCard.MoveToTheCenter();
	}
	
	
	void OnActionCardCentered(ActionCardView actionCard)
	{
		
	}
}
