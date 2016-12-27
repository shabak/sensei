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
//	private int i;

    private static int size = 27;
    Vector3 center = new Vector3(0, 0, 0);
    float[] degreesPerSecond = new float[size];
    Vector3[] v = new Vector3[size];
    GameObject[] rotated = new GameObject[size];
    Random rand = new Random();

    private void Start() {
//		showItem();

        for (var i = 0; i < size; i++)
        {
            degreesPerSecond[i] = rand.Next(35, 75);
            rotated[i] = Instantiate(items[i]);
            rotated[i].transform.position = new Vector3(0, (i/5)+0.2f, 0);
            v[i] = rotated[i].transform.position - center;
        }
    }

    private void Update() {

        for (var i = 0; i < size; i++)
        {
            v[i] = Quaternion.AngleAxis(degreesPerSecond[i] * Time.deltaTime, Vector3.forward) * v[i];
            rotated[i].transform.position = center + v[i];
        }

//        if (Application.isEditor) {
//			// first frame when user click left mouse
//			if (Input.GetMouseButtonDown(0)) {
//				onDragStarted(new Touch());
//			}
//			// every frame user hold on left mouse
//			if (Input.GetMouseButton(0)) {
//				 onDragContinued();
//			}
//			// when mouse is released
//			if (Input.GetMouseButtonUp(0)) {
//				onDragEnded();
//			}
//		} else {
//			foreach (Touch touch in Input.touches) {
//				switch (touch.phase) {
//					case TouchPhase.Began: onDragStarted(touch); break;
//					case TouchPhase.Moved: onDragContinued(); break;
//					case TouchPhase.Ended: onDragEnded(); break;
//				}
//			}
//		}

	}

	void onDragStarted(Touch touch) {		
		Vector3 position = Application.isEditor ? Input.mousePosition : (Vector3)touch.position;  
		RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);
		if (hit2D) {
			dragging = hit2D.collider.gameObject;
			Debug.Log(dragging);
			Item item = (Item)dragging.GetComponent(typeof(Item));
			if (item.draggable) {
				offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragging.transform.position;
				draggingMode = true;
			}
		}
	}
	
	void onDragContinued() {
		if (draggingMode) {
			dragging.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
			Item item = (Item)dragging.GetComponent(typeof(Item));
			// bar.Value = (100f - item.getDistanceEstimate());
			// Debug.Log (item.getDistanceEstimate()); 
			if (item.isPositionAchieved()) {
				// bar.Value = 0;
				item.disposeOnExactPosition();
				onDragEnded();
//				if (i < items.Length) {
//					showItem();
//				} else {
//					//TODO:
//				}
			}
		}
	}
	
	void onDragEnded() {
		draggingMode = false;
	}

	private void showItem() {
//		active = items[i++];
//		float zShift = -i/10.0f;
//		active.transform.position = new Vector3(0, 0, zShift);
//		Instantiate(active);
	} 
}