using UnityEngine;
using System.Collections;
using Model;


public class ATBGaugeController : MonoBehaviour
{
	public ATBGaugeView atbGaugeSource;
	public ParametersView parametersViewSource;
	
	
	void Start()
	{
		Events.characterViewCreatedEvent += OnCharacterViewCreated;
	}
	
	
	void OnCharacterViewCreated(CharacterView characterView)
	{
		ATBGaugeView atbGaugeView = GameObject.Instantiate(atbGaugeSource) as ATBGaugeView;
		atbGaugeView.transform.parent = transform;
		atbGaugeView.transform.localScale = Vector3.one;
		atbGaugeView.Init(characterView);
		
		ParametersView parametersView = GameObject.Instantiate(parametersViewSource) as ParametersView;
		parametersView.transform.parent = transform;
		parametersView.transform.localScale = Vector3.one;
		parametersView.Init(characterView);
	}
}
