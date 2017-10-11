using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water : MonoBehaviour {

    public Text waterText;

	public float WaterProduceStep = 0.1f;
	public int WaterProduce = 1;
	public int WaterProduced = 0;
	private float waterProduceProcess;

    private int amount;

    public int Amount
    {
        set { amount = value >= 0 ? value : 0; waterText.text = amount.ToString(); }
        get { return amount; }
    }

    public void Produce()
    {
        waterProduceProcess += WaterProduceStep;
        if (waterProduceProcess >= 1.0f)
        {
            waterProduceProcess -= 1.0f;
            WaterProduced += WaterProduce;
            gameLogic.Instance.OnWaterProduced(WaterProduced);
        }
    }

    public void Gather()
    {
        if (WaterProduced > 0)
        {
            gameLogic.Instance.OnGatherWaterDel(WaterProduced);
            Amount += WaterProduced;
            WaterProduced = 0;
            gameLogic.Instance.OnWaterProduced(WaterProduced);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
