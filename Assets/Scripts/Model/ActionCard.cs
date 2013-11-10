using UnityEngine;
using System.Collections;


namespace Model
{
	[System.Serializable]
	public class ActionCard
	{
		public ActionCardData data;
		
		public Character owner;
		
		
		public void Init(ActionCardData data, Character owner)
		{
			this.data = data;
			this.owner = owner;
		}
	}
}