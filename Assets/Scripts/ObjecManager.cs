using System;
using UnityEngine;

public class ObjecManager : MonoBehaviour
{
	[SerializeField] InteractableObject[] _interactableObjects;
	public static event Action OnAllFound;

	private void Start()
	{
		InteractableObject.OnObjectFound += CheckAllObjectsFound;
	}

	private void CheckAllObjectsFound()
	{
		for (var i = 0; i < _interactableObjects.Length; i++)
		{
			if(!_interactableObjects[i].IsFound)
			{
				return;
			}
		}
		OnAllFound?.Invoke();
	}

	public void SetObjectsActive()
	{
		for (var i = 0; i < _interactableObjects.Length; i++)
		{
			_interactableObjects[i].gameObject.SetActive(true);
		}
	}
}