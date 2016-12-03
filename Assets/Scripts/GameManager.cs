using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public int movesAvailable = 50;
	public int levelMoves;
	public int totalMoves;
	public int totalScore = 0;
	public int bonusMoves = 25;

	private int level = 1;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	public void InitGame() 
	{
		boardScript.SetupScene ();
	}

	private void OnLevelWasLoaded (int index) 
	{
		level++;
		levelMoves = 0;
		InitGame();
	}


	public void GameOver () 
	{	
		totalScore = totalMoves * level;
		enabled = false;
	}

}
