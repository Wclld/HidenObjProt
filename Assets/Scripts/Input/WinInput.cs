using UnityEngine;

public class WinInput : IInput
{
	private readonly GameObject _cutout;
	private readonly Vector3 _canvasSize;


	public WinInput(GameObject cutout, Vector3 canvasSize)
	{
		_cutout = cutout;
		_canvasSize = canvasSize;
	}


	public void DetectInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_cutout.SetActive(true);
		}
		if(Input.GetMouseButton(0))
		{
			var mousePos = Input.mousePosition;
			mousePos.x -= _canvasSize.x / 2;
			mousePos.y -= _canvasSize.y / 2;
			_cutout.transform.localPosition = mousePos;
		}
		if (Input.GetMouseButtonUp(0))
		{
			_cutout.SetActive(false);
		}
	}
}