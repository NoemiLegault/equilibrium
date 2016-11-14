using UnityEngine;
using System.Collections;

public class oeillet : MonoBehaviour {

	public GameObject gestionGravite;

	void OnTriggerEnter(Collider maCollision)
	{
		//Debug.Log (maCollision.gameObject.tag);
		if (maCollision.gameObject.tag == "tige") {
			Debug.Log ("check !");
			gestionGravite.SendMessageUpwards ("retourPositionActuelle", SendMessageOptions.DontRequireReceiver);
		}
	}

}
