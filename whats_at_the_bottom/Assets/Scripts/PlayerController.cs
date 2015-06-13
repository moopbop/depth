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
	#endregion
	
	#region private variables
	private float xMove;
	private float yMove;
	private Rigidbody2D rb;
	private Vector2 velocity;
	#endregion
	
	void Start()
	{
		xMove = 0;
		yMove = 0;
		rb = this.GetComponent<Rigidbody2D>();
		velocity = new Vector2(0, 0);
	}
	
	void Update()
	{
		#region get input
		xMove = Input.GetAxis("Horizontal");
		yMove = Input.GetAxis("Vertical");
		#endregion
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
		transform.Translate(velocity * Time.deltaTime);
	}
}
