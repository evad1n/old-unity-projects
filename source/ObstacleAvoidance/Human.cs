using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Vehicle {

    public float seekWeight;
    public float fleeWeight;
    public float avoidWeight;
    public GameObject seekTarget;
    public GameObject fleeTarget;

    public List<GameObject> obstacles;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    protected override void CalcSteeringForces()
    {
        Vector3 final = Vector3.zero;

        final += (seekWeight * Seek(seekTarget.transform.position));

        if(Vector3.Distance(fleeTarget.transform.position, transform.position) < 6)
        {
            final += (fleeWeight * Flee(fleeTarget.transform.position));
            Debug.DrawLine(transform.position, fleeTarget.transform.position, Color.red);
        }

        for(int i = 0; i < obstacles.Count; i++)
        {
            Vector3 avoid = avoidWeight * AvoidObstacle(obstacles[i].transform.position, 3);

            if(avoid != Vector3.zero)
            {
                Debug.DrawLine(transform.position, obstacles[i].transform.position, Color.green);
                final += avoid;
            }
        }

        final = final.normalized * maxForce;

        ApplyForce(final);
    }
}
