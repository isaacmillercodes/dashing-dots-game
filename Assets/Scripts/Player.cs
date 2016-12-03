﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public bool activePlayer = false;
	public bool ignoreInput = false;
	public float acceleration;

	private Vector3 currentDirection = Vector3.zero;

	private Rigidbody2D body;
	private float speed;

	void Start() 
	{
		body = GetComponent<Rigidbody2D>();
		body.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	private void Update() 
	{
		speed = body.velocity.magnitude;

		if (speed < 0.5) 
		{
			this.body.velocity = new Vector3(0, 0, 0);
			this.ignoreInput = false;

			if (!this.activePlayer) 
			{
				this.body.constraints = RigidbodyConstraints2D.FreezeAll;
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

		if (currentDirection.Equals(Vector3.zero)) 
		{
			int horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
			int vertical = (int)(Input.GetAxisRaw ("Vertical"));

			if (horizontal != 0) 
			{
				vertical = 0;
			}

			Vector3 inputDirection = new Vector3(horizontal, vertical, 0);

			if (!inputDirection.Equals(Vector3.zero) && activePlayer && !ignoreInput) 
			{
				currentDirection = inputDirection;
				ignoreInput = true;
				this.body.velocity = currentDirection * acceleration;
				currentDirection = Vector3.zero;
			}
		}
	}

	private void OnTriggerEnter2D	 (Collider2D other) 
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

	private void Restart () 
	{
		SceneManager.LoadScene (0);
	}

}
