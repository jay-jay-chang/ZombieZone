using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itweenMisc : MonoBehaviour {

	public System.Action onComplete;

	Image target;

	public void itweenMoveElememt(Vector2 pos){

		if (target == null) {
			target = this.GetComponent<Image>();
		}
		target.rectTransform.anchoredPosition = pos;
	}

	public void OnComplete(){
		if (onComplete != null) {
			onComplete ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
