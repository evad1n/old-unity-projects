using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Handles initial creation of asteroids
/// </summary>
public class AsteroidManager : MonoBehaviour {

    public GameObject[] asteroidPrefabs;

    private float timer = 0;
    private float interval = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate() {
        timer += Time.deltaTime;

        if(timer > interval)
        {
            SpawnAsteroid();
            timer = 0;
            interval = Random.Range(1, 2.5f);
        }
	}

    /// <summary>
    /// Spawns asteroids at random intervals with random directions at random positions along the edge of the screen
    /// </summary>
    void SpawnAsteroid()
    {
        int r = Random.Range(0, 3);

        GameObject asteroid = Instantiate(asteroidPrefabs[r]);
        GetComponent<CollisionDetection>().asteroids.Add(asteroid);

        r = Random.Range(0, 4);
        Vector3 spawnPos;

        switch (r)
        {
            case 0:
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(0.1f, 0.9f), 10));
                break;
            case 1:
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0.1f, 0.9f), 10));
                break;
            case 2:
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), -0.1f, 10));
                break;
            case 3:
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 10));
                break;
            default:
                spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
                break;
        }

        asteroid.transform.position = spawnPos;
    }
}
