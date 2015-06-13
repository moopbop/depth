using UnityEngine;
using System.Collections;

/*
 * References:
 * http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html
 */
public class PlayerController : MonoBehaviour
{
	#region public variables
	[Tooltip("Speed multiplier")]
	public float acceleration;
	
	[Tooltip("Side-to-side velocity cap")]
	public float maxXVel;					// Not affected by combo.
	
	[Tooltip("Vertical velocity cap")]
	public float maxYVel;					// Will be affected by combo.
	
	[Tooltip("Combo multiplier")]
	public int combo;						// Public for ease of coding, will be made private on release.
	
	[Tooltip("Rate to increase Y velocity per combo level")]
	public float comboYSpeedMult;
	
	[Tooltip("Maximim air value. Player dies when they have 0 air")]
	public float maxAir;
	
	public float airLossPerSecond;
	
	// For clamping player movement, will be made private after tinkering is done
	public float minXPos;
	public float maxXPos;
	public float minYPos;
	public float maxYPos;
	#endregion
	
	#region private variables
	private float xMove;
	private float yMove;
	private Rigidbody2D rb;
	private Vector2 velocity;
	private float air;
	#endregion
	
	void Start()
	{
		xMove = 0;
		yMove = 0;
		rb = this.GetComponent<Rigidbody2D>();
		velocity = new Vector2(0, 0);
		air = maxAir;
	}
	
	void Update()
	{
		#region get input
		xMove = Input.GetAxis("Horizontal");
		yMove = Input.GetAxis("Vertical");
		#endregion
		
		// Decrease air
		if (air > 0) air -= Time.deltaTime * airLossPerSecond;
		Debug.Log(air);
		
		// Die if no air
		if (air <= 0)
		{
			Die();
		}
	}
	
	void FixedUpdate()
	{
		velocity = new Vector2(xMove * acceleration, yMove * acceleration);
		
		// Not very elegant, but it works
		if (velocity.x > maxXVel)
		{
			velocity.x = maxXVel;
		}
		if (velocity.y > maxYVel)
		{
			velocity.y = maxYVel;
		}
	}
	
	void LateUpdate()
	{
		// Apply movement
		transform.Translate(velocity * Time.deltaTime);
		
		// Clamp position
		float clampedX = Mathf.Clamp(this.transform.position.x, minXPos, maxXPos);
		float clampedY = Mathf.Clamp(this.transform.position.y, minYPos, maxYPos);
		Vector2 clampedPosition = new Vector2(clampedX, clampedY);
		
		this.transform.position = clampedPosition;
	}
	
	private void Die()
	{
		Application.LoadLevel(1);
	}
}
