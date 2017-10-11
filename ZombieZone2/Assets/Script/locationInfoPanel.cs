using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class locationInfoPanel : MonoBehaviour {

	public System.Action OnlocGo;
	public Text desc;
	public Text cost;
	public Button go;

	public void OnLocGO(){
		hide();
		if (OnlocGo != null) {
			OnlocGo ();
		}
	}

	public void show(location loc, int travelTime){
		this.desc.text = "it will take " + travelTime + " seconds";
		this.cost.text = "Water : " + loc.WaterCost;
		this.gameObject.SetActive (true);
		go.interactable = (gameLogic.Instance.Water.Amount < loc.WaterCost) ? false : true;
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
