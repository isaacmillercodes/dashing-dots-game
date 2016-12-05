using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public int movesAvailable = 25;
	public int levelMoves;
	public int totalMoves;
	public int levelScore = 500;
	public int totalScore = 0;
	public int bonusMoves = 15;
	public int level = 1;
	public int movesPerLevel;

	private Text movesAvailableText;
	private Text scoreText;
	private GameObject menuImage;
	private Button newGameButton;


	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();

	}

	private void InitGame()
	{
		boardScript.SetupScene ();
	}

	private void OnLevelWasLoaded (int index)
	{
		if (level > 1) 
		{
			totalScore += ((levelScore / levelMoves) + (totalMoves / level));
			levelMoves = 0;
		}
		level++;
		InitGame();
	}

	void Update () {
	}

	public void GameOver ()
	{
		if (movesAvailable <= 0)
		{
			instance = null;
			Destroy (gameObject);
			SceneManager.LoadScene (0);
		}
	}

}
