using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity.Examples;

public class battleController : MonoBehaviour, IPointerDownHandler {

	public CanvasScaler refScaler;
	public Section[] bg;
	public float speed = 1.0f;
	public int sectionLeftLimit = -1;
	public int sectionRightLimit = 1;
	public zmmModel zmm;
	public GameObject basespace;
	public GameObject[] sections;
	float charOffset;
	float move = 0;
	float zmmMove = 0;

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
			OnSectionLoad(0);
			OnSectionLoad(1);
			OnSectionLoad(2);
		}
		foreach(ZombieView view in basespace.GetComponentsInChildren<ZombieView>()){
			view.OnZombieSelect += OnZombieSelect;
		}
		charOffset = zmm.gameObject.transform.localPosition.x;
	}

	void OnSectionLoad(int sectionId){
		bg [sectionId].onSectionLoad(sections[bg[sectionId].id-sectionLeftLimit]);
	}

	// Update is called once per frame
	void Update () {
		float refWidth = refScaler.referenceResolution.x;
		if(bg.Length == 3){
			if((bg[0].id == sectionLeftLimit && bg[0].gameObject.transform.localPosition.x >= 0 && zmm.transform.localPosition.x <= charOffset /*&& move > 0*/) || 
				(bg[2].id == sectionRightLimit && bg[2].gameObject.transform.localPosition.x <= 0 && zmm.transform.localPosition.x >= charOffset /*&& move < 0*/)){

				float zmmMove = zmm.transform.localPosition.x + move;
				float moveAbs = Mathf.Abs (zmmMove);
				if (Mathf.Abs(zmmMove) > speed ) {
					zmm.TryMove(zmmMove);
					float factor = (zmmMove > 0) ? -speed : speed;
					Vector3 newpos = new Vector3 (zmm.transform.localPosition.x + factor, zmm.transform.localPosition.y, zmm.transform.localPosition.z);
					zmm.transform.localPosition = newpos;
				}
				else{
					zmmMove = 0;
					zmm.TryMove(0);
				}
				return;
			}
		}
		//while normal moving, reset transform
		zmm.transform.localPosition = new Vector3 (charOffset, zmm.transform.localPosition.y, zmm.transform.localPosition.z);

		if (Mathf.Abs (move) >= speed) {
			float factor = (move > 0) ? speed : -speed;
			//move scene
			foreach (Section sc in bg) {
				Vector3 newpos = new Vector3 (sc.gameObject.transform.localPosition.x + factor, sc.gameObject.transform.localPosition.y, sc.gameObject.transform.localPosition.z);
				sc.gameObject.transform.localPosition = newpos; 
			}
			//move active objects
			Vector3 basepos = new Vector3 (basespace.transform.localPosition.x + factor, basespace.transform.localPosition.y, basespace.transform.localPosition.z);
			basespace.transform.localPosition = basepos;

			move -= factor;
		} else {
			move = 0;
		}
		zmm.TryMove(move);
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
			OnSectionLoad(2);
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
			OnSectionLoad(0);
		}
	}

	public void OnZombieSelect(ZombieAI ai){
		move = 0;
		zmm.TryShoot (ai);
	}
}
