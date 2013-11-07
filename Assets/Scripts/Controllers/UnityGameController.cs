using UnityEngine;
using System.Collections.Generic;
using Model;


public class UnityGameController : MonoBehaviour
{
	static UnityGameController instance;
	
	public GameController gameController = new GameController();
	
	public List<CharacterData> allies;
	public List<CharacterData> enemies;
	
	public static GameController GameController { get { return Instance.gameController; } }
	
	bool initialized;
	
	
	public static UnityGameController Instance {
		get {
			if (instance == null) {
				GameObject go = GameObject.Find("Game Controller");
				instance = go.GetComponent<UnityGameController>();
			}
			
			return instance;
		}
	}
	
	
	void Update()
	{
		if (!initialized && Input.GetKey(KeyCode.Space)) {
			List<Character> allyModels = new List<Character>();
			List<Character> enemyModels = new List<Character>();
			
			foreach (CharacterData data in allies) {
				Character character = new Character();
				character.Init(data);
				allyModels.Add(character);
			}
			
			foreach (CharacterData data in enemies) {
				Character character = new Character();
				character.Init(data);
				enemyModels.Add(character);
			}
			
			gameController.SetupBattle(allyModels, enemyModels);
			
			initialized = true;
		}
	}
}
