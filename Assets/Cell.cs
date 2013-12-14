using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public bool isActive = false;
	public float bounce = 1.70158f;

	public int x;
	public int y;

	public Color normal;
	public Color highlight;

	void Awake() {
		renderer.material.color = normal;
	}

	void OnMouseDown() {
		ToggleActivation();
	}

	void OnMouseEnter() {
		if (Input.GetMouseButton(0)) {
			ToggleActivation();
		}
		StartCoroutine("HighlightColor");
	}

	void OnMouseExit() {
		StartCoroutine("RestoreColor");
	}

	public void ToggleActivation()
	{
		if (isActive) {
			Deactivate();
		} else {
			Activate();
		}
	}

	IEnumerator RestoreColor()
	{
		StopCoroutine("HighlightColor");
		float duration = 0.5f;
		float elapsed = 0;
		
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			renderer.material.color = Color.Lerp(highlight, normal, elapsed/duration);
			yield return 0;
		}
		renderer.material.color = normal;
	}

	IEnumerator HighlightColor()
	{
		StopCoroutine("RestoreColor");
		float duration = 0.25f;
		float elapsed = 0;
		
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			Color.Lerp(normal, highlight, elapsed/duration);
			yield return 0;
		}
		renderer.material.color = highlight;
		yield return new WaitForSeconds(1);
		StartCoroutine("RestoreColor");
	}

	public void Deactivate()
	{
		StopCoroutine("MoveTo");
		StartCoroutine("MoveTo", 0.0f);
		isActive = false;
	}

	public void Activate()
	{
		StopCoroutine("MoveTo");
		StartCoroutine("MoveTo", 0.66f);
		isActive = true;
	}

	IEnumerator MoveTo(float y)
	{
		var start = transform.localPosition;
		Vector3 target = new Vector3(0, (y - start.y), 0);

		float duration = 0.5f;
		float elapsed = 0;

		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localPosition = EaseOut(elapsed, start, target, duration);
			yield return 0;
		}
		transform.localPosition = start + target;
	}


	Vector3 EaseOut(float t, Vector3 b, Vector3 c, float d)
	{
		return c * ( ( t = t / d - 1 ) * t * ( ( bounce + 1 ) * t + bounce ) + 1 ) + b;
	}

	Vector3 EaseIn(float t, Vector3 b, Vector3 c, float d)
	{
		return c * ( t /= d ) * t * ( ( bounce + 1 ) * t -  bounce) + b;
	}
}
