using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowLine : MonoBehaviour {

	public GameObject arrow;
	public LineRenderer linerenderer;

	public GameObject sGO;
	public GameObject eGO;

	public Vector3 start;
	public Vector3 end;

	// Use this for initialization
	void Start () {
		if (sGO != null) {
			setStart (sGO);
		}
		if (eGO != null) {
			setEnd (eGO);
		}
	}

	void OnModify(){
		Vector3 dir = end - start;
		float angle = 0;
		if (dir.x >= 0) {
			if (dir.y < 0) {
				angle = 360f;
			}
		} else {
			angle = 180f;
//			if (dir.y < 0) {
//				angle = 180f;
//			} else {
//				angle = 180f;
//			}
		}
		angle += Mathf.Atan (dir.y / dir.x) * 180f / Mathf.PI;
		arrow.transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
		Vector3 forward = dir * 10 / dir.magnitude;
		arrow.transform.localPosition = new Vector3 (end.x + forward.x, end.y + forward.y, arrow.transform.localPosition.z);
	}

	public void setStart (GameObject go){
		start = go.transform.localPosition;
		linerenderer.SetPosition (0, new Vector3(start.x, start.y, 0));
		OnModify();
	}

	public void setEnd(GameObject go){
		Vector3 dir = go.transform.localPosition - start;
		dir *= (dir.magnitude - 60) / dir.magnitude;
		end = start + new Vector3 (dir.x, dir.y, 0);
		linerenderer.SetPosition (1, new Vector3(end.x, end.y, 0));
		OnModify();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
