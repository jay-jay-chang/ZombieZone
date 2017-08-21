using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAnchor : MonoBehaviour {

	public Vector2 x_axis_limit;
	public Transform target;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = this.gameObject.transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = target.transform.position + offset;
		Vector3 localPos = this.gameObject.transform.localPosition;
		if(localPos.x > x_axis_limit.x){
			localPos.x = x_axis_limit.x;
		}
		else if(localPos.x < x_axis_limit.y){
			localPos.x = x_axis_limit.y;
		}
		this.transform.localPosition = localPos;
	}
}
