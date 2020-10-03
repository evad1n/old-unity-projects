using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

    public Vector3 position = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(0, 0, 0);
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, 0, 0);

    public float mass = 10;
    public float maxSpeed = 5;
    public float maxForce = 200f;
    public float safeDistance = 3;

    protected abstract void CalcSteeringForces();

	// Use this for initialization
	public virtual void Start () {
        position = transform.position;
	}

    // Update is called once per frame
    public void Update () {
        CalcSteeringForces();
        UpdatePosition();
        SetTransform();
    }

    public void UpdatePosition()
    {
        position = transform.position;

        velocity += acceleration * Time.deltaTime;

        velocity = velocity.normalized * maxSpeed;

        position += velocity * Time.deltaTime;

        direction = velocity.normalized;
        Debug.DrawLine(position, position + direction);

        acceleration = Vector3.zero;

        transform.position = position;
    }

    public void SetTransform()
    {
        transform.forward = direction;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - transform.position;

        desired = desired.normalized * maxSpeed;

        Vector3 seek = desired - velocity;

        return seek;
    }

    public Vector3 Flee(Vector3 target)
    {
        Vector3 desired = target - transform.position;

        desired = desired.normalized * maxSpeed;

        Vector3 flee = velocity - desired;

        return flee;
    }

    public Vector3 AvoidObstacle(Vector3 obstacle, float radius)
    {
        Vector3 toObstacle = obstacle - transform.position;

        Vector3 right = transform.position + transform.right;

        Vector3 rightToObstacle = obstacle - right;

        Debug.DrawLine(transform.position, right, Color.magenta);

        if(toObstacle.magnitude < safeDistance)
        {
            float dot = Vector3.Dot(rightToObstacle, right);

            if (rightToObstacle.magnitude < radius)
            {
                if (dot > 0)
                {
                    return transform.right * maxSpeed * (1 - toObstacle.magnitude / safeDistance) * - 1;
                }
                else if (dot < 0)
                {
                    return transform.right * maxSpeed * (1 - toObstacle.magnitude / safeDistance);
                }

            }
        }
        return Vector3.zero;
    }

    public Vector3 Wander()
    {
        //Find position half a second from now
        Vector3 future = position += velocity * 0.5f;

        return Vector3.zero;
    }
}
