using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	private GameObject newGameButton;
//	private GameObject menuImage;

	// Use this for initialization
	void Start () {
//		menuImage = GameObject.Find ("MenuBackground");
//		Button btn = newGameButton.GetComponent<Button>();
		newGameButton = GameObject.Find("NewGameButton");

		Button newGame = newGameButton.GetComponent<Button> ();

//		Debug.Log (newGameButton);
//		Debug.Log (menuImage);
//		Debug.Log (newGame);


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
