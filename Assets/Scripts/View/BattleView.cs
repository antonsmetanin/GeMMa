using UnityEngine;
using System.Collections.Generic;
using Model;


public class BattleView : MonoBehaviour
{
	public CharacterView characterViewSource;
	public Battlefield battlefieldSource;
	public ActionCardView actionCardSource;
	public ResultCardView resultCardSource;
	public Transform cardsContainer;
	
	List<ActionCardView> actionCards = new List<ActionCardView>();
	Pool<ActionCardView> actionCardsPool = new Pool<ActionCardView>();
	
	List<ResultCardView> resultCards = new List<ResultCardView>();
	Pool<ResultCardView> resultCardsPool = new Pool<ResultCardView>();
	
	ActionCardView chosenActionCard;
	ResultCardView chosenResultCard;
	
	Battle battle;
	
	
	public void Init(Battle battle)
	{
		this.battle = battle;
		
		Battlefield battlefield = GameObject.Instantiate(battlefieldSource) as Battlefield;
		battlefield.transform.parent = transform;
		battlefield.transform.localPosition = Vector3.zero;
		battlefield.transform.localRotation = Quaternion.identity;
		
		actionCardsPool.Init(actionCardSource);
		resultCardsPool.Init(resultCardSource);
		
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
		if (character.side == BattleSide.Left) {
//			float cardWidth = 10.0f;
			float cardWidth = 56f / (float)(character.actionDeck.Count - 1);
//			float x = (float)(Screen.width / 2) - (cardWidth * (float)character.actionDeck.Count) * 0.5f;
//			float x = -(cardWidth * (float)(character.actionDeck.Count - 1)) * 0.5f;
			float x = 28f;
			
			foreach (ActionCard actionCard in character.actionDeck) {
				ActionCardView actionCardView = actionCardsPool.Get();

				var pivot = new GameObject("Pivot");
				pivot.transform.parent = cardsContainer;
				pivot.transform.localPosition = new Vector3(0, -360f, 0);
				pivot.transform.localScale = Vector3.one;
				pivot.transform.localEulerAngles = new Vector3(0, 0, x);

				actionCardView.Init(actionCard);
				actionCardView.transform.parent = pivot.transform;
				actionCardView.transform.localPosition = new Vector3(0, 500.0f, 0.0f);
				actionCardView.transform.localRotation = Quaternion.identity;
				actionCardView.transform.localScale = Vector3.one * 300f;

				actionCardView.Reset();
				
				actionCardView.openEvent += OnActionCardOpen;
				actionCardView.centeredEvent += OnActionCardCentered;
				actionCards.Add(actionCardView);
				
				x -= cardWidth;
			}
		}
	}
	
	
	void OnActionCardOpen(CardView openCard)
	{
		ActionCardView actionCard = openCard as ActionCardView;
		
		foreach (ActionCardView card in actionCards) {
			card.clickable = false;
			card.actionCard.owner.PullActionCard(actionCard.actionCard);
		}
		
		actionCard.MoveToTheCenter();
	}
	
	
	void OnActionCardCentered(CardView centeredCard)
	{
		chosenActionCard = centeredCard as ActionCardView;
		
		while (actionCards.Count > 0) {
			ActionCardView card = actionCards[actionCards.Count - 1];
			
			if (card != chosenActionCard) {
				card.openEvent -= OnActionCardOpen;
				card.centeredEvent -= OnActionCardCentered;
				card.gameObject.SetActive(false);
			}
			
			actionCards.RemoveAt(actionCards.Count - 1);
		}
		
		Character character = chosenActionCard.actionCard.owner;
		
//		float cardWidth = 250.0f;
//		float x = (float)(Screen.width / 2) - (cardWidth * (float)character.resultDeck.Count) * 0.5f;

		float cardWidth = 56f / (float)(character.resultDeck.Count - 1);
		float x = 28f;
		
		foreach (ResultCard resultCard in character.resultDeck) {
			var pivot = new GameObject("Pivot");
			pivot.transform.parent = cardsContainer;
			pivot.transform.localPosition = new Vector3(0, -360f, 0);
			pivot.transform.localScale = Vector3.one;
			pivot.transform.localEulerAngles = new Vector3(0, 0, x);

			ResultCardView resultCardView = resultCardsPool.Get();
			resultCardView.Init(resultCard);
			resultCardView.transform.parent = pivot.transform;
			resultCardView.transform.localPosition = new Vector3(0, 500.0f, 0.0f);
			resultCardView.transform.localRotation = Quaternion.identity;
			resultCardView.transform.localScale = Vector3.one * 300f;
			
			resultCardView.Reset();
			
			resultCardView.openEvent += OnResultCardOpen;
			resultCardView.centeredEvent += OnResultCardCentered;
			resultCards.Add(resultCardView);
			
			x -= cardWidth;
		}
	}
	
	
	void OnResultCardOpen(CardView openCard)
	{
		ResultCardView resultCard = openCard as ResultCardView;
		
		foreach (ResultCardView card in resultCards) {
			card.clickable = false;
			
			card.resultCard.owner.PullResultCard(resultCard.resultCard);
		}
		
		resultCard.MoveToTheCenter();
	}
	
	
	void OnResultCardCentered(CardView centeredCard)
	{
		chosenResultCard = centeredCard as ResultCardView;
		
		Character character = chosenResultCard.resultCard.owner;
		
		character.PullResultCard(chosenResultCard.resultCard);
		character.SelectTarget(battle.enemies[0]);
		character.ConfirmMove();
		
		while (resultCards.Count > 0) {
			ResultCardView card = resultCards[resultCards.Count - 1];
			
			if (card != chosenResultCard) {
				card.openEvent -= OnResultCardOpen;
				card.centeredEvent -= OnResultCardCentered;
				card.gameObject.SetActive(false);
			}
			
			resultCards.RemoveAt(resultCards.Count - 1);
		}
		
		chosenActionCard.openEvent -= OnActionCardOpen;
		chosenActionCard.centeredEvent -= OnActionCardCentered;
		chosenActionCard.gameObject.SetActive(false);
		
		chosenResultCard.openEvent -= OnResultCardOpen;
		chosenResultCard.centeredEvent -= OnResultCardCentered;
		chosenResultCard.gameObject.SetActive(false);
	}
}
