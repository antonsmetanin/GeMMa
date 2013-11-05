using UnityEngine;
using System.Collections;
using Model;


public class ATBGaugeView : MonoBehaviour
{
	public UISlider slider;
	public CharacterView characterView;
	public ATBGauge gauge;
	
	
	public void Init(CharacterView characterView)
	{
		this.characterView = characterView;
		gauge = characterView.character.atbGauge;
	}
	
	
	void Update()
	{
		if (characterView != null) {
			Vector3 pos = Camera.main.WorldToScreenPoint(characterView.transform.position + Vector3.up * 2.0f);
			pos.z = 0.0f;
			transform.localPosition = pos;
			
			slider.sliderValue = gauge.ATBValue;
		}
	}
}
