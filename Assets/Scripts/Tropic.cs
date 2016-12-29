using System;
using UnityEngine;
using Random = System.Random;

public class Tropic : MonoBehaviour
{
    private static readonly Random rand = new Random();

    private const int SIZE = 27;
    private const float ACCELERATION_FACTOR = 2.1f;

    readonly int[] degreesPerSecond = new int[SIZE];
    readonly float[] degreesАcc = new float[SIZE];
    readonly bool[] active = new bool[SIZE];

    private float acceleration;
    private bool started;

    public void Start() {
        for (var i = 0; i < SIZE; i++) {
            degreesPerSecond[i] = rand.Next(15, 25);
            active[i] = true;
        }
    }

    public void Update() {
        if (started) {
            var finished = true;
            for (var i = 0; i < SIZE; i++) {
                if (!active[i]) continue;
                var item = transform.GetChild(i);
                Item item_ = item.GetComponent<Item>();
                if (360.0f - degreesАcc[i] < 0.5f) {
                    item.position = new Vector3(item_.x, item_.y, item.position.z);
                    active[i] = false;
                    continue;
                }
                var angle = (degreesPerSecond[i] + acceleration) * Time.deltaTime;
                item.position = Quaternion.AngleAxis(angle, Vector3.forward) * item.position;
                degreesАcc[i] += angle;
                finished = false;
            }
            if (finished) {
                started = false;
                Start();
            }
        }


        if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) {
                started = true;
            } else if (Input.GetMouseButton(0)) {
                acceleration += ACCELERATION_FACTOR;
            } else if (Input.GetMouseButtonUp(0)) {

            } else {
                acceleration = (acceleration > 0) ? acceleration - ACCELERATION_FACTOR : 0;
            }
        } else {
            foreach (Touch touch in Input.touches) {
                switch (touch.phase) {
                    case TouchPhase.Began: started = true; break;
                    case TouchPhase.Moved: acceleration += ACCELERATION_FACTOR; break;
                    case TouchPhase.Ended: break;
                }
            }
            if (Input.touchCount == 0) {
                acceleration = (acceleration > 0) ? acceleration - ACCELERATION_FACTOR : 0;
            }
        }
//        Debug.Log(started);

    }

}