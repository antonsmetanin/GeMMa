using UnityEngine;
using System.Collections.Generic;
using Model;


public class UpdateController : MonoBehaviour
{
	void Update()
	{
		UnityGameController.GameController.Update();
	}
}
