using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControleGravite : MonoBehaviour {

	/*-- VARIABLES ----------------------------------------------------------------------*/

	private bool gravite = false;																			// initialise la scène sans gravité
	private bool niveauReussi = true;																		// condition de réussite du niveau

	public GameObject[] tableauDesElements;																	// tableau des éléments mobiles
	private Vector3[] positionElement;																		// tableau des positions des éléments mobiles
	private Quaternion[] rotationElement; 																	// tableau des angles de rotation des éléments mobiles

	public float nbtiges;

	private float chronometre;																				// chronomète le temps d'équilibre

	public float seuilDeReussitePositif = 0.5f;																// limite supérieure de l'intervalle de vélocité à respecter
	public float seuilDeReussiteNegatif = -0.5f;															// limite inférieure de l'intervalle de vélocité à respecter
	public float facteurDeVelociteEnY = 100f;																// facteur d'ajout de velocité en Y

	public float tempsReussite = 5.0f;




	////////////////////////////////////////////////////////////////////////////////////////
	/////////////////////////////////    START    //////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		positionElement = new Vector3[tableauDesElements.Length];											// initialise la longueur du tableau des positions
		rotationElement = new Quaternion[tableauDesElements.Length];										// initialise la longueur du tableau des angles de rotation

		for (int i = 0; i < tableauDesElements.Length; i++) {
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();						// récupère le Rigidbody du GameObject
			etatDuPoids.useGravity = false;																	// initialise le jeu à zéro gravité
		}

	}

	////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////   UPDATE   //////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

	void Update () {

		/*-- ACTIVER / DÉSACTIVER LA GRAVITÉ ---------------------------------------------*/

		if (Input.GetKeyDown("space")){																		// au clic la touche "espace"

			if (gravite == false){																			// si la gravité n'est pas effective
				positionActuelle ();																		// appel de la fonction qui récupère la position des éléments

			}
				
			else {																							// si la gravité est effective
				retourPositionActuelle ();																	// appel de la fonction qui replace les éléments
			}
		}

		/* -- LORSQUE LA GRAVITÉ EST EFFECTIVE ------------------------------------------*/
		if (gravite == true) {																			// si la gravité est active


			// oeillet (text noemi)//////////////////////////////////////////////////////
			for (int e = 0; e < tableauDesElements.Length; e++) 
			{	
				
				Transform monTransform = tableauDesElements[e].GetComponent<Transform> ();
				int x = 0;
				while (x < monTransform.childCount) 
				{
					GameObject monEnfant0 = monTransform.GetChild (x).gameObject;
					if (monEnfant0.tag == "basPoid") 
					{
						Collider monCollider = monEnfant0.GetComponent<Collider>();
						if (!monCollider.isTrigger) 
						{
							Debug.Log ("OEILLET TOUCHER !!!!! RETOUR EN MODE FIX !!!!");
							retourPositionActuelle ();
						}
					}

					x++;
				}
			}
		

			/////////////////////////////////////////////////////////////////////////////
			 
			
			
			chronometre += Time.deltaTime;																	// démarre le chronomètre

			for (int i = 0; i < tableauDesElements.Length; i++) {											// pour tous les éléments mobiles
				Rigidbody rigidbodyElement = tableauDesElements[i].GetComponent<Rigidbody>();        		// récupère le Rigidbody du GameObject
				float velociteElementEnY = rigidbodyElement.velocity.y;										// récupère la valeur de la vélocité en Y

				/* -- AJOUT DE FORCE SUR LES GAMEOBJECTS --------------------------------*/
				if (chronometre >= 0.3f) {																	// après 0.3 secondes
					if (velociteElementEnY < seuilDeReussiteNegatif) {										// si la vélocité en y est inférieur au seuil de réussite négatif
						//float ajoutDeForce =  Mathf.Abs(velociteDuPoidsEnY * facteurDeVelociteEnY);
						rigidbodyElement.AddForce (Vector3.down * facteurDeVelociteEnY, ForceMode.Force);	// ajoute le facteur de vélocité en Y aux GameObjects
					}
				}
					
				/* -- CONDITION DE RÉUSSITE DU NIVEAU -----------------------------------*/
				if (chronometre >= 1.0f) {																	// après 1 seconde
					if (velociteElementEnY < seuilDeReussiteNegatif || 
						velociteElementEnY >seuilDeReussitePositif) {										// si la vélocité dépasse l'intervalle de réussite
						niveauReussi = false;																// le niveau n'est pas réussi
						Debug.Log ("la velocité dépasse le seuil de réussite ???????????????????");
						//indicateurReussite.text = "PERDU";
					}
				}
			}
				
			/* -- NIVEAU RÉUSSI ---------------------------------------------------------*/
			if (chronometre >= tempsReussite){																		// après 5 secondes
				if(niveauReussi){																			// si l'intervalle de vélocité est respecté
					Debug.Log ("GAGNÉ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
					// le joueur a réussi le niveau
				}else{
					retourPositionActuelle ();
				}
			}
				
		} // fin if(gravite == true)

	} // fin update() -----------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////   FONCTIONS   ///////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////


	void positionActuelle () {
		for (int i = 0; i < tableauDesElements.Length; i++) {												// boucle sur le tableau des éléments mobiles
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();						// récupère le Rigidbody du GameObject
			etatDuPoids.useGravity = true;																	// active la gravité
			etatDuPoids.isKinematic = false;																// l'élément peut bouger

			positionElement[i] = tableauDesElements[i].transform.position;									// récupère la position de l'élément dans un tableau
			rotationElement[i] = tableauDesElements[i].transform.rotation;									// récupère l'angle de l'élément dans un tableau

			Deselection(tableauDesElements[i]);	
			//Debug.Log (tableauDesElements [i]);
		}
		gravite = true;																						// booléen, la gravité est active
	}

	void retourPositionActuelle () {
		for (int i = 0; i < tableauDesElements.Length; i++) {												// boucle sur le tableau des éléments mobiles
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();						// récupère le Rigidbody du GameObject
			etatDuPoids.useGravity = false;																	// désactive la gravité
			etatDuPoids.isKinematic = true;																	// l'élément ne peut plus bouger

			tableauDesElements[i].transform.position = positionElement[i];									// repositionne l'élément à sa position avant gravité
			tableauDesElements[i].transform.rotation = rotationElement[i];									// repositionne l'angle de l'élément avant gravité

			ResetEcouteurBasPoid(tableauDesElements[i]);	
			/*
			float retour = vitesse * Time.deltaTime;
			transform.position = Vector3.MoveTowards(tableauDePoids[i].transform.position, positionDuPoid[i], retour);
			*/
		}
		chronometre = 0;																					// réinitialise le chronomètre à 0
		gravite = false;																					// booléen, la gravité est inactive
	}


	void Deselection(GameObject selectionner)
	{
		Transform monTransform2 = selectionner.GetComponent<Transform> ();
		int x = 0;
		while(x < monTransform2.childCount)
		{
			GameObject monEnfant0 = monTransform2.GetChild(x).gameObject;
			monEnfant0.SetActive(false);
			if (monEnfant0.name != "selectionner") 
			{
				//Debug.Log (monEnfant0.name);
				monEnfant0.SetActive (true);
			}

			//monEnfant0 = null;

			x++;
		}
	}
		
	void ResetEcouteurBasPoid(GameObject selectionner)
	{
		Transform monTransform2 = selectionner.GetComponent<Transform> ();
		int x = 0;
		while(x < monTransform2.childCount)
		{
			GameObject monEnfant0 = monTransform2.GetChild(x).gameObject;
			if (monEnfant0.tag == "basPoid") 
			{
				//Debug.Log (monEnfant0.name);
				Collider monCollider = monEnfant0.GetComponent<Collider> ();
				monCollider.isTrigger = true;
			}

			x++;
		}
	}


}
