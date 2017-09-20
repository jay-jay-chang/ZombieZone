using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainResFX : MonoBehaviour {

	public Text text;
	public float Duration = 1.0f;
	public Color ToColor;
	public Color FromColor;
	public float UpDis = 100.0f;
	private float time;
	private bool show = false;

	// Use this for initialization
	void Start () {
	}
		
	// Update is called once per frame
	void Update () {
		if (show) {
			time += Time.deltaTime;
			if (time < Duration) {
				text.rectTransform.anchoredPosition += new Vector2 (0, Time.deltaTime * UpDis);
				text.color = Color.Lerp (FromColor, ToColor, time / Duration);
			} else {
				GameObject.Destroy (this.gameObject);
			}
		}
	}

	public void Show (string context){
		show = true;
		text.text = context;
	}
}
