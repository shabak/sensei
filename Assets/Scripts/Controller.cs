using UnityEngine;

public class Controller : MonoBehaviour
{
	// OPACITY: http://answers.unity3d.com/questions/654836/unity2d-sprite-fade-in-and-out.html
	//
	// DPI/PT: https://www.paintcodeapp.com/news/ultimate-guide-to-iphone-resolutions
	// 
	// RAYCAST 2D: http://forum.unity3d.com/threads/raycast2d-first-hit.228679/

	public GameObject active;
	public GameObject active2;
	public GameObject dragging;
	public Vector3 offset; //vector between touchpoint/mouseclick to object center
	public bool draggingMode = false;
	public GameObject[] items;
	private int i = 0;
	// ProgressBarBehaviour bar;

	void Start () {
		// GameObject go = GameObject.Find("ProgressBarLabelAbove");
		// bar = go.GetComponent<ProgressBarBehaviour>();
		showItem();		
	}

	void Update() {
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
				if (i < items.Length) {
					showItem();					
				} else {
					//TODO:
				}
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

// TODO: Was changed Collider to Collider 2D!
// Proof: https://www.reddit.com/r/Unity3D/comments/23cx9g/why_choose_2d_colliders_over_3d_colliders_in_a_2d/
// void onDragStarted(Touch touch) {
// 	// convert mouse click position to a ray
// 	Ray ray = Application.isEditor ? Camera.main.ScreenPointToRay(Input.mousePosition) : Camera.main.ScreenPointToRay(touch.position);
// 	RaycastHit hit;
// 	// a bit extended active area for touch screen, for editor - presice area
// 	if (Application.isEditor ? Physics.Raycast(ray, out hit) : Physics.SphereCast(ray, 0.3f, out hit)) {
// 		active = hit.collider.gameObject;
// 		// point - active item center
// 		offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - active.transform.position;
// 		draggingMode = true;
// 	}
// }
// void onDragContinued() {
// 	if (draggingMode) {
// 		Vector3 newGOCenter2 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
// 		active.transform.position = new Vector3(newGOCenter2.x, newGOCenter2.y, 0.0f);
// 	}
// }