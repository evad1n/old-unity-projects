using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that destroys the game object it is attached to after a certain time
/// </summary>
public class DeathTimer : MonoBehaviour {

    public float delay = 2;

    private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		timer += Time.deltaTime;

        if (timer > delay)
            Destroy(gameObject);
    }
}
