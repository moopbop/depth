using UnityEngine;
using System.Collections;

// Source: https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/2d-scrolling-backgrounds
//edited by Jacob because the original version would cause "lag" when a combo was gained or lost
public class ScrollingBackground : MonoBehaviour
{
	public float scrollSpeed;
	PlayerController player;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<PlayerController>();
	}

	void Update()
	{
		float currentOffsetY = this.GetComponent<Renderer> ().sharedMaterial.GetTextureOffset ("_MainTex").y;
		float y = Time.deltaTime * (scrollSpeed - player.combo * player.comboYSpeedMult);
		Vector2 offset = new Vector2(0, currentOffsetY + y);
		this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
