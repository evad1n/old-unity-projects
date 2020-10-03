using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunt : MonoBehaviour {

    public GameObject chest;
    public bool near = false;

	// Use this for initialization
	void Start () {
        GUI.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, chest.transform.position) < 5)
        {
            near = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                chest.GetComponent<Animator>().SetBool("open", true);
                chest.GetComponent<AudioSource>().Play();
            }
        }
        else
            near = false;
	}

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 60), "Hold 'Q' to go up \n Hold 'E' to go down \n  Hold SHIFT to accelerate");

        if(near)
            GUI.Box(new Rect(Screen.width/2 - 100, Screen.height - 70, 200, 60), "Press SPACE to open the chest");
    }
}
