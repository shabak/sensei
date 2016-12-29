using System;
using UnityEngine;
using Random = System.Random;

public class Tropic : MonoBehaviour
{
    private static readonly Random rand = new Random();

    private const int SIZE = 27;
    readonly int[] degreesPerSecond = new int[SIZE];
    readonly float[] acc = new float[SIZE];

    public void Start() {
        for (var i = 0; i < SIZE; i++) {
            degreesPerSecond[i] = rand.Next(25, 45);
        }
    }

    public void Update() {

        for (var i = 0; i < SIZE; i++) {
            var item = transform.GetChild(i);
            Item item_ = item.GetComponent<Item>();
            if (!item_.active) continue;
            var angle = degreesPerSecond[i] * Time.deltaTime;
            item.position = Quaternion.AngleAxis(angle, Vector3.forward) * item.position;
            acc[i] += angle;
            if (!(360.0f - acc[i] < 0.5f)) continue;
            item.position = new Vector3(item_.x, item_.y, item.position.z);
            item_.active = false;
        }
	}

}