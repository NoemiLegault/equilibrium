using UnityEngine;
using System.Collections;

public class ControleCamera : MonoBehaviour {
	public float rayon = 200;
	public float inclinaison = 90; // Angle par rapport à l'axe vertical
	public float azimut = 270; // Angle par rapport à l'axe horizontal 
	public float incRadius =1; // Vitesse que ca va avancer
	public float incAngle=10; // À chaque fois que le joueur touche une fleche: Le mouvement est de X angle
	public float maxInclinaison=170;  // La section du haut est inatteignable
	public float minInclinaison=10; // La section du bas est inatteignable
	public float maxRayon=300; // Max Avancer
	public float minRayon=100; // Max reculer
	public GameObject pointPivot;  // Objet qui determine le centre (sphere)
	public float vitesse=5000; // vitesse (plus chiffre est gros plus ca parait instantané)
	public float vitesseRapprochement=100; // Vitesse de rapprochement (avec molette de la souris)
	float azimutRotationTotal=0; // angle horizontal pour me permettre de positionner la camera (rotation) Changement d'angle par rapport à l'origine
	float inclinaisonRotationTotal=0; // angle vertical pour me permettre de positionner la camera (rotation) Changement d'angle par rapport à l'origine
	// Les fleches tant que le joueur appuie sur les fleches
	bool upArrow = false;
	bool downArrow = false;
	bool rightArrow = false;
	bool leftArrow = false;
	public int vitesseRafraichissement=2; // Permettre de controler un ralentissement du mouvement quand on maintient une fleche enfoncée
	int compteurRafraichissement; // compteur du rafraichissement


	// Use this for initialization
	void Start () {
		compteurRafraichissement = vitesseRafraichissement; // Des la premiere fois que le joueur appuie sur une touche la cam doit se déplacer
	}


	// Update is called once per frame
	void Update () {

		//Gestion des bool si appuyer ou pas
		if (Input.GetKeyDown (KeyCode.UpArrow)) upArrow = true;
		if (Input.GetKeyUp (KeyCode.UpArrow)) upArrow = false;

		if (Input.GetKeyDown (KeyCode.DownArrow)) downArrow = true;
		if (Input.GetKeyUp (KeyCode.DownArrow)) downArrow = false;

		if (Input.GetKeyDown (KeyCode.RightArrow)) rightArrow = true;
		if (Input.GetKeyUp (KeyCode.RightArrow)) rightArrow = false;

		if (Input.GetKeyDown (KeyCode.LeftArrow)) leftArrow = true;
		if (Input.GetKeyUp (KeyCode.LeftArrow)) leftArrow = false;


		rayon = rayon - Input.GetAxis("Mouse ScrollWheel")*vitesseRapprochement; // Changement de la valeur du rayon selon la mollete de la souris et la vitesse d'avancé
		if (rayon < minRayon) rayon = minRayon; // Empêcher d'être trop rapproché du stabile
		if (rayon > maxRayon) rayon = maxRayon; // Empêcher d'être trop éloigné du stabile



		compteurRafraichissement++; // incremente le rafraichissement
		if (compteurRafraichissement <= vitesseRafraichissement) 
			return; // On sort pour limiter la vitesse de rafraichissement

		if (upArrow && inclinaison > minInclinaison) {
			inclinaison = inclinaison - incAngle; // On monte (inclinaison)
			inclinaisonRotationTotal = inclinaisonRotationTotal - incAngle; // l'angle de la cam change
		}
		if (downArrow && inclinaison < maxInclinaison) {
			inclinaison = inclinaison + incAngle; // On descend (inclinaison)
			inclinaisonRotationTotal = inclinaisonRotationTotal + incAngle; // l'angle de la cam change
		}
		if (rightArrow) {
			azimut = azimut + incAngle; // On va vers la droite (azimut)
			azimutRotationTotal = azimutRotationTotal + incAngle; // l'angle de la cam change
		}
		if (leftArrow) {
			azimut = azimut - incAngle; // On va vers la gauche (azimut)
			azimutRotationTotal = azimutRotationTotal - incAngle; // l'angle de la cam change
		}

		// Deplacement + rotation selon le point de pivot
		// Calcul des coordonnees spherique pour l'emplacement de la camera selon le rayon, l'angle de l'azimut et l'angle de l'inclinaison (reference Wikipedia)
		float x = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Cos(degree (azimut))) + pointPivot.transform.position.x;
		float y= (rayon * Mathf.Cos (degree (inclinaison)))  + pointPivot.transform.position.y; 
		float z = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Sin (degree (azimut))) + pointPivot.transform.position.z;
		// Deplacement de la camera a la coordonnee selon les calculs
		Vector3 newPosition = new Vector3 (x, y, z); 
		transform.position= Vector3.MoveTowards(transform.position,newPosition,Time.deltaTime * vitesse);
		// Reajustement de la direction de la camera vers le point de reference en fonction de mouvement deja realise.
		Quaternion targetRotation = Quaternion.Euler (-inclinaisonRotationTotal,-azimutRotationTotal, 0 );
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * vitesse);

		compteurRafraichissement = 0; // Remise à 0 du compteur après le déplacement
	}

	// conversion d'un angle vers un radiant (requis pour la fonction sin et cos)
	private float degree(float angle) { 
		return Mathf.PI * angle / (float)180.0; 
	}
}
