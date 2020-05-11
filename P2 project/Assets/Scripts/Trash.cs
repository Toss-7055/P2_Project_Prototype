using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
	private bool active = false;
	public bool Active
	{
		get { return active; }
		set { active = value; }
	}
	private bool draggable = true;
	private bool dragging = false;
	private bool overGarbageCan = false;
	private bool overCorrectGarbageCan = false;
	private bool reset = false;
	public bool Reset
	{
		get { return reset; }
		set { reset = value; }
	}
	
	private Vector2 dragOffset = new Vector2(0, 0);
	private Vector2 startPos;
	
	private const double MIN_DIST = 1.5;
	private const double MOVE_SPEED = 0.05;
	
	private int type = 0; /*
		0 = Plastic
		1 = Paper
		2 = Organic
	*/
	public int Type // Basically our getter and setter method in one!
	{
		get { return type; } // Return the variable if getter.
		set { type = value; } // Apply the value to the variable if setter.
	}
	
	private GameController game;
	
	public GameObject sortCorrect;
	public GameObject sortWrong;
	
	private AudioSource sound;
	
	public AudioClip Sound_Pick;
	
	// Plays a specific sound (overrides any already-playing sounds).
	private void PlaySound(AudioClip s)
	{
		sound.clip = s; // Get the new sound file.
		sound.Play(); // Play the sound.
	}
	
    // Start is called before the first frame update.
	void Start()
    {
		// Get original position:
		startPos = new Vector2(transform.position.x, transform.position.y);
		
		// Get GameController object:
		game = Object.FindObjectOfType<GameController>();
		
		// Get AudioSource Component:
		sound = GetComponent<AudioSource>();
    }
	
	
	public void UpdatePosition(Vector2 areaMin, Vector2 areaMax)
	{
		transform.position = new Vector3(Random.Range(areaMin.x, areaMax.x), Random.Range(areaMin.y, areaMax.y), transform.position.z);
		startPos = new Vector2(transform.position.x, transform.position.y);
	}
	
	void OnMouseDown()
	{
		if(active)
		{
			if(Input.GetMouseButtonDown(0) && draggable) // If left mouse button is held down (and the object is draggable):
			{
				if(!dragging) // If we are not already dragging the object, then the dragging has JUST begun:
				{
					Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get in-world coordinate of cursor.
					dragOffset.x = transform.position.x - cursor.x; // Set horizontal drag-offset (distance between object and cursor).
					dragOffset.y = transform.position.y - cursor.y; // Set vertical drag-offset (distance between object and cursor).
					PlaySound(Sound_Pick); // Play pick-up sound.
				}
				
				dragging = true; // We are now dragging this object!
			}
		}
	}
	
	void OnMouseUp()
	{
		if(active)
		{
			if(Input.GetMouseButtonUp(0) || !draggable) // If left mouse button has been let go (or the object is no longer draggable)
			{
				if(overGarbageCan)
				{
					Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
					
					if(overCorrectGarbageCan)
					{
						Global.score += Global.POINTS; // Add points to score.
						Instantiate(sortCorrect, pos, Quaternion.identity); // Create correct mark.
					}
					else // Over a wrong garbage can:
					{
						Instantiate(sortWrong, pos, Quaternion.identity); // Create wrong mark.
					}
					
					// Decrement the trash counter and delete this trash object:
					game.trashCount--;
					Destroy(gameObject);
				}
				else
				{
					dragging = false; // We are no longer dragging this object!
					draggable = false;
					reset = true;
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		GarbageCan os = other.GetComponent<GarbageCan>(); // Other Script.
		if(os != null)
		{
			os.Hovering = true;
			overGarbageCan = true;
			if(other.gameObject.tag == gameObject.tag) // If tags are identical:
			{
				overCorrectGarbageCan = true;
			}
			else overCorrectGarbageCan = false;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		GarbageCan os = other.GetComponent<GarbageCan>(); // Other Script.
		if(os != null)
		{
			os.Hovering = false;
			overGarbageCan = false;
			overCorrectGarbageCan = false;
		}
	}
	
	private void MoveBack()
	{
		// Get current 2D position:
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		
		if(Vector2.Distance(startPos, pos) <= MIN_DIST) // If this object is close enough to its starting position:
		{
			// Set to exact starting position and stop resetting:
			pos = startPos;
			reset = false;
			draggable = true;
		}
		else
		{
			// Move closer to starting position (by a factor of MOVE_SPEED):
			pos += (startPos - pos) * (float)MOVE_SPEED;
		}
		
		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
	}
	
    // Update is called once per frame.
    void Update()
    {
		if(active)
		{
			if(draggable)
			{
				if(dragging)
				{
					Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get in-world coordinate of cursor.
					transform.position = new Vector3(cursor.x + dragOffset.x, cursor.y + dragOffset.y, transform.position.z);
				}
			}
			
			if(reset)
			{
				MoveBack();
			}
		}
    }
}
