﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapArrow : MonoBehaviour {

	public GameObject sGO;
	public GameObject eGO;

	public Vector3 start;
	public Vector3 end;

	public SpriteRenderer sRenderer;
	public float speed;

	float updateTime;

	void Start () {
		sRenderer.material.SetFloat ("_Speed", speed);
	}

	void OnModify(){
		Vector3 dir = end - start;
		makeLine (dir.magnitude);

		this.gameObject.transform.localPosition = new Vector3 (0.5f*(end.x + start.x), 0.5f*(end.y + start.y), this.gameObject.transform.localPosition.z);

		float angle = 0;
		if (dir.x >= 0) {
			if (dir.y < 0) {
				angle = 360f;
			}
		} else {
			angle = 180f;
			//			if (dir.y < 0) {
			//				angle = 180f;
			//			} else {
			//				angle = 180f;
			//			}
		}
		angle += Mathf.Atan (dir.y / dir.x) * 180f / Mathf.PI;
		this.gameObject.transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
		//Vector3 forward = dir * 10 / dir.magnitude;
		//this.gameObject.transform.localPosition = new Vector3 (end.x + forward.x, end.y + forward.y, this.gameObject.transform.localPosition.z);


	}

	public void setLine(GameObject s, GameObject e){
		start = s.transform.localPosition;
		//this.gameObject.transform.localPosition = new Vector3 (start.x, start.y, this.gameObject.transform.localPosition.z);
		end = e.transform.localPosition;
		OnModify ();
	}

//	public void setStart (GameObject go){
//		start = go.transform.localPosition;
//		this.gameObject.transform.localPosition = new Vector3 (start.x, start.y, 0);
//		//linerenderer.SetPosition (0, new Vector3(start.x, start.y, 0));
//		OnModify();
//	}
//
//	public void setEnd(GameObject go){
//		Vector3 dir = go.transform.localPosition - start;
//		dir *= (dir.magnitude - 60) / dir.magnitude;
//		end = start + new Vector3 (dir.x, dir.y, 0);
//		//linerenderer.SetPosition (1, new Vector3(end.x, end.y, 0));
//		OnModify();
//	}

	public void makeLine(float length){
		Vector2 unit = new Vector2( sRenderer.sprite.textureRect.width, sRenderer.sprite.textureRect.height);
		int num = Mathf.CeilToInt(length / sRenderer.sprite.textureRect.width);
		sRenderer.size = new Vector2 (sRenderer.sprite.textureRect.width / this.gameObject.transform.localScale.x * num, sRenderer.sprite.textureRect.height / this.gameObject.transform.localScale.y);
//		if (createdObject == null) {
//			createdObject = new List<GameObject> ();
//		}
//
//		Image image = mapArrowUnit.GetComponent<Image> ();
//		float curlength = image.rectTransform.rect.width;
//		int i = 0;
//		for (; curlength <= length; ++i) {
//			Debug.Log (createdObject.Count + "," + i );
//			GameObject unit = null;
//			if (createdObject.Count <= i) {
//				unit = (GameObject)GameObject.Instantiate (mapArrowUnit);
//				createdObject.Add (unit);
//			} else {
//				unit = createdObject [i];
//			}
//			if(!unit.activeSelf){
//				unit.SetActive (true);
//			}
//			unit.transform.SetParent (this.gameObject.transform);
//			unit.transform.localPosition = new Vector3 (image.rectTransform.rect.width*i, 0, 0);
//			unit.transform.localRotation = Quaternion.Euler(0, 0, 0);
//			unit.transform.localScale = Vector3.one;
//			curlength += image.rectTransform.rect.width;
//		}
//
//		for(;i < createdObject.Count; ++i){
//			createdObject[i].SetActive(false);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
