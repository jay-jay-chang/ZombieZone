﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

	public GameObject target;
	public float speed = 1;
	public GameObject axisBase;
	public float transition;
	public int hp = 5;

	public void OnDamage(int dmg){
		hp -= dmg;
		if(hp <= 0){
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	}

	void chase(float transition){
		float factor = transition > 0 ? -speed : speed;
		Vector3 newpos = new Vector3 (transform.localPosition.x + factor, transform.localPosition.y, transform.localPosition.z);
		transform.localPosition = newpos;
	}

	void attack(){
	}
	
	// Update is called once per frame
	void Update () {
		transition = axisBase.transform.localPosition.x + transform.localPosition.x - target.transform.localPosition.x;
		float dis = Mathf.Abs(transition);
		if (dis < 350) {
			if (dis > 20) {
				chase (transition);
			} else {
				attack ();
			}
		}
	}
}
