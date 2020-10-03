using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockNumbers : MonoBehaviour {

    public GameObject number;

    private const float Radius = 2.25f;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 12; i++)
        {
            float angle = (i * 30 + 90) * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Cos(angle) * Radius, Mathf.Sin(angle) * Radius, 0);
            GameObject instance = (GameObject)Instantiate(number, pos, Quaternion.identity);

            TextMesh text = instance.GetComponentInChildren<TextMesh>();
            text.text = "" + (12 - i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
