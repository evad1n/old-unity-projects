using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour {
    /// <summary>
    /// Will Dickinson
    /// Generates gameobjects randomly across a terrain
    /// </summary>

    public GameObject objectToPlace;
    [Range(30, 200)]
    public int numberOfObjects = 50;
    public float offset = 0.5f;

    private TerrainData myTerrainData;
    private Terrain terrain;

    // Use this for initialization
    void Start () {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        terrain = gameObject.GetComponent<Terrain>();

        Generate();
	}
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Generates objects that are positioned randomly across a terrain
    /// </summary>
    void Generate()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int xPos = Random.Range(0, (int)myTerrainData.size.x);
            int zPos = Random.Range(0, (int)myTerrainData.size.z);
            float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));

            Vector3 pos = new Vector3(xPos, yPos + offset, zPos);

            Instantiate(objectToPlace, pos, Quaternion.identity);
        }
    }
}
