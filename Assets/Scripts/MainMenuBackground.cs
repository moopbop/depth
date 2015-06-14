using UnityEngine;
using System.Collections;

public class MainMenuBackground : MonoBehaviour {
		
	public float scrollSpeed;

	void Update () {
		float currentOffsetY = this.GetComponent<Renderer> ().sharedMaterial.GetTextureOffset ("_MainTex").y;
		float y = Time.deltaTime * (scrollSpeed);
		Vector2 offset = new Vector2(0, currentOffsetY + y);
		this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
