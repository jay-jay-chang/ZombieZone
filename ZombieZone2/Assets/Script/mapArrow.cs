using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapArrow : MonoBehaviour {

	public float length;
	public GameObject mapArrowUnit;

	List < GameObject > createdObject;

	public void makeLine(float length){
		if (createdObject == null) {
			createdObject = new List<GameObject> ();
		}

		Image image = mapArrowUnit.GetComponent<Image> ();
		float curlength = image.rectTransform.rect.width;
		int i = 0;
		for (; curlength <= length; ++i) {
			Debug.Log (createdObject.Count + "," + i );
			GameObject unit = null;
			if (createdObject.Count <= i) {
				unit = (GameObject)GameObject.Instantiate (mapArrowUnit);
				createdObject.Add (unit);
			} else {
				unit = createdObject [i];
			}
			if(!unit.activeSelf){
				unit.SetActive (true);
			}
			unit.transform.SetParent (this.gameObject.transform);
			unit.transform.localPosition = new Vector3 (image.rectTransform.rect.width*i, 0, 0);
			unit.transform.localScale = Vector3.one;
			curlength += image.rectTransform.rect.width;
		}

		for(;i < createdObject.Count; ++i){
			createdObject[i].SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		makeLine (10000);
	}
	
	// Update is called once per frame
	void Update () {
		//makeLine (length);
	}
}
