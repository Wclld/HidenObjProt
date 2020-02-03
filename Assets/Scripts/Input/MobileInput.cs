using UnityEngine;

public class MobileInput : IInput
{
	private readonly GameObject _cutout;
	private readonly Vector3 _canvasSize;

	public MobileInput(GameObject cutout, Vector3 canvasSize)
	{
		_cutout = cutout;
		_canvasSize = canvasSize;
		Input.multiTouchEnabled = false;
	}
	public void DetectInput()
	{
		if(Input.touchCount > 0)
		{
			var touch = Input.touches[0]; 
			if(touch.phase == TouchPhase.Began)
			{
				_cutout.SetActive(true);
			}
			else
			if(touch.phase == TouchPhase.Ended)
			{
				_cutout.SetActive(false);
			}
			if(touch.phase == TouchPhase.Moved)
			{
				var touchPosition = touch.position;
				touchPosition.x -= _canvasSize.x / 2;
				touchPosition.y -= _canvasSize.y / 2;
				_cutout.transform.localPosition = touchPosition;
			}
		}
 		// else
		// {
		// 	_cutout.SetActive(false);
		// }
	}
}