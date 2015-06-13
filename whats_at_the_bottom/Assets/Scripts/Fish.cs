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
	
		Vector2 velocity = new Vector2(0, speedY * (player.combo + 1) * player.comboYSpeedMult);

		transform.Translate(velocity * Time.deltaTime);

		if (this.transform.position.y >= 5.5)
			Destroy (gameObject);

	}
}
