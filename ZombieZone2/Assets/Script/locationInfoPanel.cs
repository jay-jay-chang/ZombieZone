using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class locationInfoPanel : MonoBehaviour {

	public System.Action OnlocGo;
	public Text desc;

	public void OnLocGO(){
		hide();
		if (OnlocGo != null) {
			OnlocGo ();
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
