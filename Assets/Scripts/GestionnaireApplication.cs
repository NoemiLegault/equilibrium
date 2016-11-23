using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class  GestionnaireApplication : MonoBehaviour {
	

	public void Quit () //Bouton pour sortir du menu (Pour le moment est le bouton de stats) 
	{
		
		UnityEditor.EditorApplication.isPlaying = false; //Test pour quitter le jeu. Si l'éditeur est active, quitter le jeu.
	
	
	}






	/////////////////////// TEST //////////////////////////////////////////////////////////////

	public void demarageNiveau()
	{
		Debug.Log ("allo");
		SceneManager.LoadScene ("niveau_1", LoadSceneMode.Single);

	}



	///////////////////////////////////////////////////////////////////////////////////////////





}
