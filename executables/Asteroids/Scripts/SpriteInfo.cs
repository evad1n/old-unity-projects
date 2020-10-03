using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will Dickinson
/// A class that has helper methods for collision detection
/// </summary>
public class SpriteInfo : MonoBehaviour {

    private SpriteRenderer sprite;

    public float radius = 2;

	// Use this for initialization
	void Awake () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        Debug.DrawLine(sprite.bounds.min, new Vector3(MinX(), MaxY()));
        Debug.DrawLine(sprite.bounds.min, new Vector3(MaxX(), MinY()));
        Debug.DrawLine(sprite.bounds.center, sprite.bounds.center + new Vector3(radius, 0, 0));
    }

    public void SetColor(Color color)
    {
        sprite.color = color;
    }

    public float MaxX()
    {
        Vector3 max = sprite.bounds.max;
        return max.x;
    }

    public float MaxY()
    {
        Vector3 max = sprite.bounds.max;
        return max.y;
    }

    public float MinX()
    {
        Vector3 min = sprite.bounds.min;
        return min.x;
    }

    public float MinY()
    {
        Vector3 min = sprite.bounds.min;
        return min.y;
    }

    public Vector3 Center()
    {
        return sprite.bounds.center;
    }
}
