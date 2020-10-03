using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// A class for an asteroid that moves linearly and splits apart into smaller asteroids when destroyed
/// </summary>
public class Asteroid : MonoBehaviour {

    public GameObject child;
    public GameObject death;
    public Vector3 direction;
    public float speed;
    public int level = 1;

    private GameObject manager;

    // Use this for initialization
    void Awake () {
        if(level == 1)
        {
            direction = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            direction.Normalize();

            speed = Random.Range(0.01f, 0.05f);
        }
        
        gameObject.GetComponent<Wrap>().velocity = direction;
        manager = GameObject.FindGameObjectWithTag("manager");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        transform.position += (direction * speed);
	}

    /// <summary>
    /// Split an asteroid into two smaller asteroids
    /// </summary>
    public void Split()
    {
        Gui scoring = manager.GetComponent<Gui>();

        switch (level)
        {
            case 2:
                scoring.score += 50;
                break;
            case 3:
                scoring.score += 100;
                break;
            default:
                scoring.score += 20;
                break;
        }

        if(level < 3)
        {
            Asteroid script;

            GameObject firstChild = Instantiate(child);
            firstChild.transform.position = gameObject.transform.position;

            script = firstChild.GetComponent<Asteroid>();
            script.level = level + 1;
            script.speed = Random.Range(speed + speed / 4, speed + speed / 2);
            script.direction = (direction += new Vector3(Random.Range(direction.x - 0.5f, direction.x + 0.5f), Random.Range(direction.y - 0.5f, direction.y + 0.5f), 0)).normalized;

            GameObject secondChild = Instantiate(child);
            secondChild.transform.position = gameObject.transform.position;

            script = secondChild.GetComponent<Asteroid>();
            script.level = level + 1;
            script.speed = Random.Range(speed + speed / 4, speed + speed / 2);
            script.direction = (direction += new Vector3(Random.Range(direction.x - 0.5f, direction.x + 0.5f), Random.Range(direction.y - 0.5f, direction.y + 0.5f), 0)).normalized;

            manager.GetComponent<CollisionDetection>().asteroids.Add(firstChild);
            manager.GetComponent<CollisionDetection>().asteroids.Add(secondChild);
        }

        //Death particle effect
        GameObject effect = Instantiate(death);
        effect.transform.position = transform.position;
        effect.transform.localScale *= (1 / Mathf.Pow(level, 2));

        manager.GetComponent<CollisionDetection>().asteroids.Remove(gameObject);
        Destroy(gameObject);
    }
}
