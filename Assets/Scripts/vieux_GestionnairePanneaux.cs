
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class vieux_GestionnairePanneaux : MonoBehaviour {  //Définition du menu actuel. Panneau

		public Animator initialementOuvert;  //Panneau: State Ouvert 

		private int m_OuvrirParametreId;
		private Animator m_Ouvert;
		private GameObject m_PrecedemmentSelectionne;

		const string k_OuvertTransitionNom = "Open"; //Ouvrir transition 
		const string k_FermeEtatNom = "Closed";  //Ferme

		private GameObject MenuDroiteContenu; // variable qui contien de GameObject ouvert dans la partie de doite du menu


		// --- var test ----------------------- //

		// ------------------------------------ //


		/*

	public void OnEnable()   //Quand est activée
	{
		m_OuvrirParametreId = Animator.StringToHash (k_OuvertTransitionNom); //StringToHash(stringName), est généré à l'aide de Animator

		if (initialementOuvert == null)
			return;

		OpenPanel(initialementOuvert);
	}

	public void OpenPanel (Animator anim)  //Ouvrir l'animation si le panneau est ouvert. 
	{
		if (m_Ouvert == anim)
			return;

		anim.gameObject.SetActive(true);
		var newPrecedemmentSelectionne = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

		CloseCurrent(); //Ferme la selection current.

		m_PrecedemmentSelectionne = newPrecedemmentSelectionne; 

		m_Ouvert = anim;
		m_Ouvert.SetBool(m_OuvrirParametreId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject); //Rechercher le premier bouton activé sélectionnable.

		SetSelected(go);
	}
		

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject aller = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				aller = selectable.gameObject;
				break;
			}
		}
		return aller;
	}

	public void CloseCurrent()
	{
		if (m_Ouvert == null)
			return;

		m_Ouvert.SetBool(m_OuvrirParametreId, false);
		SetSelected(m_PrecedemmentSelectionne);
		StartCoroutine(DesactiverPanneauSupprime(m_Ouvert));
		m_Ouvert = null;
	}

	IEnumerator DesactiverPanneauSupprime(Animator anim)  // Création de transitions d'écrans
	{
		bool closedSateReached = false;
		bool wantToClose = true;
		while (!closedSateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedSateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_FermeEtatNom);

			wantToClose = !anim.GetBool(m_OuvrirParametreId);

			yield return new WaitForEndOfFrame();
		}  

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
	*/




		// --- controle menu de droite  -------------------------------------------------------------------
		public void affichage(GameObject monGameObject)
		{
			/*
		Transform monTransform = monGameObject.GetComponent<Transform> ();
		Transform parent = monTransform.parent;
		resetAffichage (parent.gameObject);
		//UnityEngine.UI.Text monText = this.GetComponent<UnityEngine.UI.Text>();
		GameObject monGameObject = monTransform.gameObject;
		*/
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
