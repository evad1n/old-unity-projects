using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a human vehicle that will evade zombies that are close and wander around.
/// Will Dickinson
/// </summary>
public class Human : Vehicle {

    public float fleeWeight;
    public float avoidWeight;
    public float boundsWeight;
    public float wanderWeight;
    public GameObject fleeTarget;
    public GameObject manager;
    public GameObject future;
    public Material forward;
    public Material right;

    private List<GameObject> obstacles;
    private List<GameObject> humans;
    private List<GameObject> zombies;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        future = Instantiate(future);
        future.transform.SetParent(this.transform);
    }

    protected override void CalcSteeringForces()
    {
        obstacles = manager.GetComponent<ExerciseManager>().obstacles;
        humans = manager.GetComponent<ExerciseManager>().humans;
        zombies = manager.GetComponent<ExerciseManager>().zombies;

        Vector3 final = Vector3.zero;

        if(Vector3.Distance(FindClosest().transform.position, transform.position) < 15)
        {
            final += (fleeWeight * Evade(fleeTarget));
            haste = true;
        }
        else
        {
            haste = false;
            final += (wanderWeight * Wander());
            Debug.DrawLine(transform.position, transform.position + Wander(), Color.cyan);
            Debug.Log("human wander");
        }

        Vector3 avoid = AvoidObstacle(obstacles);

        if (avoid != Vector3.zero)
        {
            Debug.DrawLine(transform.position, transform.position + avoid, Color.green);
            Debug.Log("avoiding");
            final += (avoidWeight * avoid);
        }

        Vector3 separate = Separation(humans);

        if(separate != Vector3.zero)
        {
            Debug.DrawLine(transform.position, transform.position + separate, Color.white);
            final += (avoidWeight * Separation(humans));
        }

        if (manager.GetComponent<ExerciseManager>().OutOfBounds(transform.position, boundsDistance))
            final += (boundsWeight * Bounds());

        final = final.normalized * maxForce;

        ApplyForce(final);
    }

    public GameObject FindClosest()
    {
        GameObject closest = zombies[0];
        foreach (GameObject g in zombies)
        {
            if (Vector3.Distance(g.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = g;
            }
        }

        fleeTarget = closest;

        return closest;
    }

    void OnRenderObject()
    {
        if (manager.GetComponent<ExerciseManager>().debug)
        {
            //Draw forward vector
            forward.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.forward);
            GL.End();

            //Draw right vector
            right.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.right);
            GL.End();

            //Draw future position
            future.SetActive(true);
            future.transform.position = transform.position + (velocity * futureDistance);
        }
        else
        {
            future.SetActive(false);
        }
    }
}
