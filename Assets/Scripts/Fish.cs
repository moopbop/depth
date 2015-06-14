using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	PlayerController player;

	public float speedY;

	public Sprite beer;

	private int rotate;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerController>();

		rotate = Random.Range (1, 3);

		int intbeer = Random.Range (0, 100);

		if (intbeer == 50)
			this.GetComponent<SpriteRenderer> ().sprite = beer;

		if (rotate == 2) {
			Vector3 scale = this.transform.localScale;

			scale.x = -1;

			this.transform.localScale = scale;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 velocity = new Vector2(0, speedY * (player.combo + 1) * player.comboYSpeedMult);

		transform.Translate(velocity * Time.deltaTime);

		if (this.transform.position.y >= 5.5)
			Destroy (gameObject);

	}
}
