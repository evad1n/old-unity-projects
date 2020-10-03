using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// This script was necessary to align the power bar to the side of the screen due to different resolutions and the way I made the power bar
/// </summary>
public class Position : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
