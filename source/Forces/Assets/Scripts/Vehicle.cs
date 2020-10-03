using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    public Vector3 position = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(0, 0, 0);
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, 0, 0);

    public float mass = 50;

    public Vector3 wind = Vector3.zero;
    public Vector3 gravity = new Vector3(0, -1, 0);

    public bool friction;

    private Vector3 prevPos;

	// Use this for initialization
	void Start () {
        prevPos = position;
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        ApplyForce(wind);
        ApplyForce(gravity * mass);

        if (Input.GetMouseButton(0))
            ApplyMouseForce();

        Bounce();

        if(friction)
            ApplyFriction(0.5f);

        position = transform.position;

        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        direction = velocity.normalized;

        acceleration = Vector3.zero;

        transform.position = position;

        prevPos = position;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;

        friction *= coeff;

        acceleration += friction;
    }

    private void ApplyMouseForce()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;

        Vector3 force = mousePos - transform.position;

        acceleration += force / mass;
    }

    private void Bounce()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenPos.y, screenPos.z));
            velocity.x *= -1;
        }
        else if (screenPos.x < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, screenPos.y, screenPos.z));
            velocity.x *= -1;
        }
        else if (screenPos.y > Screen.height)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, Screen.height, screenPos.z));
            velocity.y *= -1;
        }
        else if (screenPos.y < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, 0, screenPos.z));
            velocity.y *= -1;
        }
    }
}
