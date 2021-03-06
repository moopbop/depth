﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * References:
 * http://deranged-hermit.blogspot.com/2014/01/2d-platformer-collision-detection-with.html
 */
public class PlayerController : MonoBehaviour
{
	#region public variables
	public float acceleration;
	public float maxXVel;					// Not affected by combo.
	public float maxYVel;					// Will be affected by combo.
	public float comboYSpeedMult;
	
	public int combo;						// Public for ease of coding, will be made private on release.
	public float maxComboTimer;

	
	public float maxAir;
	public GameObject bubble;
	public float airLossPerSecond;
	public float airIncreaseOnPopBubble;
	public LayerMask bubbleMask;
	public float bubbleSpawnTime;
	
	public GameObject fish;
	
	public GameObject shark;
	public float sharkStartSpawnDistance;
	
	public AudioClip fishHitAudio;
	public AudioClip deathAudio;
	public AudioClip bubbleGetAudio;
	
	// For clamping player movement, will be made private after tinkering is done
	public float minXPos;
	public float maxXPos;
	public float minYPos;
	public float maxYPos;
	#endregion
	
	#region private variables
	private float xMove;
	private float yMove;
	private Vector2 velocity;
	
	private float air;
	private float comboTimer;
	
	private Vector3 fishPosition;
	private float fishTimer;
	
	private Vector3 sharkPosition;
	private bool spawnSharks;
	private float sharkTimer;
	
	private Vector3 bubblePosition;
	private float bubbleTimer;

	private float score;
	private float baseScore;
	
	private CameraShake shake;
	#endregion
	
	void Start()
	{
		xMove = 0;
		yMove = 0;
		velocity = new Vector2(0, 0);
		air = maxAir;
		fishTimer = 0;
		bubbleTimer = 0;
		spawnSharks = false;
		sharkTimer = 0;
		baseScore = 25f;
		shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
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

		// Combo
		if (comboTimer > 0)
			comboTimer -= Time.deltaTime;

		if (comboTimer <= 0)
			combo = 0;

		// Fish
		if (fishTimer > 0)
			fishTimer -= Time.deltaTime;

		if (fishTimer <= 0)
			SpawnFish ();

		// Bubbles
		if (bubbleTimer > 0)
			bubbleTimer -= Time.deltaTime;

		if (bubbleTimer <= 0)
			SpawnBubble ();
		
		// Sharks
		if (sharkTimer > 0)
			sharkTimer -= Time.deltaTime;

		if (score >= 5000f)
		{
			spawnSharks = true;
		}

		if (sharkTimer <= 0 && spawnSharks)
		{
			SpawnShark();
		}

		score += Time.deltaTime * (baseScore * (combo + 1));

		GameObject.Find ("txtScore").GetComponent<Text> ().text = score.ToString ("###################################");

		if (score >= 10000f)
			Application.LoadLevel (4);
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
			if (bubbleGetAudio != null) AudioSource.PlayClipAtPoint(bubbleGetAudio, this.transform.position);
		}

		if (colTag == "Fish") {
			combo++;
			comboTimer = maxComboTimer;
			Object.Destroy(col.gameObject);
			if (fishHitAudio != null)  AudioSource.PlayClipAtPoint(fishHitAudio, this.transform.position);
			shake.Shake();
		}
		
		if (colTag == "Shark")
		{
			Die();
		}
	}
	
	private void Die()
	{
		if (deathAudio != null) AudioSource.PlayClipAtPoint(deathAudio, this.transform.position);
		Object.Destroy(this);
		Application.LoadLevel (5);
	}

	private void SpawnFish()
	{
		fishPosition = new Vector3 (Random.Range (minXPos, maxXPos), -6, -1.1f);
		GameObject.Instantiate (fish, fishPosition, Quaternion.identity);
		fishTimer = .5f;
	}
	
	private void SpawnShark()
	{
		sharkPosition = new Vector3(Random.Range(minXPos, maxXPos), -6, -1.1f);
		GameObject.Instantiate(shark, sharkPosition, Quaternion.identity);
		sharkTimer = 3.5f;
	}

	private void SpawnBubble()
	{
		bubblePosition = new Vector3 (Random.Range (minXPos, maxXPos), Random.Range (minYPos, maxYPos), -1.1f);
		GameObject.Instantiate (bubble, bubblePosition, Quaternion.identity);
		bubbleTimer = bubbleSpawnTime;
	}

	public float getAir()
	{
		return air;
	}
}
