using UnityEngine;
using System.Collections;
using Model;


public class ATBGaugeController : MonoBehaviour
{
	public ATBGaugeView atbGaugeSource;
	
	
	void Start()
	{
		Events.characterViewCreatedEvent += OnCharacterViewCreated;
	}
	
	
	void OnCharacterViewCreated(CharacterView characterView)
	{
		ATBGaugeView atbGaugeView = GameObject.Instantiate(atbGaugeSource) as ATBGaugeView;
		atbGaugeView.Init(characterView);
		atbGaugeView.transform.parent = transform;
		atbGaugeView.transform.localScale = Vector3.one;
		atbGaugeView.transform.localPosition = Vector3.one * 20.0f;
	}
}
