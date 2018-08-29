using System.Collections.Generic;	// Allow us to use Lists
using UnityEngine;
using System;
using Random = UnityEngine.Random;



public class BoardManager : MonoBehaviour {
	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;			// Set the min/max range for the value

		// Assign constructor
		public Count(int min, int max){
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public Count grassCount = new Count(5,10);
	public Count treeCount = new Count(1,5);
	
	public GameObject player;
	public GameObject[] grassTiles;
	public GameObject[] treeTiles;

	private Transform boardHolder;									// store a reference to the transform of our game objects
	private List<Vector3> gridPositions = new List<Vector3> ();		// a list of possible locations to place tiles

	// Clear list gridPositions and prepares it to generate a new board
	void InitialiseList(){
		gridPositions.Clear();
		
		for(int x = 1; x< columns-1; x++){
			for(int y=1; y<rows-1; y++){
				// for each index add a new vector3 to the list
				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	}

	// Setup the outer walls and floor (background) of the gameboard
	// Clearly it is of no necessity to setup the floor in this game
	void BoardSetup(){
		boardHolder = new GameObject("Board").transform;

		for(int x=-1; x<columns+1; x++){
			for(int y=-1; y<rows+1; y++){
				if(x==-1|| x==columns || y==-1 || y==rows){
					GameObject toInstantiate = treeTiles[Random.Range(0,treeTiles.Length)];
					// Instantiate the game object using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position
					GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}

				// GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
				// instance.transform.SetParent(boardHolder);
			}
		}
	}

	// RandomPosition returns a random position from our list gridPositions
	// It would remove the tile exists and return its position. Hence in this project we dont have to do this.
	Vector3 RandomPosition(){
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 RandomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return RandomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum){
		int objectCount = Random.Range(minimum,maximum);

		for(int i=0;i<objectCount; i++){
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0,tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level){
		// Create outer walls
		BoardSetup();

		// Reset gridPositions
		InitialiseList();

		// Instantiate a random number of grass
		LayoutObjectAtRandom(grassTiles, grassCount.minimum, grassCount.maximum);
		LayoutObjectAtRandom(treeTiles, treeCount.minimum, treeCount.maximum);

		Instantiate(player, new Vector3(columns-1, rows-1, 0f), Quaternion.identity);
	}
}
