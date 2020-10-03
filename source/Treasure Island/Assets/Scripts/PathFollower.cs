using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a vehicle that follows a path of waypoints
/// Will Dickinson
/// </summary>
public class PathFollower : MonoBehaviour{

    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    public float waypointArriveDistance = 3;
    public float speed = 0.1f;

    private Vector3 position;

    // Use this for initialization
    public void Start()
    {
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

    public void Update()
    {
        position = transform.position;

        //Check if we are close enough to our target and then switch to next waypoint
        if(Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) < waypointArriveDistance)
        {
            currentWaypoint++;

            if(currentWaypoint > waypoints.Length - 1)
            {
                currentWaypoint = 0;
            }
        }

        transform.LookAt(waypoints[currentWaypoint].transform.position);

        transform.position = Vector3.MoveTowards(position, waypoints[currentWaypoint].transform.position, speed);
    }
}
