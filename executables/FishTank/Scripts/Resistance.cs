using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information about the area of resistance and how much resistance it should cause
/// </summary>
public class Resistance : MonoBehaviour {

    public GameObject resistanceArea;

    public float x, y, z;
    public float width, height, depth;
    public float coefficient = 0.5f;

	// Use this for initialization
	void Start () {
        width = resistanceArea.transform.localScale.x;
        height = resistanceArea.transform.localScale.y;
        depth = resistanceArea.transform.localScale.z;
        x = resistanceArea.transform.position.x - width / 2;
        y = resistanceArea.transform.position.y - height / 2;
        z = resistanceArea.transform.position.z - depth / 2;
    }
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Returns the drag coefficient corresponding to an object's location in comparison to the area of resistance
    /// </summary>
    /// <param name="pos">The object's location</param>
    /// <returns></returns>
    public float GetDrag(Vector3 pos)
    {
        if (pos.x > x && pos.x < x + width && pos.y > y && pos.y < y + height && pos.z > z && pos.z < z + depth)
        {
            return coefficient;
        }
        else
            return 0;
    }
}
