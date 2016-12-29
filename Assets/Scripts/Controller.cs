using System;
using UnityEngine;
using Random = System.Random;

public class Controller : MonoBehaviour
{
	// OPACITY: http://answers.unity3d.com/questions/654836/unity2d-sprite-fade-in-and-out.html
	//
	// DPI/PT: https://www.paintcodeapp.com/news/ultimate-guide-to-iphone-resolutions
	// 
	// RAYCAST 2D: http://forum.unity3d.com/threads/raycast2d-first-hit.228679/

	public GameObject active;
	public GameObject dragging;
	public Vector3 offset; //vector between touchpoint/mouseclick to object center
	public bool draggingMode;
	public GameObject[] items;
	private int i;

    private void Start() {
		showItem();
    }

    private void Update() {
        if (Application.isEditor) {
			// first frame when user click left mouse
			if (Input.GetMouseButtonDown(0)) {
				onDragStarted(new Touch());
			}
			// every frame user hold on left mouse
			if (Input.GetMouseButton(0)) {
				 onDragContinued();
			}
			// when mouse is released
			if (Input.GetMouseButtonUp(0)) {
				onDragEnded();
			}
		} else {
			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
					case TouchPhase.Began: onDragStarted(touch); break;
					case TouchPhase.Moved: onDragContinued(); break;
					case TouchPhase.Ended: onDragEnded(); break;
				}
			}
		}
	}

	void onDragStarted(Touch touch) {		
		Vector3 position = Application.isEditor ? Input.mousePosition : (Vector3)touch.position;  
		RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);
		if (hit2D) {
			dragging = hit2D.collider.gameObject;
			Debug.Log(dragging);
			Item item = (Item)dragging.GetComponent(typeof(Item));
			if (item.active) {
				offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragging.transform.position;
				draggingMode = true;
			}
		}
	}
	
	void onDragContinued() {
		if (draggingMode) {
			dragging.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
			Item item = (Item)dragging.GetComponent(typeof(Item));
			// Debug.Log (item.getDistanceEstimate());
			if (item.isPositionAchieved()) {
				item.disposeOnExactPosition();
				onDragEnded();
			}
		}
	}
	
	void onDragEnded() {
		draggingMode = false;
	}

	private void showItem() {
		active = items[i++];
		float zShift = -i/10.0f;
		active.transform.position = new Vector3(0, 0, zShift);
		Instantiate(active);
	} 
}