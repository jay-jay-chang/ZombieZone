using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZombieView : MonoBehaviour, IPointerDownHandler {

	public event System.Action<ZombieAI> OnZombieSelect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData ped) 
	{
		//Vector2 pos = mouseToWorldPos (ped.position);
		//move = move + (-pos.x - move);
		if (OnZombieSelect != null) {
			OnZombieSelect(this.transform.parent.GetComponent<ZombieAI>());
		}
	}
}
