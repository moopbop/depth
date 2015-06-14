using UnityEngine;
using System.Collections;

// Source: http://www.mikedoesweb.com/2012/camera-shake-in-unity/
public class CameraShake : MonoBehaviour {

   private Vector3 originPosition;
   private Quaternion originRotation;
   public float shake_decay;
   public float shake_intensity;
 
	 void Start()
	 {
		 originPosition = this.transform.position;
		 originRotation = this.transform.rotation;
	 }
 
   void Update (){
      if (shake_intensity > 0){
         transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
         transform.rotation = new Quaternion(
         originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
         shake_intensity -= shake_decay;
      }
	  else
	  {
		  this.transform.position = originPosition;
		  this.transform.rotation = originRotation;
	  }
   }
 
   public void Shake(){
      shake_intensity = .05f;
	  shake_decay = 0.02f;
   }
}
