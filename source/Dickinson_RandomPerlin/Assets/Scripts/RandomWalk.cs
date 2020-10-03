using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour {

    private bool walking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
            walking = !walking;

        if(walking)
        {
            int x = 0;
            int z = 0;
            float r = Random.Range(0f, 1f);
            if (r < 0.2)
                x = -1;
            else if (r < 0.4)
                x = 1;
            else if (r < 0.6)
                z = -1;
            else if (r < 0.8)
                z = 1;

            this.transform.Translate(x, 0, z);
        }

    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        GUI.skin.box.fontSize = 20;

        GUI.Box(new Rect(200, 100, 200, 100), "Press the 'M' key to toggle walker movement");

        GUI.skin.box.wordWrap = true;
    }
}
