using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Represents the marinara that appears during marinara madness
/// </summary>
public class Marinara : MonoBehaviour {

    public Vector3 direction;

    private GameObject manager;
    private float speed = 0.2f;
    private float lifeTime = 0;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime += Time.deltaTime;

        transform.position += (direction * speed);

        if (lifeTime > 2f)
            Kill();
    }

    //Destroys the tomato
    public void Kill()
    {
        manager.GetComponent<CollisionDetection>().tomatoes.Remove(gameObject);
        Destroy(gameObject);
    }
}
