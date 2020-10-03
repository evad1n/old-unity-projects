using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public Camera camera;
    public GameObject player;

    private bool gameOver = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Adjust camera position
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
            camera.transform.position = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);

        //Check if player has fallen off the screen
        if (player.transform.position.y < camera.rect.bottom)
        {
            //Game over
            gameOver = true;
        }
    }
}
