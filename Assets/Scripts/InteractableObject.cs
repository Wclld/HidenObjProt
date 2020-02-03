using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
	[SerializeField] float _flashInTime;
	[SerializeField] float _flashOutTime;
	[SerializeField] float _moveTime;
	[SerializeField] Transform _placeInPanel;
	[SerializeField] Image _flashImage;
	public void PointerClick()
	{
		if(_placeInPanel)
		{
			StartCoroutine(SelfHighlight());
		}
	}

	private IEnumerator SelfHighlight()
	{
		var timePassed = 0f;
		var color = _flashImage.color;
		while(timePassed < _flashInTime)
		{
			timePassed += Time.deltaTime;
			color.a = Mathf.Lerp(0, 1, timePassed / _flashInTime);
			
			_flashImage.color = color;
			yield return null;
		}

		timePassed = 0;
		while(timePassed < _flashOutTime)
		{
			timePassed+= Time.deltaTime;
			color.a = Mathf.Lerp(1, 0, timePassed / _flashOutTime);
			_flashImage.color = color;
			yield return null;
		}

		StartCoroutine(MoveToPanel());
	}

	private IEnumerator MoveToPanel()
	{
		var startPos = transform.position;
		var targetPos = _placeInPanel.position;

		var startRotation = transform.rotation;

		var trail = GetComponent<TrailRenderer>();
		trail.enabled = true;

		var rectTransform = GetComponent<RectTransform>();
		var startRectSize = rectTransform.rect.size; 
		var targetRectSize = _placeInPanel.GetComponent<RectTransform>().rect.size;

		var timePassed = 0f;

		while(timePassed < _moveTime)
		{
			timePassed += Time.deltaTime;
			var timePercent = timePassed / _moveTime;

			var currentSize = Vector2.Lerp(startRectSize, targetRectSize, timePercent);;
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize.y);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize.x);

			transform.position = Vector3.Lerp(startPos, targetPos, timePercent);
			transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, timePercent);

			yield return null;
		}
		gameObject.SetActive(false);
	}
}
