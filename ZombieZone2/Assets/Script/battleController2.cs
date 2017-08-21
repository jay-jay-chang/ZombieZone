using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity.Examples;

public class battleController2 : MonoBehaviour , IPointerDownHandler{

	public Transform cameraAnchor;
	public CanvasScaler refScaler;
	public GameObject basespace;
	public zmmModel player;

	public void OnPointerDown(PointerEventData ped) 
	{
		Vector2 pos = UIUtility.mouseToScreenPos (refScaler.referenceResolution.x, refScaler.referenceResolution.y, ped.position);
		if(player != null){
			player.MoveForward(pos.x + (cameraAnchor.localPosition - player.transform.localPosition).x);
		}
	}

	public void OnZombieSelect(ZombieAI ai){
		player.MoveForward(0);
		player.TryShoot (ai);
	}

	// Use this for initialization
	void Start () {
		foreach(ZombieView view in basespace.GetComponentsInChildren<ZombieView>()){
			view.OnZombieSelect += OnZombieSelect;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
