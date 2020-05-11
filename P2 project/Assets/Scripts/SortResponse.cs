using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortResponse : MonoBehaviour
{
	private int time;
	private const int timeMax = 60;
	
	private float scaleAnim = 0.0f; // The scale animation value (goes from 0 to 1).
	private Vector3 originalScale; // The original scale of the object.
	private const float SCALE_SPEED = 0.1f; // The speed of the scale animation.
	private const float SCALE_MIN = 1.25f; // The minimum scale factor.
	private const float SCALE_MAX = 1.75f; // The maximum scale factor.
	
    // Start is called before the first frame update
    void Start()
    {
		// Get original scale:
        originalScale = transform.localScale;
		
		// Set timer:
		time = timeMax;
    }

    // Update is called once per frame
    void Update()
    {
		// Animate scale:
		scaleAnim = ((float) time) / ((float) timeMax);
		float s = Mathf.Lerp(SCALE_MIN, SCALE_MAX, scaleAnim);
		transform.localScale = originalScale * s;
		
		// Animate transparency:
		this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, scaleAnim);
		
		if(time <= 0) // If timer has run out:
		{
			Destroy(gameObject); // Destroy self.
		}
		
        time--; // Decrement timer.
    }
}
