using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  
using System.Collections;

public class BoutonUIAction : MonoBehaviour {
	// Use this for initialization
	private UnityEngine.UI.Text monTextBouton;
	void Start () {
		monTextBouton = this.GetComponent<UnityEngine.UI.Text> ();
		monTextBouton.color = Color.white;
	}
		

	public void couleurOver(){
		Debug.Log ("hover");
		monTextBouton.color = Color.yellow;
		Debug.Log (monTextBouton.color);
	}

	public void couleurOut(){
		Debug.Log ("out");
		monTextBouton.color = Color.white;
		Debug.Log (monTextBouton.color);
	}
}
