using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class battleController : MonoBehaviour, IPointerDownHandler {

	public CanvasScaler refScaler;
	public GameObject controlTarget;
	public float speed = 1.0f;
	public bool y_axis_lock = false;
	Vector2 target;
	Vector2 current;

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
		Debug.Log ("clickPos = " + pos.x + "," + pos.y);
		target = pos + current;
		Debug.Log ("targetPos = " + target.x + "," + target.y);
	}

	// Use this for initialization
	void Start () {
		if (controlTarget != null)
		{
			current = target = new Vector2 (controlTarget.transform.localPosition.x, controlTarget.transform.localPosition.y);
		}
	}

	// Update is called once per frame
	void Update () {
		float fx = target.x - current.x;//controlTarget.transform.localPosition.x;
		float fy = target.y - current.y;//controlTarget.transform.localPosition.y;
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
}
