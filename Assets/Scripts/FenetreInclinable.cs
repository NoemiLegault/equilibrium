using UnityEngine;

public class FenetreInclinable : MonoBehaviour    //Fenêtre d'inclinaison
{
	public Vector2 range = new Vector2(5f, 3f); //Vector2-représenter des positions et des vecteurs 2D.


	Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	void Start ()
	{
		mTrans = transform;
		mStart = mTrans.localRotation;
	}

	void Update ()
	{
		Vector3 pos = Input.mousePosition; //Vector3 Passer les positions 3D et les directions autour.

		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
		mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

		mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f); //Position dans le monde. Retourne une rotation.
	}
}
