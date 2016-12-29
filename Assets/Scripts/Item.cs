using UnityEngine;

public class Item : MonoBehaviour {

	public float x;
	public float y;
	public bool active = true;

	void Start () {
		active = true;
	}
	
	void Update () {
	}

	public bool isPositionAchieved() {
		return Vector3.Distance(transform.position, new Vector2(x, y)) < 0.04f;
	}	

	public void disposeOnExactPosition() {
		Vector3 targetPosition = new Vector3(x, y, transform.position.z);
		transform.position = targetPosition;
		active = false;
	}

//	public float getDistanceEstimate() {
//		Vector2 position = getPos();
//		return ((getDistance(position.x, x) + getDistance(position.y, y)) / 2.0f) * 100f;
//	}

	private Vector2 getPos() {
		return GetComponent<Transform>().transform.position;
	}

	private static float getDistance(float position, float origin) {
		return Mathf.Abs(Mathf.Abs(position) - Mathf.Abs(origin));
	}
}
