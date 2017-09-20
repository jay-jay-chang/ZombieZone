using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameLogic : MonoBehaviour {

	private static gameLogic _gameLogic;
	public static gameLogic Instance {
		get{  
			return _gameLogic;
		}
	}

	public Text foodText;
	public Text waterText;
	public Text powerText;

	public sceneController sc;

	public float FoodProduceStep = 0.1f;
	public int FoodProduce = 1;
	public int FoodProduced = 0;
	private float FoodProduceProcess;

	public float WaterProduceStep = 0.1f;
	public int WaterProduce = 1;
	public int WaterProduced = 0;
	private float WaterProduceProcess;

	public float powerProduceInterval = 1.0f;
	public int PowerProduce = 15;
	public float PowerDecayRate = 0.01f;
	private float powerProduceTime = 0f;
	public int PowerHealthMax;
	private int powerHealth = 100;
	public int PowerHealth{
		set{ powerHealth = value >= 0 ? value : 0; OnPowerHPChange (PowerHealth); }
		get{ return powerHealth; }
	}

	public float powerComsumeInterval = 1.0f;
	public int PowerComsume = 10;
	private float powerComsumeTime = 0f;

	private int food;
	private int water;
	private int power;


	public int Food{
		set{ food = value >= 0 ? value : 0; foodText.text = food.ToString(); }
		get{ return food; }
	}
		
	public int Water{
		set{ water = value >= 0 ? value : 0; waterText.text = water.ToString();}
		get{ return water; }
	}

	public int Power{
		set{ power = value >= 0 ? value : 0; powerText.text = power.ToString();}
		get{ return power; }
	}

	void Awake() {
		_gameLogic = this;
	}

	// Use this for initialization
	void Start () {
		PowerHealthMax = PowerHealth;
	}
	
	// Update is called once per frame
	void Update () {
		powerProduceTime += Time.deltaTime;
		if(powerProduceTime >= powerProduceInterval){
			powerProduceTime -= powerProduceInterval;
			Power += Mathf.CeilToInt(((float)PowerHealth)/PowerHealthMax*PowerProduce);
			PowerHealth -= Mathf.CeilToInt( PowerDecayRate * PowerHealthMax );
		}

		powerComsumeTime += Time.deltaTime;
		if (powerComsumeTime >= powerComsumeInterval) {
			powerComsumeTime -= powerComsumeInterval;
			if (Power >= PowerComsume) {
				Power -= PowerComsume;
				sc.IsLight = true;

				FoodProduceProcess += FoodProduceStep;
				if(FoodProduceProcess >= 1.0f){
					FoodProduceProcess -= 1.0f;
					FoodProduced += FoodProduce;
					OnFoodProduced (FoodProduced);
				}

				WaterProduceProcess += WaterProduceStep;
				if(WaterProduceProcess >= 1.0f){
					WaterProduceProcess -= 1.0f;
					WaterProduced += WaterProduce;
					OnWaterProduced (WaterProduced);
				}
			} else {
				sc.IsLight = false;
			}
		}
	}

	public void OnEnterHome(){
	}

	public void OnPowerHPChange(int amount){
		if (PowerHPChangeDel != null) {
			PowerHPChangeDel(amount);
		}
	}
	public System.Action<int> PowerHPChangeDel;

	public void OnFoodProduced(int amount){
		if (FoodProducedDel != null) {
			FoodProducedDel(amount);
		}
	}
	public System.Action<int> FoodProducedDel;

	public void OnWaterProduced(int amount){
		if (WaterProducedDel != null) {
			WaterProducedDel(amount);
		}
	}
	public System.Action<int> WaterProducedDel;

	public void GainFood(){
		if (GainFoodDel != null && FoodProduced > 0) {
			GainFoodDel(FoodProduced);
			Food += FoodProduced;
			FoodProduced = 0;
			OnFoodProduced (FoodProduced);
		}
	}
	public System.Action<int> GainFoodDel;

	public void GainWater(){
		if (GainWaterDel != null && WaterProduced > 0) {
			GainWaterDel(WaterProduced);
			Water += WaterProduced;
			WaterProduced = 0;
			OnWaterProduced (WaterProduced);
		}
	}
	public System.Action<int> GainWaterDel;

	public void ShowMap(){
		if (ShowMapDel != null) {
			ShowMapDel();
		}
	}
	public System.Action ShowMapDel;

	public void MapTravelStart(int interval){
		if (MapTravelStartDel != null) {
			MapTravelStartDel (interval);
		}
	}

	public System.Action<int> MapTravelStartDel;

	public void MapTravelEnd(){
		if (MapTravelEndDel != null) {
			MapTravelEndDel ();
		}
	}

	public System.Action MapTravelEndDel;

	public void EnterLocationStart(){
		if (EnterLocationStartDel != null) {
			EnterLocationStartDel ();
		}
	}

	public System.Action EnterLocationStartDel;

	public void EnterLocationEnd(){
		if (EnterLocationEndDel != null) {
			EnterLocationEndDel ();
		}
	}

	public System.Action EnterLocationEndDel;
}
