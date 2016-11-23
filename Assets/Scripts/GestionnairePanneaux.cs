using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GestionnairePanneaux : MonoBehaviour {  //Définition du menu actuel. Panneau

	private GameObject MenuDroiteContenu; // variable qui contien de GameObject ouvert dans la partie de doite du menu



	// --- controle menu de droite  -------------------------------------------------------------------
	public void affichage(GameObject monGameObject)
	{
		InitialisationMenuDroite ();
		MenuDroiteContenu = monGameObject;
		monGameObject.SetActive(true);

	}

	void InitialisationMenuDroite()
	{
		if (MenuDroiteContenu && MenuDroiteContenu != null) 
		{
			MenuDroiteContenu.SetActive (false);
			MenuDroiteContenu = null;
		}
	}




/////////////////////// TEST //////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////

	
	
	
}
