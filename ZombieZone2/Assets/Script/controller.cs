using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class controller : MonoBehaviour, IPointerDownHandler{

	public CanvasScaler refScaler;
	public GameObject controlTarget;
	public float speed = 1.0f;
	public bool y_axis_lock = false;
	public GameObject GainResFXRef;
	public Transform FoodPosition;
	public Transform WaterPosition;

	public Text WaterText;
	public Text FoodText;

	public Slider PowerHP;
	public Image PowerHpBar;
	Vector2 target;

	static Vector2 getNormPos(Vector2 pos)
	{
		float w = Screen.width;
		float h = Screen.height;
		return new Vector2 ( ((float)pos.x - w * 0.5f)/w, ((float)pos.y - h * 0.5f)/h);
	}

    Vector2 mouseToWorldPos(Vector2 mousePos)
	{
		float w = Screen.width;
		float h = Screen.height;

		return new Vector2 ( ((float)mousePos.x - w * 0.5f)/w*refScaler.referenceResolution.x, ((float)mousePos.y - h * 0.5f)/h*refScaler.referenceResolution.y );
	}
		

	public void OnPointerDown(PointerEventData ped) 
	{
		Vector2 pos = mouseToWorldPos (ped.position);
		//Debug.Log ("clickPos = " + pos.x + "," + pos.y);
		target = pos;
	}

	// Use this for initialization
	void Start () {
		if (controlTarget != null)
		{
			target = new Vector2 (controlTarget.transform.localPosition.x, controlTarget.transform.localPosition.y);
		}
		gameLogic.Instance.GainFoodDel += OnGainFood;
		gameLogic.Instance.GainWaterDel += OnGainWater;
		gameLogic.Instance.WaterProducedDel += OnProduceWater;
		gameLogic.Instance.FoodProducedDel += OnProduceFood;
		gameLogic.Instance.PowerHPChangeDel += OnPowerHpChange;
	}
	
	// Update is called once per frame
	void Update () {
		float fx = target.x - controlTarget.transform.localPosition.x;
		float fy = target.y - controlTarget.transform.localPosition.y;
		if (y_axis_lock) 
		{
			fy = 0;
		}

		if (Mathf.Abs(fx) < 1f && Mathf.Abs(fy) < 1f) 
		{
			return;
		}
		float factor = speed / Mathf.Sqrt (fx * fx + fy * fy);

		//Debug.Log (factor * fx + "," + factor * fy);
		Vector3 newpos = new Vector3(controlTarget.transform.localPosition.x + factor * fx, controlTarget.transform.localPosition.y + factor * fy, controlTarget.transform.localPosition.z);
		controlTarget.transform.localPosition = newpos;
	}

	void OnGainFood(int amount){
		ShowGainResFX (FoodPosition.localPosition, "Food + " + amount);
	}

	void OnGainWater(int amount){
		ShowGainResFX (WaterPosition.localPosition, "Water + " + amount);
	}

	void OnProduceWater(int amount){
		WaterText.text = "Water(" + amount.ToString() + ")";
	}

	void OnProduceFood(int amount){
		FoodText.text = "Food(" + amount.ToString() + ")";
	}

	void ShowGainResFX(Vector3 pos, string context){
		GameObject fx = (GameObject)GameObject.Instantiate(GainResFXRef);
		fx.transform.SetParent(this.transform);
		fx.transform.localScale = Vector3.one;
		fx.transform.localPosition = pos;
		fx.GetComponentInChildren<GainResFX> ().Show (context);
	}

	void OnPowerHpChange(int amount){
		float v = (float)amount / (float)gameLogic.Instance.PowerHealthMax;
		if (v > 0.8f) {
			PowerHpBar.color = Color.green;
		} else if (v > 0.4f) {
			PowerHpBar.color = Color.yellow;
		} else {
			PowerHpBar.color = Color.red;
		}
		PowerHP.value = v;
	}
}
