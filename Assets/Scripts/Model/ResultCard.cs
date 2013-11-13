using UnityEngine;
using System.Collections;


namespace Model
{
	public class ResultCard : Card
	{
		public ResultCardData data;
		
		public Character owner;
		
		
		public void Init(ResultCardData data, Character owner)
		{
			this.data = data;
			this.owner = owner;
		}
	}
}