using UnityEngine;
using System.Collections;

public class ControleCamera : MonoBehaviour {
	public float rayon = 16;
	public float inclinaison = 90;
	public float azimut = 270;
	public float incRadius =1;
	public float incAngle=10;
	public float maxInclinaison=170;
	public float minInclinaison=10;
	public float maxAzimut=359;
	public float minAzimut=0;
	public float maxRayon=20;
	public float minRayon=5;
	public GameObject pointPivot;
	public float vitesse=500;
	float azimutRotationTotal=0;
	float inclinaisonRotationTotal=0;
	bool upArrow = false;
	bool downArrow = false;
	bool rightArrow = false;
	bool leftArrow = false;
	public int vitesseRafraichissement=3; 
	int compteurRafraichissement; // compteur du rafraichissement
	// Use this for initialization
	
	// Use this for initialization
	void Start () {
		compteurRafraichissement = vitesseRafraichissement; // Des la premiere fois que le joueur appuie sur une touche la cam doit se déplacer
	}

	// Update is called once per frame
	void Update () {
		Quaternion targetRotation;
		if (Input.GetKeyDown (KeyCode.UpArrow)) upArrow = true;
		if (Input.GetKeyUp (KeyCode.UpArrow)) upArrow = false;

		if (Input.GetKeyDown (KeyCode.DownArrow)) downArrow = true;
		if (Input.GetKeyUp (KeyCode.DownArrow)) downArrow = false;

		if (Input.GetKeyDown (KeyCode.RightArrow)) rightArrow = true;
		if (Input.GetKeyUp (KeyCode.RightArrow)) rightArrow = false;

		if (Input.GetKeyDown (KeyCode.LeftArrow)) leftArrow = true;
		if (Input.GetKeyUp (KeyCode.LeftArrow)) leftArrow = false;

		compteurRafraichissement++;
		if (compteurRafraichissement <= vitesseRafraichissement)
			return;

		if (upArrow && inclinaison > minInclinaison) {
			inclinaison = inclinaison - incAngle;
			inclinaisonRotationTotal = inclinaisonRotationTotal - incAngle;
		}
		if (downArrow && inclinaison < maxInclinaison) {
			inclinaison = inclinaison + incAngle;
			inclinaisonRotationTotal = inclinaisonRotationTotal + incAngle;
		}
		if (rightArrow) {
			azimut = azimut + incAngle;
			azimutRotationTotal = azimutRotationTotal + incAngle;
		}
		if (leftArrow) {
			azimut = azimut - incAngle;
			azimutRotationTotal = azimutRotationTotal - incAngle;
		}
		if (azimut > maxAzimut) azimut = minAzimut;
		if (azimut < minAzimut) azimut = maxAzimut;
		float x = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Cos(degree (azimut))) + pointPivot.transform.position.x;
		float y= (rayon * Mathf.Cos (degree (inclinaison)))  + pointPivot.transform.position.y;
		float z = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Sin (degree (azimut))) + pointPivot.transform.position.z;
		Vector3 newPosition = new Vector3 (x, y, z);
		transform.position= Vector3.MoveTowards(transform.position,newPosition,Time.deltaTime * vitesse);
		targetRotation = Quaternion.Euler (-inclinaisonRotationTotal,-azimutRotationTotal, 0 );
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * vitesse);

		compteurRafraichissement = 0;
	}

	private float degree(float angle) {
		return Mathf.PI * angle / (float)180.0;
	}
}
