using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class deplacementObject : MonoBehaviour {

	public Transform[] ObjectTransformTable;
	public GameObject maCam;
	private Camera cam;

	public UnityEngine.UI.Text selectionAffichageNom;
	private GameObject selectionner;
	public GameObject[] modulesLayers;

	private bool mouseDown = false;

	public int[] nbEnfants;
	public GameObject[] parents;
	public GameObject[] enfants;

	private GameObject monParent;
	private GameObject monEnfant;
	private GameObject sousModule;

	private Vector3 maPosition;
	//private int parentposition; 
	//private bool limites = true;
	private float difference = 0.0F;
	private float distanceCompilation;
	private float distanceMax;

	//private Camera maCam;
	// Use this for initialization
	void Start () 
	{
		//Time.timeScale = 0;
		// code inspirer de : 
		// https://docs.unity3d.com/ScriptReference/EventSystems.EventTrigger.html
		// http://answers.unity3d.com/questions/519708/check-oncollisionenter-on-other-gameobject.html
		// https://unity3d.com/fr/learn/tutorials/topics/scripting/events
		//public delegate void OnTriggerEnter();

		//GetComponent<EventTrigger>( );

		cam = maCam.GetComponent<Camera>();
		//Debug.Log (cam);
	}

	// Update is called once per frame
	void Update () 
	{

		if (Input.GetMouseButtonUp (0)) 
		{
			mouseDown = false;
			//parentposition = null;
			monParent = null;
		}


		//Debug.Log (ObjectsTable.Length);
		if (Input.GetMouseButtonDown (0)) 
		{
			mouseDown = true;
			maPosition = Input.mousePosition;
			RaycastHit hit;

			Ray ray = this.cam.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) 
			{
				//Debug.Log (hit.collider.name);
				if (hit.collider != null) 
				{


					if (selectionner) 
					{
						//mesLimitesColliders = null;
						//== désélection =============================================
						Transform monTransform = selectionner.GetComponent<Transform> ();
						int x = 0;
						while(x < monTransform.childCount)
						{
							GameObject monEnfant0 = monTransform.GetChild (x).gameObject;
							monEnfant0.SetActive (true);
							if (monEnfant0.name != "deselectionner") 
							{
								monEnfant0.SetActive (false);
							}

							x++;
						}

					}
													/*
													// OBJECT 0 ///////////////////////////////////////////////////////////////
													GameObject monObject0 = monTransform.GetChild (0).gameObject;
													monObject0.SetActive (false);
													if (monObject0.name != "selectionner") 
													{
														monObject0.SetActive (true);
													}
													//////////////////////////////////////////////////////////////////////////
													
													// OBJECT 0 ///////////////////////////////////////////////////////////////
													GameObject monObject1 = monTransform.GetChild (1).gameObject;
													monObject1.SetActive (false);
													if (monObject1.name != "selectionner") 
													{
														monObject1.SetActive (true);
													}
													//////////////////////////////////////////////////////////////////////////
													/// */
													//==============================================================

					// INITIALISATION NOUVELLE SELECTION 
					selectionner = hit.collider.gameObject;
					Transform monTransform2 = selectionner.GetComponent<Transform> ();
					int y = 0;
					while(y < monTransform2.childCount)
					{
						GameObject monEnfant1 = monTransform2.GetChild (y).gameObject;
						monEnfant1.SetActive (true);
						if (monEnfant1.name == "deselectionner") 
						{
							monEnfant1.SetActive (false);
						}

						y++;
					}

					Debug.Log (selectionner);
					int nb = 0;
					for (int p = 0; p < parents.Length; p++)
					{	int e = 0;	
						/*
						while (enfants [e]) 
						{
							if (enfants [e] == selectionner) 
							{
								monParent = parents [p];
								monEnfant = enfants [e];

								Transform parentTransform = monParent.GetComponent<Transform> ();
								Transform enfantTransform = monEnfant.GetComponent<Transform> ();

								distanceMax = parentTransform.localScale.y;
								distanceCompilation = Vector3.Distance (parentTransform.position, enfantTransform.position);

								parentposition = p;

								int m;
								for (m = 0; m < modulesLayers.Length; m++)
								{
									if(modulesLayers[m] == enfantTransform.parent)
									{
										sousModule = modulesLayers [m];
									}
								}





								nb++;
								break;
							}
							e++;
						}
						*/
						while (enfants [e]) 
						{
							if (enfants [e] == selectionner) 
							{
								monParent = parents [p];
								monEnfant = enfants [e];

								Transform parentTransform = monParent.GetComponent<Transform> ();
								Transform enfantTransform = monEnfant.GetComponent<Transform> ();
								float d =  Vector3.Distance (parentTransform.position, enfantTransform.position);

								Vector3 worldScale = transform.localScale;
								Vector3 monScale = Vector3.Scale(worldScale,parentTransform.localScale);
								//Debug.Log (distanceMax);
								distanceMax = monScale.y;
								//Debug.Log ("monScale : " + monScale);
								Debug.Log (parentTransform.position - enfantTransform.position);
								Transform TransformCam = maCam.GetComponent<Transform> ();
								/*
								//if (((parentTransform.position.x - enfantTransform.position.x) + (parentTransform.position.z - enfantTransform.position.z)) > 0)
								if(TransformCam.rotation.y > 90 && TransformCam.rotation.y < 270)
								{
									//distanceCompilation = distanceMax / 2 + d;
								} 
								else 
								{
									//distanceCompilation = distanceMax / 2 - d;
								}

								//parentposition = p;
								/*
								int m;
								for (m = 0; m < modulesLayers.Length; m++)
								{
									if(modulesLayers[m] == enfantTransform.parent)
									{
										sousModule = modulesLayers [m];
									}
								}
								*/

								nb++;
								break;
							}
							e++;
					
						}

					}
						

					selectionAffichageNom.text = selectionner.name; //affichage du nom de la nouvelle selection

				}
			}
		} 

		if (mouseDown == true && monParent != null)
		{
			//Debug.Log (selectionner);
			/*
			int nb = 0;
			for (int e = nb; e < (nbEnfants [parentposition]); e++) 
			{
				if (enfants [e] == selectionner) 
				{
					maPosition = Input.mousePosition;
					break;
				}
			}
			*/
			if (maPosition != Input.mousePosition) 
			{
				
				//Debug.Log (maPosition);
				//Debug.Log ("mousePosition =" + Input.mousePosition);
				difference = (maPosition.x - Input.mousePosition.x)/100;


				Transform parentTransform = monParent.GetComponent<Transform> ();
				Transform monTransform = monEnfant.GetComponent<Transform> ();
				//Transform module = parentTransform.parent;
				Transform moduleTransform = parentTransform.parent;
				//Transform moduleTransform = module.GetComponent<Transform> ();


				//float d =  Vector3.Distance (parentTransform.position, monTransform.position);


				Vector3 maPositionSelection = monTransform.position;
				//maPositionSelection.x = (maPositionSelection.x + difference/2);
				//monTransform.position = maPositionSelection;
				Transform TransformCam = cam.GetComponent<Transform> ();
				Vector3 maCamRotation = TransformCam.eulerAngles;
				//Debug.Log("MA ROTATION DE CAM = " + maCamRotation.y);
				if ((distanceCompilation + difference) > 0 || (distanceCompilation + difference) < distanceMax) 
				{
					if(maCamRotation.y > 90 && maCamRotation.y < 270)
					{
						monTransform.Translate (Vector3.right * difference);
					} 
					else 
					{
						monTransform.Translate (Vector3.right * -difference);
					}
					/*
					//if (moduleTransform.rotation.y < -180 || moduleTransform.rotation.y > 0) 
					Debug.Log("MA ROTATION DE CAM" + TransformCam.rotation);
					if(TransformCam.rotation.y > 90 && TransformCam.rotation.y < 270)
					{
						monTransform.Translate (Vector3.right * difference);
					} 
					else 
					{
						monTransform.Translate (Vector3.right * -difference);
					}
					distanceCompilation += difference;
					//Debug.Log (distanceCompilation);
					*/
					distanceCompilation += difference;
				}
			}	
		}
	} // fin void update();




	/*
	void OnMouseDown (){
		mouseDown = true;
	}

	void OnMouseUp (){
		mouseDown = false;
	}
	*/
	/*
	void OnTriggerEnter (Collider monCollider)
	{
		if (monCollider.gameObject.name == "limites")
		{
			Debug.Log ("allo");
			limites = false;
			Transform monTransform = monEnfant.GetComponent<Transform> ();
			Vector3 maPositionSelection = monTransform.position;
			maPositionSelection.x = maPositionSelection.x-(maPositionSelection.x + difference/2);
			monTransform.position = maPositionSelection;
		}
	}

	void OnTriggerExit (Collider monCollider)
	{
		if (monCollider.gameObject.name == "limites")
		{
			limites = true;
		}
	}
	*/





}
