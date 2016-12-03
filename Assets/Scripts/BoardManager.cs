using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count 
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max) 
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 16;
	public int rows = 16;
	public Count barrierCount = new Count (12, 16);
	public GameObject floorTile;
	public GameObject[] tokenTiles;
	public GameObject[] barrierTiles;
	public GameObject[] outerWallTiles;
	public GameObject[] playerTiles;

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List <Vector3>();

	void InitializeList() 
	{

		gridPositions.Clear ();

		for (int x = 1; x < columns - 1; x++) {
			for (int y = 1; y < rows - 1; y++) {

				gridPositions.Add (new Vector3 (x, y, 0f));

			}
		}
	}

	void BoardSetup() 
	{
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) 
		{
			for (int y = -1; y < rows + 1; y++) 
			{

				GameObject toInstantiate = floorTile;

				if (x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent (boardHolder);
			}
		}
			
	}

	Vector3 RandomPosition() 
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);

		return randomPosition;
	}

	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum) 
	{

		int objectCount = Random.Range (minimum, maximum + 1);

		for (int i = 0; i < objectCount; i++) 
		{
			Vector3 randomPosition = RandomPosition ();

			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];

			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}

	}

	void LayoutPlayersAtRandom () 
	{

		for (int i = 0; i < playerTiles.Length; i++) 
		{
			Vector3 randomPosition = RandomPosition ();

			GameObject player = playerTiles[i];

			Instantiate (player, randomPosition, Quaternion.identity);
		}

	}

	public void SetupScene () 
	{
		BoardSetup ();

		InitializeList ();

		LayoutObjectAtRandom (barrierTiles, barrierCount.minimum, barrierCount.maximum);

		LayoutObjectAtRandom (tokenTiles, 1, 1);

		LayoutPlayersAtRandom ();
	}

}
