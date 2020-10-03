using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public GameObject pink;
    public GameObject blue;
    public GameObject green;

    private List<GameObject> creatures;

	// Use this for initialization
	void Start () {
        creatures = new List<GameObject>();

        pink = Spawn(pink);
        pink.GetComponent<Vehicle>().mass = 1;
        creatures.Add(pink);

        blue = Spawn(blue);
        blue.GetComponent<Vehicle>().mass = 5;
        creatures.Add(blue);

        green = Spawn(green);
        green.GetComponent<Vehicle>().mass = 20;
        creatures.Add(green);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject g in creatures)
                g.GetComponent<Vehicle>().friction = !g.GetComponent<Vehicle>().friction;
        }
	}

    private GameObject Spawn(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 1));
        return obj;
    }
}
