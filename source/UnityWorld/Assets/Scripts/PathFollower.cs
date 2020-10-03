using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a vehicle that follows a path of waypoints
/// Will Dickinson
/// </summary>
public class PathFollower : Vehicle {

    public float avoidWeight = 1;
    public float alignWeight = 1;
    public float followWeight = 1;
    public float boundsWeight = 1;
    public float separationWeight = 1;
    public GameObject manager;
    public int currentWaypoint;
    public float waypointArriveDistance = 3;

    private List<GameObject> obstacles;
    private List<GameObject> pathFollowers;
    public GameObject[] waypoints;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        obstacles = manager.GetComponent<ExerciseManager>().obstacles;
        waypoints = manager.GetComponent<ExerciseManager>().waypoints;
        pathFollowers = manager.GetComponent<ExerciseManager>().pathFollowers;

        //Find closest waypoint to start
        float dist = 1000;
        for(int i = 0; i < waypoints.Length; i++)
        {
            if(Vector3.Distance(waypoints[i].transform.position, transform.position) < dist)
            {
                dist = Vector3.Distance(waypoints[i].transform.position, transform.position);
                currentWaypoint = i;
            }
        }
    }

    protected override void CalcSteeringForces()
    {
        Vector3 final = Vector3.zero;

        //Check if we are close enough to our target and then switch to next waypoint
        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) < waypointArriveDistance)
        {
            //Cycle through waypoints
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        final += (followWeight * Seek(waypoints[currentWaypoint].transform.position));

        final += (separationWeight * Separation(pathFollowers));

        final += (avoidWeight * AvoidObstacle(obstacles));

        final += Drag(manager.GetComponent<Resistance>().GetDrag(transform.position));

        final = final.normalized * maxForce;

        ApplyForce(final);

        //Keep vehicle grounded
        transform.position = new Vector3(transform.position.x, manager.GetComponent<ExerciseManager>().GetHeight(transform.position) + 0.2f, transform.position.z);
    }
}
