using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

	// Creates a line renderer that follows a Sin() function
	// and animates it.

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public int lengthOfLineRenderer = 20;
	public GameObject start;
	public GameObject end;

	void Start()
	{
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.material = new Material(Shader.Find("UI/Default"));
		lineRenderer.widthMultiplier = 0.2f;
		lineRenderer.positionCount = lengthOfLineRenderer;

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;
	}

	void Update()
	{
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
//		var t = Time.time;
//		for (int i = 0; i < lengthOfLineRenderer; i++)
//		{
//			lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), -0.5f));
//		}
		lineRenderer.SetPosition(0, start.transform.position);
		lineRenderer.SetPosition(1, end.transform.position);
	}
}
