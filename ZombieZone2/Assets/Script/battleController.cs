using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class battleController : MonoBehaviour, IPointerDownHandler {

	public CanvasScaler refScaler;
	public Section[] bg;
	public float speed = 1.0f;
	public int sectionLeftLimit = -1;
	public int sectionRightLimit = 1;
	float move = 0;

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
		move = move + (-pos.x - move);
	}

	// Use this for initialization
	void Start () {
		if(bg.Length == 3){
			bg[0].id = -1;
			bg[1].id = 0;
			bg[2].id = 1;
			bg[0].onSectionLoad(-1);
			bg[1].onSectionLoad(0);
			bg[2].onSectionLoad(1);
		}

	}

	// Update is called once per frame
	void Update () {
		float refWidth = refScaler.referenceResolution.x;
		if(bg.Length == 3){
			if((bg[0].id == sectionLeftLimit && bg[0].gameObject.transform.localPosition.x >= 0 && move > 0) || 
				(bg[2].id == sectionRightLimit && bg[2].gameObject.transform.localPosition.x <= 0 && move < 0)){
				return;
			}
		}
		if(Mathf.Abs(move) >= speed){
			float factor = (move > 0)? speed : -speed;
			foreach(Section sc in bg){
				Vector3 newpos = new Vector3(sc.gameObject.transform.localPosition.x + factor, sc.gameObject.transform.localPosition.y, sc.gameObject.transform.localPosition.z);
				sc.gameObject.transform.localPosition = newpos; 
			}
			move -= factor;
		}
		//adjust bg
		if(bg[0].gameObject.transform.localPosition.x < -refWidth*1.5f && bg[2].id < sectionRightLimit){
			Vector3 newPos = bg[2].gameObject.transform.localPosition;
			newPos.x += refWidth;
			bg[0].gameObject.transform.localPosition = newPos;
			Section tempSc = bg[0];
			bg[0] = bg[1];
			bg[1] = bg[2];
			bg[2] = tempSc;
			bg[2].id = bg[1].id+1;
			bg[2].onSectionLoad(bg[2].id);
		}
		else if(bg[2].gameObject.transform.localPosition.x > refWidth*1.5f && bg[0].id > sectionLeftLimit){
			Vector3 newPos = bg[0].gameObject.transform.localPosition;
			newPos.x -= refWidth;
			bg[2].gameObject.transform.localPosition = newPos;
			Section tempSc = bg[2];
			bg[2] = bg[1];
			bg[1] = bg[0];
			bg[0] = tempSc;
			bg[0].id = bg[1].id-1;
			bg[0].onSectionLoad(bg[0].id);
		}
	}
}
