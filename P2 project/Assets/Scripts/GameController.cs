using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// There should only ever exist ONE of this object!
public class GameController : MonoBehaviour
{
	private int endState = 0;
	
	private bool levelBegun = false; // Whether the level has begun (when player clicks on the start button).
	private bool levelDone = false; // Whether the level is done (when all trash is sorted).
	private int time = 0; // Timer that counts up.
	public int Time
	{
		get { return Time; }
		set { Time = Mathf.Max(value); } // Time can never go below 0.
	}
	
	private Trash[] trashObjects; // The array that holds all the (initial) trash objects in the scene.
	public int trashCount = 0; // The amount of trash left to be sorted.
	
	private int levelNumber = 0;
	private int Level { get { return levelNumber; } } // Make this only get-able, not set-able (aka Read-Only).
	
	public Vector2 areaMin, areaMax; // The area within which the trash can be positioned (given by two corner vectors).
	public int areaMargin = 32;
	
	private Canvas canvas;
	public GameObject results;
	public GameObject endButton;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get current level number:
		switch(SceneManager.GetActiveScene().name)
		{
			case("Level1"):
			{
				levelNumber = 1;
				areaMin = new Vector2(0+areaMargin, 0+areaMargin);
				areaMax = new Vector2(540-areaMargin, 189-areaMargin);
				break;
			}
			case("Level2"):
			{
				levelNumber = 2;
				areaMin = new Vector2(0+areaMargin, 0+areaMargin);
				areaMax = new Vector2(540-areaMargin, 189-areaMargin);
				break;
			}
			case("Level3"):
			{
				levelNumber = 3;
				areaMin = new Vector2(0+areaMargin, 0+areaMargin);
				areaMax = new Vector2(540-areaMargin, 374-areaMargin);
				break;
			}
		}
		
		// Find all existing trash objects:
		trashObjects = Object.FindObjectsOfType<Trash>();
		
		// Set the total number of trash to be sorted:
		trashCount = trashObjects.Length;
		
		// Randomize trash positions:
		foreach(Trash t in trashObjects)
		{
			t.UpdatePosition(areaMin, areaMax);
		}
    }
	
	// Call this method to start the level.
	public void BeginLevel()
	{
		levelBegun = true;
		time = 0;
		
		// Loop through and activate all trash objects:
		foreach(Trash t in trashObjects)
		{
			t.Active = true;
		}
	}
	
	// Converts a time to a negative score reduction:
	private int TimeToScore(int t)
	{
		return (int) (-10 * ((double) time) / 60);
	}

    // Update is called once per frame
    void Update()
    {
		if(levelBegun)
		{
			if(!levelDone)
			{
				time++; // Increment timer.
				
				if(trashCount <= 0) // If there is no more trash left:
				{
					levelDone = true;
				}
			}
			else // Game is done:
			{
				if(endState == 0) // Set up the end-level screen:
				{
					// Get canvas object:
					canvas = Object.FindObjectOfType<Canvas>();
					
					// Delete current scoreboard:
					Destroy(canvas.transform.GetChild(0).gameObject);
					
					// Create result scoreboard:
					GameObject board = Instantiate(results, new Vector3(270,480,0), Quaternion.identity, canvas.transform);
					// Create end button:
					Instantiate(endButton, new Vector3(270,160,0), Quaternion.identity, canvas.transform);
					
					
					// Set up the result scoreboard:
					double t = (double) time;
					double minutes = (double) ((int)(t/3600));
					double seconds = (double) (((int) (t/60)) % 60);
					double micro = (double) ((int) ((t % 60)/60*90));
					string strTime = minutes.ToString() + ":" + seconds.ToString() + "." + micro.ToString();
					string strHigh = (Global.score + TimeToScore(time) >= Global.highscore[levelNumber]) ? " (NY!)" : "";
					double finalScore = Global.score + TimeToScore(time);
					if(finalScore < 0) finalScore = 0;
					Global.UpdateHighscore(levelNumber); // Update high-score for this level.
					board.GetComponent<Results>().resultText = "Score/Tid: " + Global.score.ToString() + " - " + strTime + "\n" + "Endelig score: " + finalScore.ToString() + "\n" + "Highscore: " + Global.highscore[levelNumber].ToString() + strHigh;
					
					// End setup:
					endState = 1;
					Global.score = 0;
				}
				
				// Do nothing and wait for the player to press the Level End Button.
			}
		}
    }
}
