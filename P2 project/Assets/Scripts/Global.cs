using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// There should only ever exist ONE of this object!
public class Global : MonoBehaviour
{
    public static int score = 0; // The current amount of points scored.
	public static int[] highscore = new int[] {0, 0, 0}; // The high-scores for our three levels.
	public const int POINTS = 100; // The points given per piece of correctly sorted trash.
	
	// Compares the current score against the high-score of a level, and updates that high-score if it is lower than the score.
	public static void UpdateHighscore(int level)
	{
		if(score > highscore[level]) highscore[level] = score;
	}
	
	// Prevent this object from being destroyed when we change scenes!
	void Awake()
	{
        DontDestroyOnLoad(transform.gameObject);
    }
	
    // Update is called once per frame.
    void Update()
    {
        
    }
}
