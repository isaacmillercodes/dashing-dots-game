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

//	public int levelMoves;
//	public int totalMoves;
//	public int movesAvailable;
//	public int totalScore = 0;
//	public int bonusMoves;
//	public int count;

	private Vector2 currentDirection = Vector2.zero;
	private Rigidbody2D body;
	private float speed;
	private Text movesAvailableText;

	void Start() 
	{
		body = GetComponent<Rigidbody2D>();
		body.constraints = RigidbodyConstraints2D.FreezeAll;

		spriteRenderer = GetComponent<SpriteRenderer>();
//		if (spriteRenderer.sprite == null)
//			spriteRenderer.sprite = sprite1;


//		movesAvailable = GameManager.instance.movesAvailable;
//		levelMoves = GameManager.instance.levelMoves;
//		totalMoves = GameManager.instance.totalMoves;
//		movesAvailable = GameManager.instance.movesAvailable;
//		levelMoves = 0;
//		bonusMoves = 25;
	}

	private void FixedUpdate() 
	{	
		movesAvailableText = GameObject.Find("MovesRemainingText").GetComponent<Text>();

		movesAvailableText.text = "Moves Remaining: " + GameManager.instance.movesAvailable;

		lastInput = ignoreInput;
		speed = body.velocity.magnitude;

//		Debug.Log ("Update Speed: " + speed);
		if (speed < .5) 
		{
//			count++;
//			Debug.Log ("Stop" + count);
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

//			Debug.Log ("Click: " + mousePosition);

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


//			if (!inputDirection.Equals (Vector2.zero) && activePlayer && !ignoreInput)
//			{
//				currentDirection = inputDirection;
////				Vector2 destination = ;
//				ignoreInput = true;
//				body.AddForce (currentDirection * acceleration);
////				body.MovePosition(destination * Time.fixedDeltaTime);
//				currentDirection = Vector2.zero;
//			}


//			Vector2 rayDirection = new Vector2(horizontal, vertical);
//
//			RaycastHit2D hit = Physics2D.Raycast(body.position, rayDirection);
//
//			//If something was hit.
//			if ( hit.collider != null )
//			{
//				//Display the point in world space where the ray hit the collider's surface.
//				Debug.Log ("Hit: " + hit.point);
//			}

//			Vector3 inputDirection = new Vector3(horizontal, vertical, 0);
//
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
//			this.body.constraints = RigidbodyConstraints2D.FreezeAll;
		}

		CheckIfGameOver ();
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

//	private void OnCollisionEnter2D (Collision2D other)
//	{	
//		if (other.gameObject.tag == "Player") 
//		{
//			body.constraints = RigidbodyConstraints2D.FreezeAll;
//		}
//	}
//		

	private void grabToken (string color, string name1, string name2, GameObject token)
	{
		if (name1.EndsWith(color + "(Clone)") && name2.EndsWith(color + "(Clone)"))
		{
			token.SetActive(false);
			Restart ();
//			movesAvailable += (bonusMoves / levelMoves);
//			totalScore += (1000 / levelMoves);
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

	private void CheckIfGameOver () {
		if (GameManager.instance.movesAvailable <= 0) {
//			SoundManager.instance.PlaySingle (gameOverSound);
//			SoundManager.instance.musicSource.Stop ();
			GameManager.instance.GameOver ();
		}
	}

	private void Restart () 
	{
		SceneManager.LoadScene (0);
	}

}
