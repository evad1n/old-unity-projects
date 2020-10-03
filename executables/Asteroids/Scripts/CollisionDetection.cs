using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Manages all collisions between asteroids, bullets and the player
/// </summary>
public class CollisionDetection : MonoBehaviour {

    public GameObject ship;
    public List<GameObject> asteroids;
    public List<GameObject> bullets;
    public List<GameObject> tomatoes;

    private AudioSource audio;


    // Use this for initialization
    void Start () {
        bullets = new List<GameObject>();
        asteroids = new List<GameObject>();
        audio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        for(int a = asteroids.Count - 1; a >= 0; a--)
        {
            if(CircleCollision(asteroids[a], ship))
            {
                ship.GetComponent<Lives>().Kill();
            }

            for (int b = bullets.Count - 1; b >= 0; b--)
            {
                if(CircleCollision(asteroids[a], bullets[b]))
                {
                    asteroids[a].GetComponent<Asteroid>().Split();
                    bullets[b].GetComponent<Bullet>().Kill();
                    audio.Play();
                    break;
                }
            }

            for (int t = tomatoes.Count - 1; t >= 0; t--)
            {
                if (CircleCollision(asteroids[a], tomatoes[t]))
                {
                    asteroids[a].GetComponent<Asteroid>().Split();
                    tomatoes[t].GetComponent<Marinara>().Kill();
                    audio.Play();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Method to determine collision using axis-aligned bounding boxes
    /// </summary>
    /// <param name="A">Game object with sprite info script</param>
    /// <param name="B">Game object with sprite info script</param>
    /// <returns></returns>
    bool AABBCollision(GameObject A, GameObject B)
    {
        SpriteInfo a = A.GetComponent<SpriteInfo>();
        SpriteInfo b = B.GetComponent<SpriteInfo>();

        if (b.MinX() < a.MaxX() && b.MaxX() > a.MinX() && b.MaxY() > a.MinY() && b.MinY() < a.MaxY())
            return true;

        return false;

    }

    /// <summary>
    /// Method to determine collision using bounding circles
    /// </summary>
    /// <param name="A">Game object with sprite info script</param>
    /// <param name="B">Game object with sprite info script</param>
    /// <returns></returns>
    bool CircleCollision(GameObject A, GameObject B)
    {
        SpriteInfo a = A.GetComponent<SpriteInfo>();
        SpriteInfo b = B.GetComponent<SpriteInfo>();

        float x = Mathf.Pow(a.Center().x - b.Center().x, 2);
        float y = Mathf.Pow(a.Center().y - b.Center().y, 2);

        if (x + y > Mathf.Pow(a.radius + b.radius, 2))
            return false;

        return true;
    }
}
