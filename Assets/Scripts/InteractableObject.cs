using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
	public static event Action OnObjectFound;

	public bool IsFound;

	[SerializeField] float _flashInTime;
	[SerializeField] float _flashOutTime;
	[SerializeField] float _moveTime;
	[SerializeField] Transform _placeInPanel;
	[SerializeField] Image _flashImage;
	private bool _isInTransition;
	private Vector3 _defaultPosition;
	private Quaternion _defaultRotation;
	private Vector2 _defaultScale;
	private RectTransform _rectTransform;

	private void Awake() 
	{
		_rectTransform = GetComponent<RectTransform>();
		_defaultPosition = transform.position;
		_defaultRotation = transform.rotation;
		_defaultScale = _rectTransform.rect.size;
	}

	private void OnEnable() 
	{
		_isInTransition = IsFound = false;
		transform.position = _defaultPosition;
		transform.rotation = _defaultRotation;
		_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _defaultScale.x);
		_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _defaultScale.y);
	}

	public void PointerClick()
	{
		if(!_isInTransition)
		{
			if(_placeInPanel)
			{
				_isInTransition = true;
				StartCoroutine(SelfHighlight());
			}
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

		IsFound = true;

		OnObjectFound?.Invoke();

		gameObject.SetActive(false);
	}
}
