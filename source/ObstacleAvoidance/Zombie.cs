using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Vehicle {

    public float seekWeight;
    public float avoidWeight;
    public GameObject target;

    public List<GameObject> obstacles;

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    protected override void CalcSteeringForces()
    {
        Vector3 final = Vector3.zero;

        final += (seekWeight * Seek(target.transform.position));

        Debug.DrawLine(transform.position, target.transform.position, Color.black);

        for (int i = 0; i < obstacles.Count; i++)
        {
            final += (avoidWeight * AvoidObstacle(obstacles[i].transform.position, 3));
        }

        final = final.normalized * maxForce;

        ApplyForce(final);
    }
}
