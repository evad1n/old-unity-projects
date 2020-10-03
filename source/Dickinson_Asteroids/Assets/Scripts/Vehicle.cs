using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Used to simulate vehicular movement and acceleration
/// </summary>

public class Vehicle : MonoBehaviour {

	[SerializeField]
	private Vector3 vehiclePosition = new Vector3(0,0,0);
	public Vector3 direction = new Vector3(0, 1, 0);
	public Vector3 velocity = new Vector3(0, 0, 0);

    public Vector3 acceleration = new Vector3(0, 0, 0);
    public float accelRate = 0.005f;
    public float maxSpeed = 0.1f;
    public float decelaration = 0.95f;

    public float rotationSpeed = 3f;
    public float totalRotation = 90;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        HandleInput();

        vehiclePosition = transform.position;

        //Debug.DrawLine(transform.position, direction);

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		vehiclePosition += velocity;
		transform.position = vehiclePosition;

        gameObject.GetComponent<Wrap>().velocity = velocity;
    }

    /// <summary>
    /// Takes user input
    /// </summary>
    void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            acceleration = accelRate * direction;
            velocity += acceleration;
        }
        else
        {
            velocity *= decelaration;
        }


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction = Quaternion.Euler(0, 0, rotationSpeed) * direction;
            totalRotation += rotationSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction = Quaternion.Euler(0, 0, -rotationSpeed) * direction;
            totalRotation -= rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
        direction.Normalize();
    }

    /// <summary>
    /// Resets velocity vectors and brings to a standstill in the center of the screen
    /// </summary>
    public void Reset()
    {
        direction = new Vector3(0, 1, 0);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
        totalRotation = 90;

        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
    }
}
