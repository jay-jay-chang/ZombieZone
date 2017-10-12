using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class power : MonoBehaviour {

	public Text powerText;

	public float PowerProduceInterval = 1.0f;
	public int PowerProduce = 15;
	public float PowerDecayRate = 0.01f;
	private float powerProduceTime = 0f;

	public float PowerComsumeInterval = 1.0f;
	public int PowerComsume = 10;
	private float powerComsumeTime = 0f;

	private int amount;

	public int Amount
	{
		set { amount = value >= 0 ? value : 0; powerText.text = amount.ToString(); }
		get { return amount; }
	}

	public void Produce(int powerHealthMax, int powerHealth)
	{
		int am = Mathf.CeilToInt (((float)powerHealth) / powerHealthMax * PowerProduce);
		Amount += am;
		gameLogic.Instance.OnPowerProduced (am);
	}

	public void Consume()
	{
		if (Amount >= PowerComsume)
		{
			Amount -= PowerComsume;
			gameLogic.Instance.OnPowerConsume (true);
		}
		else
		{
			gameLogic.Instance.OnPowerConsume (false);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		powerProduceTime += Time.deltaTime;
		if (powerProduceTime >= PowerProduceInterval)
		{
			powerProduceTime -= PowerProduceInterval;
			Produce (gameLogic.Instance.PowerHealthMax, gameLogic.Instance.PowerHealth);
			//Power += Mathf.CeilToInt(((float)PowerHealth) / PowerHealthMax * PowerProduce);
			//PowerHealth -= Mathf.CeilToInt(PowerDecayRate * PowerHealthMax);
		}

		powerComsumeTime += Time.deltaTime;
		if (powerComsumeTime >= PowerComsumeInterval)
		{
			powerComsumeTime -= PowerComsumeInterval;
			Consume ();
		}
	}
}
