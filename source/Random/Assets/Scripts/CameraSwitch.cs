using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    /// <summary>
    /// Will Dickinson
    /// Cycles through a list of cameras with the C key
    /// Also contains a GUI script that identifies each camera
    /// </summary>

    public int currentCamera;
    public GameObject[] cameras;
    //Descriptions of cameras
    public string[] desc;

    // Use this for initialization
    void Start () {

    }
	
	/// <summary>
    /// Cycles through an array of cameras using the C key
    /// </summary>
	void Update () {
		if(Input.GetKeyDown(KeyCode.C))
        {
            cameras[currentCamera].gameObject.SetActive(false);

            currentCamera++;

            if (currentCamera > cameras.Length - 1)
                currentCamera = 0;

            cameras[currentCamera].gameObject.SetActive(true);
        }
	}

    private void OnGUI()
    {
        GUI.color = Color.red;

        GUI.skin.box.fontSize = 20;

        GUI.Box(new Rect(10, 10, 200, 120), "Press 'C' to change camera views \nCamera " + currentCamera + "\n" + desc[currentCamera]);

        GUI.skin.box.wordWrap = true;
    }
}
