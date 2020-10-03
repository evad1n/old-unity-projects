using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class that manages everything that the other scripts can't
/// Will Dickinson
/// </summary>
public class ExerciseManager : MonoBehaviour {

    public GameObject flocker;
    public GameObject Obstacle;
    public GameObject pathFollower;
    public GameObject flowFieldFollower;
    public int minX = -10;
    public int maxX = 10;
    public int minY = 0;
    public int maxY = 50;
    public int minZ = -10;
    public int maxZ = 10;
    public int numFlockers;
    public int numObstacles;
    public int numPathFollowers;
    public int numFlowFieldFollowers;
    public bool debug = false;
    public GameObject[] waypoints;

    public Material forward;
    public Material path;
    //public GameObject center;

    public List<GameObject> flockers;
    public List<GameObject> obstacles;
    public List<GameObject> pathFollowers;
    public List<GameObject> flowFieldFollowers;
    public List<List<GameObject>> flocks;
    public List<GameObject> centerObjects;

    public TerrainData terrainData;

    // Use this for initialization
    void Start () {
        GUI.color = Color.red;

        terrainData = GetComponent<Terrain>().terrainData;

        //Calculate terrain bounds
        minX = (int)transform.position.x;
        maxX = (int)transform.position.x + (int)terrainData.size.x;
        minZ = (int)transform.position.z;
        maxZ = (int)transform.position.z + +(int)terrainData.size.z;

        flocks = new List<List<GameObject>>();

        //centerObjects = new List<GameObject>();
        //for (int i = 0; i < numFlockers; i++)
        //{
        //    centerObjects.Add(Instantiate(center));
        //}

        flockers = new List<GameObject>();
        for (int i = 0; i < numFlockers; i++)
        {
            flockers.Add(Instantiate(flocker));
            flockers[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            flockers[i].GetComponent<Flocker>().manager = gameObject;
        }

        pathFollowers = new List<GameObject>();
        for (int i = 0; i < numPathFollowers; i++)
        {
            pathFollowers.Add(Instantiate(pathFollower));
            pathFollowers[i].transform.position = new Vector3(Random.Range(minX, maxX), 5, Random.Range(minZ, maxZ));
            pathFollowers[i].GetComponent<PathFollower>().manager = gameObject;
        }

        flowFieldFollowers = new List<GameObject>();
        for (int i = 0; i < numFlowFieldFollowers; i++)
        {
            flowFieldFollowers.Add(Instantiate(flowFieldFollower));
            flowFieldFollowers[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            flowFieldFollowers[i].GetComponent<FlowFieldFollower>().manager = gameObject;
            flowFieldFollowers[i].GetComponent<FlowFieldFollower>().flowScript = GetComponent<FlowField>();
        }

        obstacles = new List<GameObject>();
        for (int i = 0; i < numObstacles; i++)
        {
            obstacles.Add(Instantiate(Obstacle));
            obstacles[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }

        //Disable debug view at the start
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].transform.GetChild(0).gameObject.SetActive(debug);
            GetComponent<Resistance>().resistanceArea.SetActive(debug);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Find all flocks
        foreach (GameObject g in flockers)
        {
            List<GameObject> localFlock = new List<GameObject>();
            foreach (GameObject f in flockers)
            {
                if (Vector3.Distance(g.transform.position, f.transform.position) < f.GetComponent<Flocker>().flockDistance)
                {
                    localFlock.Add(f);
                }
            }

            bool same = false;

            foreach (List<GameObject> l in flocks)
            {
                if (l == localFlock)
                    same = true;
            }
            if (!same)
                flocks.Add(localFlock);
        }

        //Toggle debug view
        if (Input.GetKeyDown(KeyCode.D))
        {
            debug = !debug;

            for(int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i].transform.GetChild(0).gameObject.SetActive(debug);
            }

            GetComponent<Resistance>().resistanceArea.SetActive(debug);
        }
    }

    public bool OutOfBounds(Vector3 pos, float boundsDistance)
    {
        if (pos.x > maxX - boundsDistance || pos.x < minX + boundsDistance || pos.y < minY + boundsDistance || pos.y > maxY - boundsDistance || pos.z < minZ + boundsDistance || pos.z > maxZ - boundsDistance)
            return true;
        else
            return false;
    }

    //public GameObject GetFlockCenter()
    //{

    //    return center;
    //}

    /// <summary>
    /// Returns the height of the terrain at the object's world position
    /// </summary>
    /// <param name="pos">The object's world position</param>
    /// <returns></returns>
    public float GetHeight(Vector3 pos)
    {
        return GetComponent<Terrain>().SampleHeight(pos);
    }

    void OnRenderObject()
    {
        if (debug)
        {
            path.SetPass(0);

            for (int i = 0; i < waypoints.Length; i++)
            {
                if (i != waypoints.Length - 1)
                {
                    GL.Begin(GL.LINES);
                    GL.Vertex(waypoints[i].transform.position);
                    GL.Vertex(waypoints[i + 1].transform.position);
                    GL.End();
                }
                else
                {
                    GL.Begin(GL.LINES);
                    GL.Vertex(waypoints[i].transform.position);
                    GL.Vertex(waypoints[0].transform.position);
                    GL.End();
                }

            }
        }


        //Vector3[] centers = new Vector3[flocks.Count];
        //Vector3[] directions = new Vector3[flocks.Count];

        ////Get centers of flocks
        //for (int i = 0; i < flocks.Count; i++)
        //{
        //    Vector3 center = Vector3.zero;
        //    foreach (GameObject g in flocks[i])
        //    {
        //        center += g.transform.position;
        //    }

        //    center /= flocks[i].Count;
        //    centers[i] = center;
        //}

        ////Get directions of flocks
        //for (int i = 0; i < flocks.Count; i++)
        //{
        //    Vector3 dir = Vector3.zero;
        //    foreach (GameObject g in flocks[i])
        //    {
        //        dir += g.GetComponent<Vehicle>().velocity;
        //    }

        //    directions[i] = dir.normalized * 3;
        //}

        //for (int i = 0; i < flocks.Count; i++)
        //{
        //    //Draw forward vector
        //    forward.SetPass(0);
        //    GL.Begin(GL.LINES);
        //    GL.Vertex(centers[i]);
        //    GL.Vertex(centers[i] + directions[i]);
        //    GL.End();

        //    //Position centers of flocks

        //    centerObjects[i].transform.forward = directions[i];
        //    centerObjects[i].transform.position = centers[i];
        //}
    }

}
