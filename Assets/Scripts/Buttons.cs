using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public int scene;
	
	public void clickededed(){

		if (scene != 4)
			Application.LoadLevel (scene);
		else
			Application.Quit ();

	}

}
