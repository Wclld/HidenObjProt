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
}