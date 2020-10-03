using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    public float jumpForce = 10;
    public Camera camera;

    Vector2 velocity;
    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);
        rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -10, 10), Mathf.Clamp(rigidbody.velocity.y, -10, 10));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "edge")
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            Debug.Log("edge");
        }

    }
}
