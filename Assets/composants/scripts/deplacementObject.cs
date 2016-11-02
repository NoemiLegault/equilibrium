using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class deplacementObject : MonoBehaviour {

	public Transform[] ObjectTransformTable;
	public Camera cam;
	public UnityEngine.UI.Text selectionAffichageNom;
	private GameObject selectionner;
	// Use this for initialization
	void Start () {
	//Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (ObjectsTable.Length);
		/*
		foreach (GameObject x in ObjectsTable) {
			Debug.Log (x);
		}
		*/
		if (Input.GetMouseButtonDown (0)) {
			//Ray ray = new Ray (cam.transform.position, Input.mousePosition);
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log (hit.collider.name);
				if (hit.collider != null) {
					if (selectionner) {
						Transform monTransform = selectionner.GetComponent<Transform> ();

						// OBJECT 0 ///////////////////////////////////////////////////////////////
						GameObject monObject0 = monTransform.GetChild(0).gameObject;
						monObject0.SetActive (false);
						if (monObject0.name != "selectionner") {
							monObject0.SetActive (true);
						}
						//////////////////////////////////////////////////////////////////////////

						// OBJECT 0 ///////////////////////////////////////////////////////////////
						GameObject monObject1 = monTransform.GetChild(1).gameObject;
						monObject1.SetActive (false);
						if (monObject1.name != "selectionner") {
							monObject1.SetActive(true);
						}
						//////////////////////////////////////////////////////////////////////////

					}

					selectionner = hit.collider.gameObject;
					Transform monTransform2 = selectionner.GetComponent<Transform> ();

					// OBJECT 0 ///////////////////////////////////////////////////////////////
					GameObject monEnfant0 = monTransform2.GetChild(0).gameObject;
					monEnfant0.SetActive (false);
					if (monEnfant0.name == "selectionner") {
						monEnfant0.SetActive (true);
					}
					//////////////////////////////////////////////////////////////////////////

					// OBJECT 0 ///////////////////////////////////////////////////////////////
					GameObject monEnfant1 = monTransform2.GetChild(1).gameObject;
					monEnfant1.SetActive (false);
					if (monEnfant1.name == "selectionner") {
						monEnfant1.SetActive(true);
					}
					//////////////////////////////////////////////////////////////////////////

					selectionAffichageNom.text = selectionner.name;
				}
			}
		}
	}
	/*
	void OnMouseDown (){
		foreach (Transform x in ObjectTransformTable) {
			//Debug.Log (x);
		}
	}
	*/
}
