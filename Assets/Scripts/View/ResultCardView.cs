using UnityEngine;
using System.Collections;
using Model;


public class ResultCardView : CardView
{
	public ResultCard resultCard;
	
	public void Init(ResultCard resultCard)
	{
		this.resultCard = resultCard;
	}
}
