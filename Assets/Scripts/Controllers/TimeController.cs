using UnityEngine;
using System.Collections;


public class TimeController
{
	public static long CurrentTime {
		get {
			return System.DateTime.Now.Ticks;
		}
	}
}	