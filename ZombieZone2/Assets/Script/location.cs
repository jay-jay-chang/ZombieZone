using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class location : MonoBehaviour, IPointerDownHandler {
	public string 	id;
	public Image  	ui;
	public System.Action<location> OnlocSelect;

	public void OnPointerDown(PointerEventData ped) {
		if (OnlocSelect != null) {
			OnlocSelect (this);
		}
	}

	void Awake(){
		ui = GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
