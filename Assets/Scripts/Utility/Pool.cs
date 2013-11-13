using UnityEngine;
using System.Collections.Generic;


public class Pool<T> where T : MonoBehaviour
{
	List<T> pool = new List<T>();
	
	T source;
	
	
	public void Init(T source)
	{
		this.source = source;
	}
	
	
	public T Get()
	{
		T obj = pool.Find(x => !x.gameObject.activeSelf);
		
		if (obj == null) {
			obj = GameObject.Instantiate(source) as T;
			
			pool.Add(obj);
		} else {
			obj.gameObject.SetActive(true);
		}
		
		return obj;
	}
}
