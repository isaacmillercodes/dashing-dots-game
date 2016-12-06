using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public bool activePlayer = false;
	public bool ignoreInput = false;
	public bool lastInput;
	public Sprite sprite1;
	public Sprite sprite2;


	private float acceleration = 25;
	private SpriteRenderer spriteRenderer;
	private Vector2 currentDirection = Vector2.zero;
	private Rigidbody2D body;
	private float speed;
	private Text movesAvailableText;
	private Text scoreText;
	private Text avgMovesText;
	private Text levelMovesText;


	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.constraints = RigidbodyConstraints2D.FreezeAll;

		spriteRenderer = GetComponent<SpriteRenderer>();

		movesAvailableText = GameObject.Find("MovesRemainingText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		avgMovesText = GameObject.Find("AverageMovesText").GetComponent<Text>();
		levelMovesText = GameObject.Find("LevelMovesText").GetComponent<Text>();
	}

	private void FixedUpdate()
	{	
		if (GameManager.instance) 
		{
			movesAvailableText.text = "Moves Remaining       " + GameManager.instance.movesAvailable;
			scoreText.text = "Total Score      " + GameManager.instance.totalScore;
			avgMovesText.text = "Average Level Moves " + GameManager.instance.avgMoves;
			levelMovesText.text = "Moves This Level              " + GameManager.instance.levelMoves;
			GameManager.instance.CheckIfGameOver();
		}

		lastInput = ignoreInput;
		speed = body.velocity.magnitude;

		if (speed < .5)
		{
			body.velocity = new Vector2(0, 0);
			ignoreInput = false;

			if (!activePlayer)
			{
				body.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

			if (hitCollider)
			{
				if (hitCollider.gameObject.tag == "Player")
				{

					Player thisPlayer = hitCollider.gameObject.GetComponent<Player>();

					Player[] players = GetComponents<Player> ();

					foreach (Player player in players)
					{
						player.activePlayer = false;
					}

					thisPlayer.activePlayer = true;
					thisPlayer.body.constraints = RigidbodyConstraints2D.None;
					thisPlayer.body.constraints = RigidbodyConstraints2D.FreezeRotation;

				}
			}
		}

		if (currentDirection.Equals(Vector2.zero))
		{
			int horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
			int vertical = (int)(Input.GetAxisRaw ("Vertical"));

			if (horizontal != 0)
			{
				vertical = 0;
			}

			Vector2 inputDirection = new Vector2 (horizontal, vertical);

			if (!inputDirection.Equals(Vector2.zero) && activePlayer && !ignoreInput)
			{
				currentDirection = inputDirection;
				ignoreInput = true;
				body.velocity = currentDirection * acceleration;
				currentDirection = Vector2.zero;
			}
		}

		if (ignoreInput && !lastInput && (speed < 0.5))
		{
			GameManager.instance.movesAvailable--;
			GameManager.instance.levelMoves++;
			GameManager.instance.totalMoves++;
		}

		CheckSprite ();
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Token")
		{
			grabToken ("Beige", this.name, other.name, other.gameObject);
			grabToken ("Green", this.name, other.name, other.gameObject);
			grabToken ("Yellow", this.name, other.name, other.gameObject);
			grabToken ("Pink", this.name, other.name, other.gameObject);
		}

	}
		

	private void grabToken (string color, string name1, string name2, GameObject token)
	{
		if (name1.EndsWith(color + "(Clone)") && name2.EndsWith(color + "(Clone)"))
		{
			token.SetActive(false);
			Restart ();
			enabled = false;
		}
	}

	void CheckSprite ()
	{
		if (activePlayer)
		{
			spriteRenderer.sprite = sprite2;
		}
		else
		{
			spriteRenderer.sprite = sprite1;
		}
	}

	private void Restart ()
	{
		SceneManager.LoadScene (1);
	}

}
