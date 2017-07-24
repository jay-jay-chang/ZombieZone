using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {
	public int id;

	public void onSectionLoad(GameObject instance){
		if (this.transform.childCount > 0) {
			for(int i=0; i<this.transform.childCount; ++i){
				GameObject oldSection = this.transform.GetChild(i).gameObject;
				Destroy (oldSection);
			}
		}
		GameObject newSection = (GameObject)Instantiate (instance);
		newSection.transform.parent = this.transform;
		newSection.transform.localPosition = Vector3.zero;
		newSection.transform.localScale = Vector3.one;
		Debug.Log ("section " + id + " loaded");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
