using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrap : MonoBehaviour {

	public Text scrapText;

	private int amount;

	public int Amount
	{
		set { amount = value >= 0 ? value : 0; scrapText.text = amount.ToString(); }
		get { return amount; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
