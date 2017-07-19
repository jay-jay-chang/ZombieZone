using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneController : MonoBehaviour {

	public Color	lightcolor;
	public Color	darkcolor;
	public Image[] 	darkItems;
	public Image[] 	lights;
	public Image	compuerLight;
	public Image	warningLen;
	public bool 	isLight = false;

	Color warningLenColer;

	float time = 0f;

	// Use this for initialization
	void Start () {
		foreach (Image img in darkItems) {
			img.color = darkcolor;
		}
		warningLenColer = warningLen.color;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 5) {
			isLight = !isLight;
			time = 0;
		}

		if (isLight) {
			foreach (Image img in darkItems) {
				img.color = lightcolor;
			}

			foreach (Image img in lights) {
				img.enabled = true;
			}

			compuerLight.enabled = true;

			warningLen.color = new Color(warningLenColer.r*lightcolor.r,  
											warningLenColer.g*lightcolor.g, 
											warningLenColer.b*lightcolor.b, 
											warningLenColer.a*lightcolor.a);
		} else {
			foreach (Image img in darkItems) {
				img.color = darkcolor;
			}

			foreach (Image img in lights) {
				img.enabled = false;
			}

			compuerLight.enabled = false;

			warningLen.color = new Color(warningLenColer.r*darkcolor.r,  
				warningLenColer.g*darkcolor.g, 
				warningLenColer.b*darkcolor.b, 
				warningLenColer.a*darkcolor.a);
		}
	}
}
