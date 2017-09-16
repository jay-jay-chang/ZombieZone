using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterLocationPanel : MonoBehaviour {

	public System.Action OnEnterLocDel;
	public Text desc;

	public void OnEnterLoc(){
		hide();
		if (OnEnterLocDel != null) {
			OnEnterLocDel ();
		}
	}

	public void show(string desc){
		this.desc.text = desc;
		this.gameObject.SetActive (true);
	}

	public void hide(){
		this.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		OnEnterLocDel += gameLogic.Instance.EnterLocationStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
