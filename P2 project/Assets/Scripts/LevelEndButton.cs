using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndButton : MonoBehaviour
{
    private bool pressedDown = false; // Whether the button is being held down.
	private bool activated = false; // Whether the button has been pressed and released properly.
	
	private float scaleAnim = 0.0f; // The scale animation value (goes from 0 to 1).
	private Vector3 originalScale; // The original scale of the object.
	private const float SCALE_SPEED = 0.1f; // The speed of the scale animation.
	private const float SCALE_MIN = 1.0f; // The minimum scale factor.
	private const float SCALE_MAX = 0.9f; // The maximum scale factor.
	
	private GameController game;
	private AudioSource clickSound;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get original scale:
        originalScale = transform.localScale;
		
		// Get GameController object:
		game = Object.FindObjectOfType<GameController>();
		
		// Get AudioSource Component:
		clickSound = GetComponent<AudioSource>();
    }
	
	void OnMouseDown()
	{
		if(!pressedDown) pressedDown = true;
	}

	void OnMouseUp()
	{
		if(pressedDown && !activated)
		{
			activated = true; // Activate.
			
			/*
				ADD CODE HERE THAT MOVES TO THE MAIN MENU!
			*/
			
			clickSound.Play(); // Play click sound.
		}
	}
	
    // Update is called once per frame
    void Update()
    {
        if(pressedDown)
		{
			// Expand to maximum scale:
			if(scaleAnim < 1.0f)
			{
				scaleAnim = Mathf.Min(scaleAnim + SCALE_SPEED, 1.0f);
			}
		}
		else // Not hovering
		{
			// Contract to minimum scale:
			if(scaleAnim > 0.0f)
			{
				scaleAnim = Mathf.Max(scaleAnim - SCALE_SPEED, 0.0f);
			}
		}
		
		// Set scale:
		float s = Mathf.Lerp(SCALE_MIN, SCALE_MAX, scaleAnim);
		transform.localScale = originalScale * s;
    }
}
