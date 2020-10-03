using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Controls movement and lifetime of a bullet
/// </summary>
public class Bullet : MonoBehaviour {

    public Vector3 direction;

    private GameObject manager;
    private float speed = 0.3f;
    private float lifeTime = 0;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("manager");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        lifeTime += Time.deltaTime;

        transform.position += (direction * speed);

        if (lifeTime > 1.5f)
            Kill();
	}

    //Destroy the bullet
    public void Kill()
    {
        manager.GetComponent<CollisionDetection>().bullets.Remove(gameObject);
        Destroy(gameObject);
    }
}
