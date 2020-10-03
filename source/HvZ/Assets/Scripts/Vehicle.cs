using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a vehicle with multiple steering behaviors. Can be inherited from.
/// Will Dickinson
/// </summary>
public abstract class Vehicle : MonoBehaviour {

    public Vector3 position = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(0, 0, 0);
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, 0, 0);

    public float mass = 10;
    public float maxSpeed = 10;
    public float wanderSpeed = 5;
    public float maxForce = 200f;
    public float obstacleDistance = 10;
    public float boundsDistance = -5;
    public float separationDistance = 3;
    public float futureDistance = 0.5f;
    public float radius = 1;
    public bool haste = false;

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

        if (haste)
            velocity = velocity.normalized * maxSpeed;
        else
            velocity = velocity.normalized * wanderSpeed;

        position += velocity * Time.deltaTime;

        direction = velocity.normalized;

        acceleration = Vector3.zero;

        transform.position = position;
    }

    public void SetTransform()
    {
        if(direction != Vector3.zero)
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

    public Vector3 AvoidObstacle(List<GameObject> obstacles)
    {
        List<GameObject> close = new List<GameObject>();

        foreach (GameObject g in obstacles)
        {
            if (Vector3.Distance(g.transform.position, transform.position) < obstacleDistance)
            {
                close.Add(g);
            }
        }

        Vector3 steer = Vector3.zero;

        foreach (GameObject g in close)
        {
            steer += Flee(g.transform.position);
        }

        return steer;
    }

    public Vector3 Pursue(GameObject target)
    {
        Vector3 future = target.transform.position + (target.GetComponent<Vehicle>().velocity * futureDistance);

        return Seek(future);
    }

    public Vector3 Evade(GameObject target)
    {
        Vector3 future = target.transform.position + (target.GetComponent<Vehicle>().velocity * futureDistance);

        return Flee(future);
    }

    public Vector3 Bounds()
    {
        //Go towards center of park
        return new Vector3(0, 20, 0) - transform.position;
    }

    /// <summary>
    /// Separate from other game objects that are too close
    /// </summary>
    /// <param name="neighbors">A list of similar game objects</param>
    /// <returns></returns>
    public Vector3 Separation(List<GameObject> neighbors)
    {
        List<GameObject> close = new List<GameObject>();

        foreach(GameObject g in neighbors)
        {
            if (Vector3.Distance(g.transform.position, transform.position) < separationDistance && g != this.gameObject)
            {
                close.Add(g);
            }
        }

        Vector3 steer = Vector3.zero;

        foreach (GameObject g in close)
        {
            steer += Flee(g.transform.position);
        }

        return steer;
    }

    public Vector3 Wander()
    {
        //Find position half a second from now
        Vector3 center = position += velocity * 0.5f;
        Vector3 point = Random.insideUnitSphere;
        point.Normalize();
        point /= 2;

        point += center;

        return point - transform.position;
    }
}
