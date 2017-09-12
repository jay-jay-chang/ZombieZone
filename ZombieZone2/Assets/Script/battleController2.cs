using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity.Examples;

public class battleController2 : MonoBehaviour , IPointerDownHandler{

	public CameraAnchor cameraAnchor;
	public CanvasScaler refScaler;
	public Transform EnemyAnchor;

	public zmmModel player;

	public Transform sectionAnchor;
	public List<GameObject> sections = new List<GameObject>();
	public GameObject[] sectionData;
	public bool SectionLoop = true;

	public void OnPointerDown(PointerEventData ped) 
	{
		Vector2 pos = UIUtility.mouseToScreenPos (refScaler.referenceResolution.x, refScaler.referenceResolution.y, ped.position);
		if(player != null){
			player.MoveForward(pos.x + (cameraAnchor.gameObject.transform.localPosition - player.transform.localPosition).x);
		}
	}

	public void OnZombieSelect(ZombieAI ai){
		player.MoveForward(0);
		player.TryShoot (ai);
	}

	void OnSectionLoad(int sectionId){
		int id = (sectionId+sectionData.Length)%sectionData.Length;
		if(sections.Count <= sectionId){
			GameObject newSection = (GameObject)Instantiate (sectionData[id]);
			newSection.transform.parent = sectionAnchor;
			newSection.transform.localPosition = new Vector3(refScaler.referenceResolution.x*sectionId, 0, 0);
			newSection.transform.localScale = Vector3.one;
			sections.Add(newSection);
			Debug.Log("section " + sectionId +" loaded(+)");
		}
		else if(sections[sectionId] == null){
			GameObject newSection = (GameObject)Instantiate (sectionData[id]);
			newSection.transform.parent = sectionAnchor;
			newSection.transform.localPosition = new Vector3(refScaler.referenceResolution.x*sectionId, 0, 0);
			newSection.transform.localScale = Vector3.one;
			sections[id] = newSection;
			Debug.Log("section " + sectionId +" loaded(_)");
		}
		//bg [sectionId].onSectionLoad(sections[bg[sectionId].id-sectionLeftLimit]);
	}

	void prepareSections(){
		int loc = Mathf.FloorToInt((player.transform.localPosition.x + 0.5f*refScaler.referenceResolution.x)/refScaler.referenceResolution.x);
		if (loc - 1 >= 0) {
			OnSectionLoad (loc - 1);
		}
		OnSectionLoad(loc);
		if (loc + 1 < sectionData.Length || SectionLoop) {
			OnSectionLoad (loc + 1);
		}
	}

	// Use this for initialization
	void Start () {
		cameraAnchor.x_axis_limit = new Vector2((sectionData.Length-1)*refScaler.referenceResolution.x, 0);
		prepareSections();
		foreach(ZombieView view in sectionAnchor.gameObject.GetComponentsInChildren<ZombieView>()){
			view.OnZombieSelect += OnZombieSelect;
		}
		//player.transform.SetSiblingIndex (40);
		//gameObject.transform.GetChild (0).transform.SetSiblingIndex(100);
		//EnemyAnchor.SetSiblingIndex (50);
		//sectionAnchor.transform.SetSiblingIndex (60);
	}
	
	// Update is called once per frame
	void Update () {
		prepareSections();
	}
}
