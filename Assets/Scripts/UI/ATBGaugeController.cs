using UnityEngine;
using System.Collections;
using Model;


public class ATBGaugeController : MonoBehaviour
{
	public ATBGaugeView atbGaugeSource;
	public ParametersView parametersViewSource;
	public Transform atbRuler;
	public Transform atbContainer;
	
	
	void Start()
	{
		Events.characterViewCreatedEvent += OnCharacterViewCreated;
	}
	
	
	void OnCharacterViewCreated(CharacterView characterView)
	{
		ATBGaugeView atbGaugeView = GameObject.Instantiate(atbGaugeSource) as ATBGaugeView;
		atbGaugeView.transform.parent = atbContainer;
		atbGaugeView.Init(characterView, atbRuler);
		
		ParametersView parametersView = GameObject.Instantiate(parametersViewSource) as ParametersView;
		parametersView.transform.parent = transform;
		parametersView.transform.localScale = Vector3.one;
		parametersView.Init(characterView);
	}
}
