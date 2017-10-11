using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class food : MonoBehaviour {

	public Text foodText;

    public float FoodProduceStep = 0.1f;
    public int FoodProduce = 1;
    public int FoodProduced = 0;
    private float foodProduceProcess;
    private int amount;

    public int Amount
    {
        set { amount = value >= 0 ? value : 0; foodText.text = amount.ToString(); }
        get { return amount; }
    }

    public void Produce()
    {
        foodProduceProcess += FoodProduceStep;
        if (foodProduceProcess >= 1.0f)
        {
            foodProduceProcess -= 1.0f;
            FoodProduced += FoodProduce;
            gameLogic.Instance.OnFoodProduced(FoodProduced);
        }
    }

    public void Gather()
    {
        if (FoodProduced > 0)
        {
            gameLogic.Instance.OnGatherFood(FoodProduced);
            Amount += FoodProduced;
            FoodProduced = 0;
            gameLogic.Instance.OnFoodProduced(FoodProduced);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
