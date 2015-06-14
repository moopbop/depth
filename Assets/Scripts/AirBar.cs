using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AirBar : MonoBehaviour {

	PlayerController player;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {

		this.GetComponent<Slider> ().value = player.getAir ();

	}
}
