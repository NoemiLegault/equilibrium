using UnityEngine;
using System.Collections;

public class ActiveStateToggler : MonoBehaviour {  //Toggler d'état actif

	public void ToggleActive () {
		gameObject.SetActive (!gameObject.activeSelf);// GameObject.activeSelf active, état sera utilisé une fois que tous les parents seront actifs.

	}
}
