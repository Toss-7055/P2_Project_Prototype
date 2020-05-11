using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
	private bool hovering = false; // Whether a trash object is hovering over this garbage can object.
	public bool Hovering
	{
		get { return hovering; }
		set { hovering = value; }
	}
	
	private float scaleAnim = 0.0f; // The scale animation value (goes from 0 to 1).
	private Vector3 originalScale; // The original scale of the object.
	private const float SCALE_SPEED = 0.1f; // The speed of the scale animation.
	private const float SCALE_MIN = 1.0f; // The minimum scale factor.
	private const float SCALE_MAX = 1.1f; // The maximum scale factor.
	
    // Start is called before the first frame update.
    void Start()
    {
		// Get original scale:
        originalScale = transform.localScale;
    }

    // Update is called once per frame.
    void Update()
    {
        if(hovering)
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
