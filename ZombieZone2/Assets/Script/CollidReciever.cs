using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CollidEvent : UnityEvent<Collider2D>
{
}

public class CollidReciever : MonoBehaviour 
{
	public CollidEvent TriggerEnter2DEvent;
	public CollidEvent TriggerStay2DEvent;
	public CollidEvent TriggerExit2DEvent;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (TriggerEnter2DEvent != null) 
		{
			TriggerEnter2DEvent.Invoke(other);
		}
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (TriggerStay2DEvent != null) 
		{
			TriggerStay2DEvent.Invoke(other);
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (TriggerExit2DEvent != null) 
		{
			TriggerExit2DEvent.Invoke(other);
		}
	}
}
