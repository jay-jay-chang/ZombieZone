using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUtility {
	public static Vector2 getNormPos(Vector2 pos)
	{
		float w = Screen.width;
		float h = Screen.height;
		return new Vector2 ( ((float)pos.x - w * 0.5f)/w, ((float)pos.y - h * 0.5f)/h);
	}

	public static Vector2 mouseToScreenPos(float resolutionX, float resolutionY , Vector2 mousePos)
	{
		float w = Screen.width;
		float h = Screen.height;
		return new Vector2 ( ((float)mousePos.x - w * 0.5f)/w*resolutionX, ((float)mousePos.y - h * 0.5f)/h*resolutionY );
	}
}
