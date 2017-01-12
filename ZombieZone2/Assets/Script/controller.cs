using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class controller : MonoBehaviour, IPointerDownHandler{

	public CanvasScaler refScaler;
	public GameObject controlTarget;
	public float speed = 1.0f;
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
		Debug.Log ("clickPos = " + pos.x + "," + pos.y);
		target = pos;
	}

	// Use this for initialization
	void Start () {
		if (controlTarget != null)
		{
			target = new Vector2 (controlTarget.transform.localPosition.x, controlTarget.transform.localPosition.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float fx = target.x - controlTarget.transform.localPosition.x;
		float fy = target.y - controlTarget.transform.localPosition.y;

		if (Mathf.Abs(fx) < 0.0001f && Mathf.Abs(fy) < 0.0001f) 
		{
			return;
		}
		float factor = speed / Mathf.Sqrt (fx * fx + fy * fy);

		Debug.Log (factor * fx + "," + factor * fy);
		Vector3 newpos = new Vector3(controlTarget.transform.localPosition.x + factor * fx, controlTarget.transform.localPosition.y + factor * fy, controlTarget.transform.localPosition.z);
		controlTarget.transform.localPosition = newpos;
	}
}
