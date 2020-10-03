using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Script to have a power meter that fills up on screen as a sprite
/// </summary>
public class PowerUp : MonoBehaviour {

    public GameObject ship;
    public bool ready = false;

    private GameObject manager;
    private float value = 0;
    private float maxValue = 1000;
    private float flashTimer = 0;
    //Used for multiple power ups
    private float offset = 0;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("manager");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        value = manager.GetComponent<Gui>().score - offset;


        //Power up ready
        if (value >= maxValue)
        {
            ready = true;

            value = maxValue;
            flashTimer += Time.deltaTime;

            //Flash while ready
            if (flashTimer > 0)
                gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            if (flashTimer > 0.1f)
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (flashTimer > 0.2f)
                flashTimer = 0;

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                ship.GetComponent<Gun>().MarinaraMadness();
                ready = false;
                offset = 100000000;
            }

        }
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        if (value < 0)
            value = 0;

        transform.localScale = new Vector3((value/maxValue), transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// Resets the power bar to empty
    /// </summary>
    public void Reset()
    {
        offset = manager.GetComponent<Gui>().score;
    }
}
