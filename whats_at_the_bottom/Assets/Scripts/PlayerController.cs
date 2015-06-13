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

	[Tooltip("The time before combo reset the instant a combo is started")]
	public float maxComboTimer;

	[Tooltip("The fish prefab we are copying for instantiation")]
	public GameObject fish;

	[Tooltip("The bubble prefab we are copying for instantiation")]
	public GameObject bubble;
	
	public float airLossPerSecond;
	public float airIncreaseOnPopBubble;
	public LayerMask bubbleMask;
	
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
	private float comboTimer;
	private Vector3 fishPosition;
	private float fishTimer;
	private Vector3 bubblePosition;
	private float bubbleTimer;
	private int maxCombo;
	private float originalMaxYVel;
	#endregion
	
	void Start()
	{
		xMove = 0;
		yMove = 0;
		rb = this.GetComponent<Rigidbody2D>();
		velocity = new Vector2(0, 0);
		air = maxAir;
		fishTimer = 0;
		bubbleTimer = 0;
		maxCombo = 12;
		originalMaxYVel = maxYVel;
	}
	
	void Update()
	{
		#region get input
		xMove = Input.GetAxis("Horizontal");
		yMove = Input.GetAxis("Vertical");
		#endregion
		
		// Cap air
		if (air > maxAir) air = maxAir;
		
		// Decrease air
		if (air > 0) air -= Time.deltaTime * airLossPerSecond;
		
		// Die if no air
		if (air <= 0)
		{
			Die();
		}
		
		Debug.Log(air);
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

		if (comboTimer > 0)
			comboTimer -= Time.deltaTime;

		if (comboTimer <= 0) {
			combo = 0;
			maxYVel = originalMaxYVel;
		}
		if (fishTimer > 0)
			fishTimer -= Time.deltaTime;

		if (fishTimer <= 0)
			SpawnFish ();

		if (bubbleTimer > 0)
			bubbleTimer -= Time.deltaTime;

		if (bubbleTimer <= 0)
			SpawnBubble ();

		maxYPos = this.transform.parent.position.y + 4.65f;
		minYPos = this.transform.parent.position.y - 4.6f;
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
	
	void OnTriggerEnter2D(Collider2D col)
	{
		string colTag = col.gameObject.tag;
		
		if (colTag == "Bubble")
		{
			air += airIncreaseOnPopBubble;
			Object.Destroy(col.gameObject);
		}

		if (colTag == "Fish") {
			if (combo < maxCombo)
			{
				combo++;
			}
			comboTimer = maxComboTimer;
			Object.Destroy(col.gameObject);
		}
	}
	
	private void Die()
	{
		Application.LoadLevel(1);
	}

	private void SpawnFish()
	{
		fishPosition = new Vector3 (Random.Range (minXPos, maxXPos), GameObject.Find("FishSpawn").transform.position.y, -1.1f);
		GameObject.Instantiate (fish, fishPosition, Quaternion.identity);
		fishTimer = .5f;
	}

	private void SpawnBubble()
	{
		bubblePosition = new Vector3 (Random.Range (minXPos, maxXPos), Random.Range (minYPos, maxYPos), -1.1f);
		GameObject.Instantiate (bubble, bubblePosition, Quaternion.identity);
		bubbleTimer = 7f;
	}

	public float getAir()
	{
		return air;
	}
}
