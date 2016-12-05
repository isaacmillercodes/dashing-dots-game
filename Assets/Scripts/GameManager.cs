using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	private int level = 1;
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

//		GameObject button = Resources.Load <GameObject>("NewGameButton");
//
//		menuImage = GameObject.Find ("MenuBackground");
//		newGameButton = GetComponent<Button>();
//		newGameButton.onClick.Invoke(InitGame);
//
		InitGame ();
	}

//	void TaskOnClick(){
//		Debug.Log ("You have clicked the button!");
//	}
//
	private void InitGame()
	{
//		levelImage = GameObject.Find("LevelImage");

//		//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
//		movesAvailableText = GameObject.Find("MovesRemainingText").GetComponent<Text>();
//
//		//Set the text of levelText to the string "Day" and append the current level number.
//		movesAvailableText.text = "Moves Remaining: " + movesAvailable;
//		menuImage.SetActive (false);
//		newGameButton.enabled = false;
		boardScript.SetupScene ();
	}

	private void OnLevelWasLoaded (int index)
	{
		totalScore += ((levelScore / levelMoves) + (totalMoves / level));
//		movesAvailable += (bonusMoves / levelMoves);
		levelMoves = 0;
		level++;
		InitGame();
	}

	void Update () {
//		newGameButton.onClick ();
	}

	public void GameOver ()
	{
		if (movesAvailable <= 0)
		{
			instance = null;
		}
	}

}
