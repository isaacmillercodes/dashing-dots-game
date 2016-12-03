using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	public int moveCount = 0;
	[HideInInspector] public int playerLevelMoves = 0;
	[HideInInspector] public int playerTotalMoves = 0;

//	private int level = 1;

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
//		level++;
		InitGame();
	}


	public void GameOver () 
	{
		enabled = false;
	}

}
