using UnityEngine;
using System.Collections;
using Model;


public class ViewController : MonoBehaviour
{
	public BattleView battleView;
	
	void Start()
	{
		UnityGameController.GameController.gameStateChangedEvent += OnGameStateChanged;
		
//		battleView.gameObject.SetActive(false);
	}
	
	
	void OnGameStateChanged(GameState state)
	{
		if (state is Battle) {
			battleView.Init(state as Battle);
//			battleView.gameObject.SetActive(true);
		}
	}
}
