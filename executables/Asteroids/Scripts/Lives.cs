using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Controls the player lives and triggers game over
/// </summary>
public class Lives : MonoBehaviour {


    private GameObject manager;
    private int lives = 3;
    private float invulnerabilityTimer;
    private float flashTimer;
    private bool invulnerable = false;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("manager");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if(invulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            flashTimer += Time.deltaTime;

            //Flash while invulnerable
            if (flashTimer > 0)
                gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            if (flashTimer > 0.1f)
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            if (flashTimer > 0.2f)
                flashTimer = 0;
        }

        if (invulnerabilityTimer > 2)
        {
            invulnerabilityTimer = 0;
            invulnerable = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
	}

    /// <summary>
    /// Takes one life away and recenters the player, also triggers game over when lives reach 0
    /// </summary>
    public void Kill()
    { 
        if(!invulnerable && lives > 0)
        {
            lives--;
            invulnerable = true;

            manager.GetComponent<Gui>().lives = lives;

            if (lives <= 0)
            {
                manager.GetComponent<Gui>().GameOver();
                Destroy(gameObject);
                manager.GetComponent<CollisionDetection>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Vehicle>().Reset();
            }
        }
    }
}
