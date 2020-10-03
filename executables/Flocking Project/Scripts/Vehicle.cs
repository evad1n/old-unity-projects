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
    public float boundsDistance = 5;
    public float separationDistance = 3;
    public float futureDistance = 0.5f;
    public float flockDistance = 6;
    public float radius = 1;

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

    /// <summary>
    /// Steer towards the target
    /// </summary>
    /// <param name="target">The object to seek</param>
    /// <returns></returns>
    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - transform.position;

        desired = desired.normalized * maxSpeed;

        Vector3 seek = desired - velocity;

        return seek;
    }

    /// <summary>
    /// Steer away from the target
    /// </summary>
    /// <param name="target">The object to flee from</param>
    /// <returns></returns>
    public Vector3 Flee(Vector3 target)
    {
        Vector3 desired = target - transform.position;

        desired = desired.normalized * maxSpeed;

        Vector3 flee = velocity - desired;

        return flee;
    }

    /// <summary>
    /// Steer away from obstacles that are too close
    /// </summary>
    /// <param name="obstacles">An impassable terrain object</param>
    /// <returns></returns>
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

    /// <summary>
    /// Seek the targets future position
    /// </summary>
    /// <param name="target">What is being pursued</param>
    /// <returns></returns>
    public Vector3 Pursue(GameObject target)
    {
        Vector3 future = target.transform.position + (target.GetComponent<Vehicle>().velocity * futureDistance);

        return Seek(future);
    }

    /// <summary>
    /// Flee the targets future position
    /// </summary>
    /// <param name="target">What is being evaded</param>
    /// <returns></returns>
    public Vector3 Evade(GameObject target)
    {
        Vector3 future = target.transform.position + (target.GetComponent<Vehicle>().velocity * futureDistance);

        return Flee(future);
    }

    /// <summary>
    /// A method to stay in bounds
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Travel randomly around the environment
    /// </summary>
    /// <returns></returns>
    public Vector3 Wander()
    {
        //Find position half a second from now
        Vector3 center = position += velocity * 0.5f;
        Vector3 point = Random.insideUnitSphere;
        point.Normalize();
        point *= 3;

        point += center;

        return Seek(point);
    }

    /// <summary>
    /// Flockers will stay near the center of their local flock
    /// </summary>
    /// <param name="flock">The group of flockers near this object</param>
    public Vector3 Cohesion(List<GameObject> flock)
    {
        Vector3 center = Vector3.zero;

        foreach (GameObject g in flock)
        {
            center += g.transform.position;
        }

        center /= flock.Count;

        return Seek(center);
    }

    /// <summary>
    /// All flockers will face the same average direction
    /// </summary>
    /// <param name="flock">The group of flockers near this object</param>
    public Vector3 Alignment(List<GameObject> flock)
    {
        Vector3 direction = Vector3.zero;

	    foreach(GameObject g in flock)
        {
		    direction += g.GetComponent<Vehicle>().velocity;
	    }


        return Seek(transform.position + direction);
    }
}
