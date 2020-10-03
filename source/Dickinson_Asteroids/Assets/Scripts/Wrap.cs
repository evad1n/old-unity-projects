using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes object wrap to the other side of the screen when they fall off the edge
/// </summary>
public class Wrap : MonoBehaviour {

    public Vector3 screenPos;
    public Vector3 velocity;

    [Range(0.0f, 0.5f)]
    public float offset;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        screenPos = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 newPos = transform.position;

        if (screenPos.x > 1 + offset && velocity.x > 0)
            newPos.x = -newPos.x;
        else if (screenPos.x < -offset && velocity.x < 0)
            newPos.x = -newPos.x;
        else if (screenPos.y > 1 + offset && velocity.y > 0)
            newPos.y = -newPos.y;
        else if (screenPos.y < -offset && velocity.y < 0)
            newPos.y = -newPos.y;

        transform.position = newPos;
    }
}
