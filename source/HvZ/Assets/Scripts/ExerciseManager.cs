using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseManager : MonoBehaviour {

    public GameObject Human;
    public GameObject Zombie;
    public GameObject Obstacle;
    public GameObject death;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public int numHumans;
    public int numObstacles;
    public int numZombies;

    public bool debug = false;
    public Material bounds;

    public List<GameObject> humans;
    public List<GameObject> zombies;
    public List<GameObject> obstacles;

    // Use this for initialization
    void Start () {
        GUI.color = Color.red;

        obstacles = new List<GameObject>();
        for (int i = 0; i < numObstacles; i++)
        {
            obstacles.Add(Instantiate(Obstacle));
            obstacles[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }

        humans = new List<GameObject>();
        for(int i = 0; i < numHumans; i++)
        {
            humans.Add(Instantiate(Human));
            humans[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            humans[i].GetComponent<Human>().manager = gameObject;
        }

        zombies = new List<GameObject>();
        for (int i = 0; i < numZombies; i++)
        {
            zombies.Add(Instantiate(Zombie));
            zombies[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            zombies[i].GetComponent<Zombie>().manager = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int h = humans.Count - 1; h >= 0; h--)
        {
            for (int z = zombies.Count - 1; z >= 0; z--)
            {
                if (SphereCollision(humans[h], zombies[z]))
                {
                    GameObject spawned = Instantiate(Zombie);
                    zombies.Add(spawned);
                    spawned.transform.position = humans[h].transform.position;
                    spawned.GetComponent<Zombie>().manager = gameObject;

                    Instantiate(death, humans[h].transform.position, Quaternion.identity);

                    GameObject deleted = humans[h];
                    humans.RemoveAt(h);
                    Destroy(deleted);
                }
            }
        }
    }

    /// <summary>
    /// Method to determine collision using bounding spheres
    /// </summary>
    /// <param name="A">Game object with Vehicle script</param>
    /// <param name="B">Game object with Vehicle script</param>
    /// <returns></returns>
    bool SphereCollision(GameObject A, GameObject B)
    {
        Vehicle a = A.GetComponent<Vehicle>();
        Vehicle b = B.GetComponent<Vehicle>();

        float x = Mathf.Pow(A.transform.position.x - B.transform.position.x, 2);
        float y = Mathf.Pow(A.transform.position.y - B.transform.position.y, 2);
        float z = Mathf.Pow(A.transform.position.z - B.transform.position.z, 2);

        if (x + y + z > Mathf.Pow(a.radius + b.radius, 2))
            return false;

        return true;
    }

    public bool OutOfBounds(Vector3 pos, float boundsDistance)
    {
        if (pos.x > maxX - boundsDistance || pos.x < minX + boundsDistance || pos.y < minY + boundsDistance || pos.y > maxY - boundsDistance || pos.z < minZ + boundsDistance || pos.z > maxZ - boundsDistance)
            return true;
        else
            return false;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(25, 25, 100, 50), "Debug View"))
            debug = !debug;

        if (GUI.Button(new Rect(150, 25, 100, 50), "Spawn Human"))
        {
            GameObject h = Instantiate(Human);
            humans.Add(h);
            h.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            h.GetComponent<Human>().manager = gameObject;
        }

        if (GUI.Button(new Rect(800, 25, 100, 50), "Restart"))
        {
            SceneManager.LoadScene(0);
        }

        if (GUI.Button(new Rect(925, 25, 100, 50), "Quit"))
        {
            Application.Quit();
        }

    }

    void OnRenderObject()
    {
        if (debug)
        {
            //Draw Bounds
            bounds.SetPass(0);

            //Bottom Box

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, minY, maxZ));
            GL.Vertex(new Vector3(minX, minY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, minY, minZ));
            GL.Vertex(new Vector3(maxX, minY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(maxX, minY, maxZ));
            GL.Vertex(new Vector3(maxX, minY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(maxX, minY, maxZ));
            GL.Vertex(new Vector3(minX, minY, maxZ));
            GL.End();

            //Top Box

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, maxY, maxZ));
            GL.Vertex(new Vector3(minX, maxY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, maxY, minZ));
            GL.Vertex(new Vector3(maxX, maxY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(maxX, maxY, maxZ));
            GL.Vertex(new Vector3(maxX, maxY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, maxY, maxZ));
            GL.Vertex(new Vector3(maxX, maxY, maxZ));
            GL.End();

            //Vertical Lines

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, minY, maxZ));
            GL.Vertex(new Vector3(minX, maxY, maxZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(minX, minY, minZ));
            GL.Vertex(new Vector3(minX, maxY, minZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(maxX, minY, maxZ));
            GL.Vertex(new Vector3(maxX, maxY, maxZ));
            GL.End();

            GL.Begin(GL.LINES);
            GL.Vertex(new Vector3(maxX, minY, minZ));
            GL.Vertex(new Vector3(maxX, maxY, minZ));
            GL.End();
        }

    }

}
