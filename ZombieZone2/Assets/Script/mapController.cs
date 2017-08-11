using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class mapController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public CanvasScaler refScaler;
	public GameObject controlTarget;
	public Vector2 mapSize;
	public GameObject home;
	public Image homeIcon;
	public Image curLoctionIcon;
	public arrowLine line;

	//ui
	public locationInfoPanel locInfoPanel;

	Image touchBoard;

	//boundery
	Vector2 xbound;
	Vector2 ybound;
	Vector2 prevPos;

	//locations
	Dictionary<string, location> locations = new Dictionary<string, location>();
	location currentLocation;
	location targetLocation;


	public void OnBeginDrag(PointerEventData ped) {
		Vector2 pos = UIUtility.mouseToWorldPos (refScaler.referenceResolution.x, refScaler.referenceResolution.y, ped.position);
		//Debug.Log ("downPos = " + pos.x + "," + pos.y);
		prevPos = pos;
	}

	public void OnDrag(PointerEventData ped) {
		Vector2 pos = UIUtility.mouseToWorldPos (refScaler.referenceResolution.x, refScaler.referenceResolution.y, ped.position);
		//Debug.Log ("dragPos = " + pos.x + "," + pos.y);
		float x = pos.x - prevPos.x + touchBoard.rectTransform.anchoredPosition.x;
		x = x > xbound.x ? xbound.x : x;
		x = x < xbound.y ? xbound.y : x;
		float y = pos.y - prevPos.y + touchBoard.rectTransform.anchoredPosition.y;
		y = y > ybound.x ? ybound.x : y;
		y = y < ybound.y ? ybound.y : y;
		touchBoard.rectTransform.anchoredPosition = new Vector2(x, y);
		prevPos = pos;
	}

	public void OnEndDrag(PointerEventData ped) {
		//Debug.Log ("upPos = " + pos.x + "," + pos.y);
		//target = pos;
	}

	// Use this for initialization
	void Start () {
		xbound = new Vector2((mapSize.x - refScaler.referenceResolution.x) * 0.5f, (-mapSize.x + refScaler.referenceResolution.x) * 0.5f);
		ybound = new Vector2((mapSize.y - refScaler.referenceResolution.y) * 0.5f, (-mapSize.y + refScaler.referenceResolution.y) * 0.5f);
		homeIcon.enabled = false;
		homeIcon.GetComponent<mapToHome> ().OnBackHome = OnBackHome;
		touchBoard = this.GetComponent<Image> ();
		initialLocations ();
		locInfoPanel.OnlocGo += OnLocGo;
		locInfoPanel.hide ();
		line.gameObject.SetActive (false);
		curLoctionIcon.gameObject.GetComponent<itweenMisc> ().onComplete += OnLocGoComplete;
	}

	void initialLocations(){
		foreach(location loc in GameObject.FindObjectsOfType<location> ()) {
			locations.Add(loc.id, loc);
			loc.OnlocSelect += OnLocSelect;
		}
		setCurrentLocation(locations["m0001"]);
	}

	void setCurrentLocation(location loc){
		currentLocation = loc;
		curLoctionIcon.rectTransform.anchoredPosition = new Vector2( loc.ui.rectTransform.anchoredPosition.x , loc.ui.rectTransform.anchoredPosition.y + 40f); 
	}

	void OnLocSelect(location loc){
		Debug.Log (loc.id + " is selected");
		Vector2 dis = new Vector2 (loc.ui.rectTransform.anchoredPosition.x - currentLocation.ui.rectTransform.anchoredPosition.x, loc.ui.rectTransform.anchoredPosition.y - currentLocation.ui.rectTransform.anchoredPosition.y);
		int time = (int)(dis.magnitude / 100f);
		locInfoPanel.show ("it will take " + time + " seconds");
		targetLocation = loc;
	}

	int getTravelTime(){
		Vector2 dis = new Vector2 (targetLocation.ui.rectTransform.anchoredPosition.x - currentLocation.ui.rectTransform.anchoredPosition.x, targetLocation.ui.rectTransform.anchoredPosition.y - currentLocation.ui.rectTransform.anchoredPosition.y);
		return (int)(dis.magnitude / 100f);
	}

	void OnLocGo(){
		line.setStart(currentLocation.gameObject);
		line.setEnd (targetLocation.gameObject);
		line.gameObject.SetActive (true);

		RectTransform s = currentLocation.gameObject.GetComponent<Image> ().rectTransform;
		RectTransform e = targetLocation.gameObject.GetComponent<Image> ().rectTransform;

		iTween.ValueTo (curLoctionIcon.gameObject, iTween.Hash (
			"from", s.anchoredPosition,
			"to", e.anchoredPosition,
			"time", (float)getTravelTime(),
			"onupdatetarget", curLoctionIcon.gameObject,
			"onupdate", "itweenMoveElememt",
			"oncomplete", "OnComplete"));
	}

	void OnLocGoComplete(){
		setCurrentLocation (targetLocation);
		line.gameObject.SetActive (false);
	}

	void OnBackHome(){
		RectTransform h = home.gameObject.GetComponent<Image> ().rectTransform;
		Vector2 pos = new Vector2(-h.anchoredPosition.x, -h.anchoredPosition.y);
		RectTransform t = this.gameObject.GetComponent<Image> ().rectTransform;
		iTween.ValueTo (this.gameObject, iTween.Hash (
			"from", t.anchoredPosition,
			"to", pos,
			"time", 1f,
			"onupdatetarget", this.gameObject,
			"onupdate", "itweenMoveElememt"));
	}

	public void itweenMoveElememt(Vector2 pos){
		touchBoard.rectTransform.anchoredPosition = pos;
	}

	Vector2 getMapPos(GameObject go){
		return new Vector2(go.transform.localPosition.x + this.transform.localPosition.x, go.transform.localPosition.y + this.transform.localPosition.y);
	}

	void checkHomeOut(){
		Vector2 pos = new Vector2(home.transform.localPosition.x + this.transform.localPosition.x, home.transform.localPosition.y + this.transform.localPosition.y);
		//Debug.Log (pos.x + "," + pos.y);
		if (Mathf.Abs (pos.x) > refScaler.referenceResolution.x * 0.5f || Mathf.Abs (pos.y) > refScaler.referenceResolution.y * 0.5f) {
			OnHomeOut (pos);
		} else if (homeIcon.enabled) {
			homeIcon.enabled = false;
		}
	}

	void OnHomeOut(Vector2 pos){
		float x = refScaler.referenceResolution.x * 0.5f - 32;
		float y = refScaler.referenceResolution.y * 0.5f - 32;
		if (pos.x > x) {
			pos.x = x;
		} else if (pos.x < -x) {
			pos.x = -x;
		}
		if (pos.y > y) {
			pos.y = y;
		} else if (pos.y < -y) {
			pos.y = -y;
		}
		Vector3 newPos = new Vector3( pos.x, pos.y, homeIcon.gameObject.transform.localPosition.z );
		homeIcon.gameObject.transform.localPosition = newPos;
		if (!homeIcon.enabled){
			homeIcon.enabled = true;
		}
		//Debug.Log ("home out");

	}

	// Update is called once per frame
	void Update () {
		checkHomeOut ();
	}
}

