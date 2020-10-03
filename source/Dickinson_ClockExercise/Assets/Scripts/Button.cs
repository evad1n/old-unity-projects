using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject hand;

    private RotateHand script;

	// Use this for initialization
	void Start () {
        script = hand.GetComponent<RotateHand>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        script.enabled = !script.enabled;
    }
}
