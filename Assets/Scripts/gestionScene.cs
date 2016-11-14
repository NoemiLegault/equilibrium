using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gestionScene : MonoBehaviour {

	public void Jouer(){
		SceneManager.LoadScene ("test_1");
	}
}
