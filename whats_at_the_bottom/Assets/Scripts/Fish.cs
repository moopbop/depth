using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	PlayerController player;

	public float speedY;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 velocity = this.GetComponent<Rigidbody2D> ().velocity;

		velocity.y = speedY + (player.combo * player.comboYSpeedMult);

		this.GetComponent<Rigidbody2D> ().velocity = velocity;

		if (this.transform.position.y >= 5.5)
			Destroy (gameObject);

	}
}
