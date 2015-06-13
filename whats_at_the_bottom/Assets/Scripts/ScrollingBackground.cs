using UnityEngine;
using System.Collections;

// Source: https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/2d-scrolling-backgrounds
public class ScrollingBackground : MonoBehaviour
{
	public float scrollSpeed;
	
	void Update()
	{
		float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2(0, y);
		this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
