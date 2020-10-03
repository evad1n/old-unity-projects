using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a vehicle that exhibits flocking behavior with similar vehicles
/// Will Dickinson
/// </summary>
public class Flocker : Vehicle {

    public float avoidWeight = 1;
    public float boundsWeight = 1;
    public float wanderWeight = 1;
    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float separationWeight = 1;
    public GameObject manager;

    private List<GameObject> obstacles;
    private List<GameObject> flockers;
    private List<GameObject> flock;

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    protected override void CalcSteeringForces()
    {
        obstacles = manager.GetComponent<ExerciseManager>().obstacles;
        flockers = manager.GetComponent<ExerciseManager>().flockers;
        flock = GetFlock(flockers);

        Vector3 final = Vector3.zero;

        final += (wanderWeight * Wander());

        final += (avoidWeight * AvoidObstacle(obstacles));

        final += (avoidWeight * Separation(flock));
        final += (alignWeight * Alignment(flock));
        final += (cohesionWeight * Cohesion(flock));

        final += Drag(manager.GetComponent<Resistance>().GetDrag(transform.position));

        if (manager.GetComponent<ExerciseManager>().OutOfBounds(transform.position, boundsDistance))
            final += (boundsWeight * Bounds());

        final = final.normalized * maxForce;

        ApplyForce(final);
    }

    /// <summary>
    /// Gets this objects flock based on its flock radius
    /// </summary>
    /// <param name="others">All flockers in the environment</param>
    /// <returns></returns>
    public List<GameObject> GetFlock(List<GameObject> others)
    {
        List<GameObject> flock = new List<GameObject>();
        foreach (GameObject g in others)
        {
            if (Vector3.Distance(transform.position, g.transform.position) < flockDistance)
            {
                flock.Add(g);
            }
        }
        return flock;
    }
}
