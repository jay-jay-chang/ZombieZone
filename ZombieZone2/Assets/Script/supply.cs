using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supply : MonoBehaviour {

	public int Amount
	{
		get { return amount; }
	}

	int amount = 5;

	public void Gather()
	{
		gameLogic.Instance.GatherSupply (Amount);
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
