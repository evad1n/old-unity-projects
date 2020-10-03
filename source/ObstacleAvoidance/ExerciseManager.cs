using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager : MonoBehaviour {

    public GameObject Human;
    public GameObject Zombie;
    public GameObject Target;
    public GameObject Obstacle;
    public float minx, maxx, minz, maxz, y;
    public int numHumans;
    public int numObstacles;

    private List<GameObject> humans;
    private List<GameObject> obstacles;

    // Use this for initialization
    void Start () {
        obstacles = new List<GameObject>();
        for (int i = 0; i < numObstacles; i++)
        {
            obstacles.Add(Instantiate(Obstacle));
            obstacles[i].transform.position = new Vector3(Random.Range(minx, maxx), y, Random.Range(minz, maxz));
        }

        Zombie = Instantiate(Zombie);
        Zombie.transform.position = new Vector3(Random.Range(minx, maxx), y, Random.Range(minz, maxz));
        Zombie.GetComponent<Zombie>().obstacles = obstacles; 

        Target = Instantiate(Target);
        Target.transform.position = new Vector3(Random.Range(minx, maxx), y, Random.Range(minz, maxz));


        humans = new List<GameObject>();
        for(int i = 0; i < numHumans; i++)
        {
            humans.Add(Instantiate(Human));
            humans[i].transform.position = new Vector3(Random.Range(minx, maxx), y, Random.Range(minz, maxz));
            humans[i].GetComponent<Human>().seekTarget = Target;
            humans[i].GetComponent<Human>().fleeTarget = Zombie;
            humans[i].GetComponent<Human>().obstacles = obstacles;
        }
    }
	
	// Update is called once per frame
	void Update () {
        GameObject closest = humans[0];
        foreach (GameObject g in humans)
        {
            if(Vector3.Distance(g.transform.position, Target.transform.position) < 4)
            {
                Target.transform.position = new Vector3(Random.Range(minx, maxx), y, Random.Range(minz, maxz));
            }

            if (Vector3.Distance(g.transform.position, Zombie.transform.position) < Vector3.Distance(closest.transform.position, Zombie.transform.position))
            {
                closest = g;
            }
        }

        Zombie.GetComponent<Zombie>().target = closest;
	}
}
