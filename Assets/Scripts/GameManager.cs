using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		InteractableObject.OnObjectFound += () => ScoreBoard.ChangeScore(1);
	}

	private void OnApplicationQuit() 
	{
		ScoreBoard.SaveScore();
	}
	private void OnApplicationFocus(bool focusStatus)
	{
		if(!focusStatus)
			ScoreBoard.SaveScore();
	}
	private void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus)
			ScoreBoard.SaveScore();
	}
}