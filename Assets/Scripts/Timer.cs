using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private const string TIMER_PATTERN = "00:{0:00}";


	public static event Action OnTimerFinished;


	[SerializeField] TextMeshProUGUI _timeText;
	private Coroutine _timerCoroutine;


	public void StartTimer(int time)
	{
		StopTimer();
		_timerCoroutine = StartCoroutine(TimeFlow(time));
	}

	public void StopTimer()
	{
		if(_timerCoroutine != null)
		{
			StopCoroutine(_timerCoroutine);
		}
	}


	private IEnumerator TimeFlow(int time)
	{
		var waitSecond = new WaitForSeconds(1);
		while (time >= 0)
		{
			_timeText.text = string.Format(TIMER_PATTERN,time);
			yield return waitSecond;
			time--;
		}
		OnTimerFinished?.Invoke();
	}
}
