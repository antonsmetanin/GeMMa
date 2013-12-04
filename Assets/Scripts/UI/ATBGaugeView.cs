using UnityEngine;
using System.Collections;
using Model;


public class ATBGaugeView : MonoBehaviour
{
	public UISprite allyPointer;
	public UISprite enemyPointer;
	public Transform atbRuler;
	public CharacterView characterView;
	public ATBGauge gauge;
	
	
	public void Init(CharacterView characterView, Transform atbRuler)
	{
		transform.localScale = Vector3.one;
		this.characterView = characterView;
		gauge = characterView.character.atbGauge;
		this.atbRuler = atbRuler;
	}
	
	
	void Update()
	{
		if (characterView != null) {
			bool isAlly = characterView.character.side == BattleSide.Left;
			
			allyPointer.gameObject.SetActive(isAlly);
			enemyPointer.gameObject.SetActive(!isAlly);
			
			transform.localPosition = new Vector3((gauge.ATBValue - 0.5f) * atbRuler.localScale.x, -40.0f, 0.0f);
		}
	}
}
