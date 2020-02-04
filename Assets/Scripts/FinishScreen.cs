using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
	[SerializeField] Timer _timer;
	[SerializeField] GameObject _gameCanvases;
	[SerializeField] GameObject _menuCanvas;
	[SerializeField] TextMeshProUGUI _finishLabel;
	[SerializeField] GameObject _finishCanvas;

	private void Start()
	{
		Timer.OnTimerFinished += ShowLoseScreen;
		ObjecManager.OnAllFound += ShowWinScreen;
	}

	private void ShowLoseScreen()
	{
		_finishLabel.text = "You lose!";
		StartCoroutine(ShowFinishScreen());
	}

	private void ShowWinScreen()
	{
		_finishLabel.text = "You win!";
		StartCoroutine(ShowFinishScreen());
	}

	private IEnumerator ShowFinishScreen()
	{
		_finishCanvas.SetActive(true);
		_gameCanvases?.SetActive(false);
		_menuCanvas?.SetActive(false);

		yield return new WaitForSeconds(5);

		_finishCanvas.SetActive(false);
		_menuCanvas?.SetActive(true);
	}

}