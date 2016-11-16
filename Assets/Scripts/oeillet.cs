using UnityEngine;
using System.Collections;

public class oeillet : MonoBehaviour {

	void OnTriggerEnter(Collider maCollision)
	{
		//Debug.Log (maCollision.gameObject.tag);
		if (maCollision.gameObject.tag == "tige") {
			Debug.Log ("check !");
			Collider monCollider = this.GetComponent<Collider> ();
			monCollider.isTrigger = false;
		}
	}


}
