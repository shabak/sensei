using UnityEngine;

public class Item : MonoBehaviour {

	public float x;
	public float y;
	public bool draggable = true;

	void Start () {
		draggable = true;
	}
	
	void Update () { 
	}

	// TODO: correct method 
	public bool isPositionAchieved() {
		Vector2 position = getPos();
		// float absPosX = Mathf.Abs(position.x);
		// float absPosY = Mathf.Abs(position.y);
		// float absX = Mathf.Abs(x);
		// float absY = Mathf.Abs(y);
		// float dX = absPosX - absX;
		// float dY = absPosY - absY;
		// bool achievedX = dX < delta;
		// bool achievedY = dY < delta;
		// bool achieved = achievedX && achievedY;
		bool achievedX = getDistance(position.x, x) < 0.04f;
		bool achievedY = getDistance(position.y, y) < 0.04f;
		bool achieved = achievedX && achievedY;
		// Debug.Log (achieved);
		return achieved;
	}	

	public void disposeOnExactPosition() {
		// TODO: sound & effects
		Vector3 targetPosition = new Vector3(x, y, transform.position.z);
		transform.position = targetPosition;
		// GetComponent<Rigidbody2D>().MovePosition(targetPosition);

		draggable = false;
	}

	public float getDistanceEstimate() {
		Vector2 position = getPos();
		return ((getDistance(position.x, x) + getDistance(position.y, y)) / 2.0f) * 100f;
	}

	private Vector2 getPos() {
		return GetComponent<Transform>().transform.position;
	}

	private float getDistance(float position, float origin) {
		return Mathf.Abs(Mathf.Abs(position) - Mathf.Abs(origin));
	}
}
