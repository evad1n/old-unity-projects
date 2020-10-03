using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a zombie vehicle that will pursue the nearest human
/// Will Dickinson
/// </summary>
public class Zombie : Vehicle {

    public float seekWeight;
    public float avoidWeight;
    public float boundsWeight;
    public float wanderWeight;
    public GameObject target;
    public GameObject manager;
    public GameObject future;
    public Material forward;
    public Material right;
    public Material tracking;

    private List<GameObject> obstacles;
    private List<GameObject> humans;
    private List<GameObject> zombies;

    // Use this for initialization
    public override void Start () {
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

        if (humans.Count != 0)
        {
            haste = true;
            final += (seekWeight * Pursue(FindClosest()));
            Debug.Log("pursuing");
        }
        else
        {
            haste = false;
            final += (wanderWeight * Wander());
            Debug.DrawLine(transform.position, transform.position + Wander(), Color.cyan);
            Debug.Log("zombie wander");
        }

        Vector3 avoid = AvoidObstacle(obstacles);

        if (avoid != Vector3.zero)
        {
            Debug.DrawLine(transform.position, transform.position + avoid, Color.green);
            Debug.Log("avoiding");
            final += (avoidWeight * avoid);
        }

        Vector3 separate = Separation(zombies);

        if (separate != Vector3.zero)
        {
            Debug.DrawLine(transform.position, transform.position + separate, Color.white);
            Debug.Log("separating");
            final += (avoidWeight * Separation(zombies));
        }

        if (manager.GetComponent<ExerciseManager>().OutOfBounds(transform.position, boundsDistance))
        {
            Debug.Log("bounds");
            final += (boundsWeight * Bounds());
        }

        if(final != Vector3.zero)
        {
            final = final.normalized * maxForce;
        }

        ApplyForce(final);
    }

    public GameObject FindClosest()
    {
        GameObject closest = humans[0];
        foreach (GameObject g in humans)
        {
            if (Vector3.Distance(g.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = g;
            }
        }

        target = closest;

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

            //Draw tracking line
            if(target != null)
            {
                tracking.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Vertex(transform.position);
                GL.Vertex(target.transform.position);
                GL.End();
            }
        }
        else
        {
            future.SetActive(false);
        }
    }
}
