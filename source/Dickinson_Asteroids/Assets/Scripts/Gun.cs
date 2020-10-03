using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// Used to shoot bullets automatically and manually by pressing space
/// </summary>
public class Gun : MonoBehaviour {

    public GameObject bullet;
    public GameObject marinara;
    public GameObject powerBar;

    private GameObject manager;
    private bool canShoot = true;
    private float shootTimer = 0;
    private Vector3 direction;

    //power ups
    private bool madness = false;
    private float marinaraTimer = 0;
    private float powerTimer = 0;
    private float duration = 5;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("manager");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        direction = gameObject.GetComponent<Vehicle>().direction;

        if (Input.GetKey(KeyCode.Space))
        {
            if (canShoot)
            {
                Shoot();
                canShoot = false;
            }
        }

        if(!canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > 0.2f)
            {
                canShoot = true;
                shootTimer = 0;
            }
        }

        //MARINARA MADNESS
        if (madness)
        {
            powerTimer += Time.deltaTime;
            marinaraTimer += Time.deltaTime;

            if (marinaraTimer > 0.05f)
            {
                GameObject shot = Instantiate(marinara);
                shot.transform.position = gameObject.transform.position;
                shot.GetComponent<Marinara>().direction = (direction += new Vector3(Random.Range(direction.x - 0.5f, direction.x + 0.5f), Random.Range(direction.y - 0.5f, direction.y + 0.5f), 0)).normalized;
                manager.GetComponent<CollisionDetection>().tomatoes.Add(shot);
                marinaraTimer = 0;
            }

            if (powerTimer > duration)
            {
                madness = false;
                powerTimer = 0;
                powerBar.GetComponent<PowerUp>().Reset();
            }
        }
	}

    /// <summary>
    /// Shoots a bullet in the direction the ship is facing
    /// </summary>
    void Shoot()
    {
        GameObject shot = Instantiate(bullet);
        manager.GetComponent<CollisionDetection>().bullets.Add(shot);
        shot.transform.position = gameObject.transform.position;
        Bullet b = shot.GetComponent<Bullet>();
        b.direction = direction;
        shot.transform.rotation = Quaternion.Euler(0, 0, gameObject.GetComponent<Vehicle>().totalRotation + 90);
    }

    /// <summary>
    /// A power up that shoots tomato sauce everywhere
    /// </summary>
    public void MarinaraMadness()
    {
        madness = true;
    }
}
