using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a vehicle that moves by following a flow field
/// Will Dickinson
/// </summary>
public class FlowFieldFollower : Vehicle {

    public float boundsWeight = 1;
    public float avoidWeight = 1;
    public float alignWeight = 1;
    public float followWeight = 1;
    public float separationWeight = 1;
    public GameObject manager;

    private List<GameObject> obstacles;
    private List<GameObject> flowFieldFollowers;
    public FlowField flowScript;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        flowFieldFollowers = manager.GetComponent<ExerciseManager>().flowFieldFollowers;
        flowScript = manager.GetComponent<FlowField>();
        obstacles = manager.GetComponent<ExerciseManager>().obstacles;
    }

    protected override void CalcSteeringForces()
    {

        Vector3 final = Vector3.zero;

        final += (followWeight * flowScript.GetFlowDirection(transform.position));

        final += (avoidWeight * AvoidObstacle(obstacles));

        final += (separationWeight * Separation(flowFieldFollowers));

        final += Drag(manager.GetComponent<Resistance>().GetDrag(transform.position));

        if (manager.GetComponent<ExerciseManager>().OutOfBounds(transform.position, boundsDistance))
            final += (boundsWeight * Bounds());

        final = final.normalized * maxForce;

        ApplyForce(final);
    }
}
