using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mapToHome : MonoBehaviour, IPointerDownHandler {

	public System.Action OnBackHome;

	public void OnPointerDown(PointerEventData ped){
		if (OnBackHome != null) {
			OnBackHome();
		}
	}
}
