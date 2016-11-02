using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
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
	
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && inclinaison > minInclinaison) inclinaison = inclinaison - incAngle;
		if (Input.GetKeyDown (KeyCode.DownArrow) && inclinaison < maxInclinaison) inclinaison = inclinaison + incAngle;
		if (Input.GetKeyDown (KeyCode.RightArrow)) azimut = azimut + incAngle;
		if (Input.GetKeyDown (KeyCode.LeftArrow)) azimut = azimut - incAngle;
		if (azimut > maxAzimut) azimut = minAzimut;
		if (azimut < minAzimut) azimut = maxAzimut;
		float x = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Cos(degree (azimut))) + pointPivot.transform.position.x;
		float y= (rayon * Mathf.Cos (degree (inclinaison)))  + pointPivot.transform.position.y;
		float z = (rayon * Mathf.Sin (degree (inclinaison)) * Mathf.Sin (degree (azimut))) + pointPivot.transform.position.z;
		Vector3 newPosition = new Vector3 (x, y, z);
		transform.position= Vector3.MoveTowards(transform.position,newPosition,Time.deltaTime * vitesse);
	}

	private float degree(float angle) {
		return Mathf.PI * angle / (float)180.0;
	}
}
