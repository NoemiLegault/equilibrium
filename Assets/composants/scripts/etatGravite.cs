using UnityEngine;
using System.Collections;

public class etatGravite : MonoBehaviour {

	/*-- VARIABLES ------------------------------------------------------------*/

	private bool gravite = false;																// initialise la scène sans gravité

	public GameObject[] tableauDesElements;														// tableau des éléments mobiles
	private Vector3[] positionElement;															// tableau des positions des éléments mobiles
	private Quaternion[] rotationElement; 														// tableau des angles de rotation des éléments mobiles
	//public float vitesse = 1;

	void Start () {

		positionElement = new Vector3[tableauDesElements.Length];								// initialise la longueur du tableau des positions
		rotationElement = new Quaternion[tableauDesElements.Length];							// initialise la longueur du tableau des angles de rotation

		for (int i = 0; i < tableauDesElements.Length; i++) {
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();			// initialise le jeu à zéro gravité 
			etatDuPoids.useGravity = true;	
			//etatDuPoids.useGravity = false;											
			etatDuPoids.isKinematic = true;	
		}

	}

	void Update () {

		/*-- ACTIVER / DÉSACTIVER LA GRAVITÉ -----------------------------------*/

		if (Input.GetKeyDown("space")){															// au clic la touche "espace"

			if (gravite == false){																// si la gravité n'est pas effective
				for (int i = 0; i < tableauDesElements.Length; i++) {							// boucle sur le tableau des éléments mobiles
					Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();	// récupère le Rigidbody du GameObject
					etatDuPoids.useGravity = true;												// active la gravité
					etatDuPoids.isKinematic = false;											// l'élément peut bouger

					positionElement[i] = tableauDesElements[i].transform.position;				// récupère la position de l'élément dans un tableau
					rotationElement[i] = tableauDesElements[i].transform.rotation;				// récupère l'angle de l'élément dans un tableau

					Deselection(tableauDesElements[i]);											// déselection de l'object

				}

				gravite = true;
			}

			else {																				// si la gravité est effective
				for (int i = 0; i < tableauDesElements.Length; i++) {							// boucle sur le tableau des éléments mobiles
					Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();	// récupère le Rigidbody du GameObject
					//etatDuPoids.useGravity = false;												// désactive la gravité
					etatDuPoids.isKinematic = true;												// l'élément ne peut plus bouger
				
					tableauDesElements[i].transform.position = positionElement[i];				// repositionne l'élément à sa position avant gravité
					tableauDesElements[i].transform.rotation = rotationElement[i];				// repositionne l'angle de l'élément avant gravité
					/*
					float retour = vitesse * Time.deltaTime;
					transform.position = Vector3.MoveTowards(tableauDePoids[i].transform.position, positionDuPoid[i], retour);
					*/

				}
				gravite = false;
			}
		}
	}

	void Deselection(GameObject selectionner)
	{
		//== désélection =============================================
		Transform monTransform = selectionner.GetComponent<Transform>();

		// OBJECT 0 ///////////////////////////////////////////////////////////////
		GameObject monObject0 = monTransform.GetChild (0).gameObject;
		monObject0.SetActive (false);
		if (monObject0.name != "selectionner") {
			monObject0.SetActive (true);
		}
		//////////////////////////////////////////////////////////////////////////

		// OBJECT 0 ///////////////////////////////////////////////////////////////
		GameObject monObject1 = monTransform.GetChild (1).gameObject;
		monObject1.SetActive (false);
		if (monObject1.name != "selectionner") {
			monObject1.SetActive (true);
		}
		//////////////////////////////////////////////////////////////////////////
		//==============================================================
	}
}
