using System;
using UnityEngine;

public static class ScoreBoard
{
	private const string SCORE_KEY = "player_score";

	public static event Action<int> OnScoreChange; 

	private static int _score;


	public static void LoadScore()
	{
		if(PlayerPrefs.HasKey(SCORE_KEY))
		{
			ChangeScore(PlayerPrefs.GetInt(SCORE_KEY)); 
		}
	}

	public static void ChangeScore(int value)
	{
		_score += value;
		OnScoreChange?.Invoke(_score);
	}
	
	public static void SaveScore()
	{
		PlayerPrefs.SetInt(SCORE_KEY, _score);
		PlayerPrefs.Save();
	}
}