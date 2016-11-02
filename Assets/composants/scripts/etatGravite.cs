using UnityEngine;
using System.Collections;

public class etatGravite : MonoBehaviour {

	/*-- VARIABLES ------------------------------------------------------------*/

	private bool gravite = false;												// initialise la scène sans gravité

	/*-- plusieurs poids  --*/

	//public Rigidbody[] tableauDePoids;
	public GameObject[] tableauDePoids;
	//private Vector3[] tableauDernierePositionPoids;





	void Start () {

		/*-- INITIALISE A GRAVITÉ DES ÉLÉMENTS À FALSE ------------------------*/

		for (int i = 0; i < tableauDePoids.Length; i++) {
			Rigidbody etatDuPoids = tableauDePoids[i].GetComponent<Rigidbody>();
			etatDuPoids.useGravity = false;
		
			//tableauDePoids[i].useGravity = false;


		}

	}

	void Update () {

		if (Input.GetKeyDown("space")){

			if (gravite == false){
				Debug.Log("gravite = false");
				for (int i = 0; i < tableauDePoids.Length; i++) {
					Rigidbody etatDuPoids = tableauDePoids[i].GetComponent<Rigidbody>();
					etatDuPoids.useGravity = true;
					etatDuPoids.isKinematic = false;

					/*
					Transform positionDuPoid = tableauDePoids[i].GetComponent<Transform>();

					Debug.Log (positionDuPoid);*/


				}	

				/*
				tableauDernierePositionPoids[i] = tableauDePoids[i].transform.position;
				Debug.Log (tableauDernierePositionPoids[i]);
				*/


				gravite = true;
			}
				
			else {
				Debug.Log("gravite = true");
				for (int i = 0; i < tableauDePoids.Length; i++) {
					Rigidbody etatDuPoids = tableauDePoids[i].GetComponent<Rigidbody>();
					etatDuPoids.useGravity = false;
					etatDuPoids.isKinematic = true;

					// movetoward
				}
				gravite = false;
			}
				
		}




	
	}
}
