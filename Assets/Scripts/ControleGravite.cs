using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControleGravite : MonoBehaviour {

	/*-- VARIABLE DES CONDITIONS DE GAMEPLAY --------------------------------------------*/
	private bool gravite = false;																			// la condition gravité est initialisé à false
	private bool clickerBoutonGravite = false;																// la condition bouton à l'écran Activer / Désactiver gravité est initialisé à false
	private bool niveauReussi = true;																		// condition de réussite du niveau
	public float seuilDeReussitePositif = 0.5f;																// limite supérieure de l'intervalle de vélocité à respecter
	public float seuilDeReussiteNegatif = -0.5f;															// limite inférieure de l'intervalle de vélocité à respecter
	public float facteurDeVelociteEnY = 100f;																// facteur d'ajout de velocité en Y
	private float chronometre;																				// chronomète le temps d'équilibre
	public float tempsReussite = 5.0f;

	/*-- VARIABLES DES ÉLÉMEMNTS DU MOBILES ---------------------------------------------*/
	public GameObject[] tableauDesElements;																	// tableau des éléments mobiles
	private Vector3[] positionElement;																		// tableau des positions des éléments mobiles
	private Quaternion[] rotationElement; 																	// tableau des angles de rotation des éléments mobiles

	/*-- VARIABLES DES ÉLÉMENTS DU UI ---------------------------------------------------*/
	public GameObject boutonActiverGravite;																	// bouton d'activation de la gravité
	public GameObject boutonDesactiverGravite;																// bouton de désactivation de la gravité
	public GameObject gagneTexteTest;																		// test affichage GAGNÉ

	////////////////////////////////////////////////////////////////////////////////////////
	/////////////////////////////////    START    //////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////

	void Start () {												
		boutonDesactiverGravite.SetActive(false);															// initialise le bouton "DÉSACTIVER GRAVITÉ" à false
		gagneTexteTest.SetActive(false);																	// initialise le test d'affichage à false

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

		/*-- ACTIVER LA GRAVITÉ -------------------------------------------------------*/

		if (gravite == false && (Input.GetKeyDown ("space") || clickerBoutonGravite == true)) 				// si la gravité est à false et que, space ou clickerBoutonGravite est à true
		{
			activeGravite ();																				// appel de la fonction qui active la gravité
		}
		else if (gravite == true && (Input.GetKeyDown ("space") || clickerBoutonGravite == false))			// si la gravité est à true et que, space ou clickerBoutonGravite est à false
		{
			desactiveGravite ();																			// appel de la fonction qui désactive la gravité
		}
			
		/* -- LORSQUE LA GRAVITÉ EST EFFECTIVE ------------------------------------------*/
		if (gravite == true) {																				// si la gravité est active

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
							desactiveGravite ();
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
					}
				}
			}
				
			/* -- NIVEAU RÉUSSI ---------------------------------------------------------*/
			if (chronometre >= tempsReussite){																// après 5 secondes
				if(niveauReussi){																			// si l'intervalle de vélocité est respecté
					Debug.Log ("GAGNÉ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");								// le joueur a réussi le niveau
					gagneTexteTest.SetActive(true);															// test UI, le joueur a réussi le niveau
				}else{
					desactiveGravite ();
				}
			}
				
		} 																									// fin if(gravite == true)

	} 																										// fin update()

	//////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////   FONCTIONS   ///////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////

	public void checkbouton()																				// au clic du bouton Activer / Désactiver gravité à l'écran
	{
		if (!clickerBoutonGravite) {																		// si le bouton de gravité à l'écran est à false
			clickerBoutonGravite = true;																	// la condition clickerBoutonGravite est à true
		} else {
			clickerBoutonGravite = false;																	// la condition clickerBoutonGravite est à false
		}
	}

	public void activeGravite () {
		for (int i = 0; i < tableauDesElements.Length; i++) {												// boucle sur le tableau des éléments mobiles
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();						// récupère le Rigidbody du GameObject
			etatDuPoids.useGravity = true;																	// active la gravité
			etatDuPoids.isKinematic = false;																// l'élément peut bouger

			positionElement[i] = tableauDesElements[i].transform.position;									// récupère la position de l'élément dans un tableau
			rotationElement[i] = tableauDesElements[i].transform.rotation;									// récupère l'angle de l'élément dans un tableau

			Deselection(tableauDesElements[i]);	
		}
		gravite = true;																						// booléen, la gravité est active
		clickerBoutonGravite = true;
		Debug.Log(gravite);
		boutonActiverGravite.SetActive(false);																// efface le bouton "ACTIVER GRAVITÉ"
		boutonDesactiverGravite.SetActive(true);															// affiche le bouton "DÉSACTIVER GRAVITÉ"
	}

	public void desactiveGravite () {
		for (int i = 0; i < tableauDesElements.Length; i++) {												// boucle sur le tableau des éléments mobiles
			Rigidbody etatDuPoids = tableauDesElements[i].GetComponent<Rigidbody>();						// récupère le Rigidbody du GameObject
			etatDuPoids.useGravity = false;																	// désactive la gravité
			etatDuPoids.isKinematic = true;																	// l'élément ne peut plus bouger

			tableauDesElements[i].transform.position = positionElement[i];									// repositionne l'élément à sa position avant gravité
			tableauDesElements[i].transform.rotation = rotationElement[i];									// repositionne l'angle de l'élément avant gravité

			ResetEcouteurBasPoid(tableauDesElements[i]);	
		}
		chronometre = 0;																					// réinitialise le chronomètre à 0	
		gravite = false;																					// booléen, la gravité est inactive
		clickerBoutonGravite = false;
		Debug.Log(gravite);
		boutonDesactiverGravite.SetActive(false);															// efface le bouton "DÉSACTIVER GRAVITÉ"
		boutonActiverGravite.SetActive(true);																// affiche le bouton "ACTIVER GRAVITÉ"
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
				monEnfant0.SetActive (true);
			}
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
				Collider monCollider = monEnfant0.GetComponent<Collider> ();
				monCollider.isTrigger = true;
			}

			x++;
		}
	}


}
