using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public int nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(nextScene);
        }
	}

    private void OnGUI()
    {
        GUI.color = Color.black;

        GUI.skin.box.fontSize = 20;

        GUI.Box(new Rect(1500, 100, 200, 100), "Press the 'C' key to switch between scenes");

        GUI.skin.box.wordWrap = true;
    }
}
