using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	private GameObject newGameButton;

	void Start () 
	{
		
		newGameButton = GameObject.Find("NewGameButton");

		Button newGame = newGameButton.GetComponent<Button> ();

		newGame.onClick.AddListener(delegate()
			{ 
				StartGame(1); 
			});

	}

	public void StartGame(int level)
	{
		SceneManager.LoadScene(level);
	}

	void HideMenu () {
//		newGameButton.gameObject.SetActive (false);
//		menuImage.gameObject.SetActive (false);
	}
}
