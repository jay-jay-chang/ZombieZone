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

	public float powerProduceInterval = 1.0f;
	public int PowerProduce = 15;
	public float PowerDecayRate = 0.01f;
	private float powerProduceTime = 0f;
	private int PowerHealthMax;
	private int powerHealth = 100;
	public int PowerHealth{
		set{ powerHealth = value >= 0 ? value : 0;}
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
		Debug.Log (-11%5);
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
			} else {
				sc.IsLight = false;
			}
		}
	}

	public void OnEnterHome(){
	}

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
