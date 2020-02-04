using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	[SerializeField] TMPro.TextMeshProUGUI _scoreText;

	private void Start()
	{
		ScoreBoard.OnScoreChange += ChangeScore;
		ScoreBoard.LoadScore();
	}
	
	private void ChangeScore(int score)
	{
		_scoreText.text = score.ToString();
	}
}