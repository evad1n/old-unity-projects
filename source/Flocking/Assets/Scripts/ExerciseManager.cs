using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Managers all flockers and any interaction in the scene
/// Will Dickinson
/// </summary>
public class ExerciseManager : MonoBehaviour {

    public GameObject Fish;
    public GameObject Obstacle;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public int numFlockers;
    public int numObstacles;
    public float flockDistance;

    public Material forward;
    public GameObject center;

    public List<GameObject> flockers;
    public List<GameObject> obstacles;
    public List<List<GameObject>> flocks;
    public List<GameObject> centerObjects;

    // Use this for initialization
    void Start () {
        GUI.color = Color.red;

        flockDistance = Fish.GetComponent<Flocker>().flockDistance;

        flocks = new List<List<GameObject>>();

        centerObjects = new List<GameObject>();
        for (int i = 0; i < numFlockers; i++)
        {
            centerObjects.Add(Instantiate(center));
        }

        flockers = new List<GameObject>();
        for (int i = 0; i < numFlockers; i++)
        {
            flockers.Add(Instantiate(Fish));
            flockers[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            flockers[i].GetComponent<Flocker>().manager = gameObject;
        }

        obstacles = new List<GameObject>();
        for (int i = 0; i < numObstacles; i++)
        {
            obstacles.Add(Instantiate(Obstacle));
            obstacles[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            foreach(GameObject g in flockers)
            {
                g.GetComponent<Flocker>().flockDistance++;
                flockDistance = g.GetComponent<Flocker>().flockDistance;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (GameObject g in flockers)
            {
                g.GetComponent<Flocker>().flockDistance--;
                flockDistance = g.GetComponent<Flocker>().flockDistance;
            }
        }


        flocks = new List<List<GameObject>>();

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

            flocks.Add(localFlock);
        }
    }

    /// <summary>
    /// Checks if this position is out of bounds
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="boundsDistance"></param>
    /// <returns></returns>
    public bool OutOfBounds(Vector3 pos, float boundsDistance)
    {
        if (pos.x > maxX - boundsDistance || pos.x < minX + boundsDistance || pos.y < minY + boundsDistance || pos.y > maxY - boundsDistance || pos.z < minZ + boundsDistance || pos.z > maxZ - boundsDistance)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns a random flock center
    /// </summary>
    /// <returns></returns>
    public GameObject GetFlockCenter()
    {
        int r = Random.Range(0, numFlockers);

        return centerObjects[r];
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(50, 50, 200, 80), "Flock Distance: " + flockDistance + "\n Use the UP and DOWN arrow \nkeys to change the distance");
    }

    void OnRenderObject()
    {
        Vector3[] centers = new Vector3[flocks.Count];
        Vector3[] directions = new Vector3[flocks.Count];

        //Get centers of flocks
        for(int i = 0; i < flocks.Count; i++)
        {
            Vector3 center = Vector3.zero;
            foreach (GameObject g in flocks[i])
            {
                center += g.transform.position;
            }

            center /= flocks[i].Count;
            centers[i] = center;
        }

        //Get directions of flocks
        for (int i = 0; i < flocks.Count; i++)
        {
            Vector3 dir = Vector3.zero;
            foreach (GameObject g in flocks[i])
            {
                dir += g.GetComponent<Vehicle>().velocity;
            }

            directions[i] = dir.normalized * 3;
        }

        for (int i = 0; i < flocks.Count; i++)
        {
            //Draw forward vector
            forward.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(centers[i]);
            GL.Vertex(centers[i] + directions[i]);
            GL.End();

            //Position centers of flocks

            centerObjects[i].transform.forward = directions[i];
            centerObjects[i].transform.position = centers[i];
        }
    }

}
