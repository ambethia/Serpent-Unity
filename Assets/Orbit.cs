using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public GameObject target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	public float scrollSpeed = 5.0f;
	
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	
	public float distanceMin = .5f;
	public float distanceMax = 15f;
	
	float x = 0.0f;
	float y = 0.0f;
	
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
	
	void LateUpdate () {
		if (target) {
			Bounds bounds = CalculateBounds(target);

			if (Input.GetKey(KeyCode.LeftAlt)) {
				x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
				y = ClampAngle(y, yMinLimit, yMaxLimit);
			}
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);			

			if (Input.GetKey(KeyCode.LeftControl))
				distance = Mathf.Clamp(distance - Input.GetAxis("Mouse Y") * scrollSpeed, distanceMin, distanceMax);

			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + bounds.center;
			transform.rotation = rotation;
			transform.position = position;
			transform.LookAt(bounds.center);
		}
	}
	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	Bounds CalculateBounds(GameObject go) {
		Bounds b = new Bounds(go.transform.position, Vector3.zero);
		Object[] rList = go.GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer r in rList) {
			b.Encapsulate(r.bounds);
		}
		return b;
	}
	
}