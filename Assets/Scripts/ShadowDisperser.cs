using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDisperser : MonoBehaviour
{
	private IInput _input;


	private void Start()
	{
		var cutout = GetComponentInChildren<Coffee.UIExtensions.Unmask>().gameObject;

		cutout.SetActive(false);

		Vector3 canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.size;
		canvasSize.y -= cutout.GetComponent<RectTransform>().rect.height * 2;

#if UNITY_EDITOR
		_input = new WinInput(cutout, canvasSize);
#elif UNITY_ANDROID || UNITY_IOS
		_input = new MobileInput(cutout, canvasSize);
#endif
	}
	
	void Update()
	{
		_input?.DetectInput();
	}
}
