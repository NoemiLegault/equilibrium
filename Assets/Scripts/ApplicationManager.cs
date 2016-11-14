using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {
	

	public void Quit () //Bouton pour sortir du menu (Pour le moment est le bouton de stats) 
	{
		
		UnityEditor.EditorApplication.isPlaying = false; //Test pour quitter le jeu. Si l'éditeur est active, quitter le jeu.
	
	
	}
}
